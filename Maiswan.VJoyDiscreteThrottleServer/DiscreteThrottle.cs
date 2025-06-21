using vJoyInterfaceWrap;

namespace Maiswan.vJoyThrottleServer;

public class DiscreteThrottle : IDisposable
{
	private int throttle = 0;
	public int Throttle
	{
		get => throttle;
		private set
		{
			throttle = value;
			UpdateVJoyAxisValue();
		}
	}

	public int NormalizedThrottle => GetJoystickValue();
	public int MaxBrake { get; set; } = 0;
	public int MaxPower { get; set; } = 0;

	private readonly vJoy joystick;
	private const int id = 1;
	private const int Scale = 32767;

	public DiscreteThrottle(int maxBrake, int maxPower)
	{
		MaxBrake = maxBrake;
		MaxPower = maxPower;
		joystick = new();
		InitializeVJoy();
		UpdateVJoyAxisValue();
	}

	// https://qiita.com/Limitex/items/23faaf3a0ef6ca8832e1
	private void InitializeVJoy()
	{
		if (!joystick.vJoyEnabled())
		{
			Console.WriteLine("vJoy driver not enabled.");
			return;
		}

		if (!joystick.isVJDExists(id))
		{
			Console.WriteLine("vJoy Device with ID {0} is not available.", id);
			return;
		}

		VjdStat status = joystick.GetVJDStatus(id);
		if (status != VjdStat.VJD_STAT_FREE)
		{
			Console.WriteLine("vJoy Device with ID {0} is not free.", id);
			return;
		}

		if (!joystick.AcquireVJD(id))
		{
			Console.WriteLine("vJoy Device with ID {0} dose not activate.", id);
			return;
		}
	}

	private int GetJoystickValue()
	{
		const double middle = 1/2d;
		double value = Throttle / (double)(Throttle >= 0 ? MaxPower : -1 * MaxBrake);
		return (int)((middle + value / 2) * Scale);
	}

	private void UpdateVJoyAxisValue()
	{
		joystick.SetAxis(GetJoystickValue(), id, HID_USAGES.HID_USAGE_RX);
	}

	public void SetMaxBrake() => Throttle = MaxBrake;
	public void SetNeutral() => Throttle = 0;
	public void SetMaxPower() => Throttle = MaxPower;
	public void Increment() => Throttle += Throttle < MaxPower ? 1 : 0;
	public int Decrement() => Throttle += Throttle > MaxBrake ? -1 : 0;

	public void Dispose()
	{
		joystick.RelinquishVJD(id);
		GC.SuppressFinalize(this);
	}
}
