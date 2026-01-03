using vJoyInterfaceWrap;

namespace Maiswan.VJoyDiscreteThrottle.Throttle;

public class DiscreteThrottle : IDisposable
{
	public double Throttle => Notches[CurrentNotchIndex];
    public int ThrottleScaled => GetThrottleScaled();
    private int GetThrottleScaled()
	{
		// Map [-1, 1] to [0, Scale]
		return (int)((Throttle + 1) / 2 * Scale);
    }

	private double[] notches = [0];
	public double[] Notches
	{
		get => notches;
		set
		{
			notches = [.. value.OrderBy(x => x)];
			CurrentNotchIndex = currentNotchIndex; // Reclamp current index
        }
	}

	private int currentNotchIndex = 0;
	public int CurrentNotchIndex
	{
		get => currentNotchIndex;
		set
		{
			currentNotchIndex = Math.Clamp(value, 0, Notches.Length - 1);
			UpdateVJoyDevice();
		}
    }

    private int neutralNotchIndex = 0;
    public int NeutralNotchIndex
    {
        get => neutralNotchIndex;
        set => neutralNotchIndex = Math.Clamp(value, 0, Notches.Length - 1);
    }


    private readonly vJoy joystick = new();
	private readonly uint joystickId;
	private const int Scale = 32767;
	private const HID_USAGES Axis = HID_USAGES.HID_USAGE_RX;


    public DiscreteThrottle(Configuration config)
    {
		joystickId = config.JoystickId;
        InitializeVJoy();

		Notches = config.Notches;
		currentNotchIndex = config.DefaultNotch;
        neutralNotchIndex = config.NeutralNotch;
	}

	// https://qiita.com/Limitex/items/23faaf3a0ef6ca8832e1
	private void InitializeVJoy()
	{
		if (!joystick.vJoyEnabled())
		{
			throw new InvalidOperationException("vJoy driver not enabled.");
		}

		if (!joystick.isVJDExists(joystickId))
		{
			throw new InvalidOperationException(
				string.Format("vJoy Device {0} does not exist.", joystickId)
			);
		}

		VjdStat status = joystick.GetVJDStatus(joystickId);
		if (status != VjdStat.VJD_STAT_FREE)
		{
            throw new InvalidOperationException(
                string.Format("vJoy Device {0} is not free.", joystickId)
            );
		}

		if (!joystick.AcquireVJD(joystickId))
		{
            throw new InvalidOperationException(
                string.Format("vJoy Device {0} cannot be acquired.", joystickId)
            );
        }
	}
    public void Dispose()
    {
        joystick.RelinquishVJD(joystickId);
        GC.SuppressFinalize(this);
    }

    private void UpdateVJoyDevice()
	{
		joystick.SetAxis(GetThrottleScaled(), joystickId, Axis);
	}

    // Shortcuts (if the caller doesn't want to touch CurrentNotchIndex directly)
    // Over/underflow handled by CurrentNotchIndex setter
    public int SetMinNotch() => CurrentNotchIndex = 0;
	public int SetNeutralNotch() => CurrentNotchIndex = neutralNotchIndex;
    public int SetMaxNotch() => CurrentNotchIndex = Notches.Length - 1;

	// ~ OpenBVE/BVE's Q/A/Z controls
    public int DecrementNotch() => CurrentNotchIndex--;
	public int SetTowardNeutralNotch()
	{
		if (CurrentNotchIndex == NeutralNotchIndex) { return CurrentNotchIndex; }
		return CurrentNotchIndex > NeutralNotchIndex ? DecrementNotch() : IncrementNotch();
	}
    public int IncrementNotch() => CurrentNotchIndex++;
}
