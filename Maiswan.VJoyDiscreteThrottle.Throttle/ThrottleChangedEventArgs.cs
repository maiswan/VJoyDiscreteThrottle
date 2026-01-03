namespace Maiswan.VJoyDiscreteThrottle.Throttle;

public class ThrottleChangedEventArgs(
    double notch, double throttle, int throttleScaled,
    double oldNotch, double oldThrottle, double oldThrottleScaled
) : EventArgs
{
    public double Notch { get; init; } = notch;
    public double Throttle { get; init; } = throttle;
    public double ThrottleScaled { get; init; } = throttleScaled;

    public double OldNotch { get; init; } = oldNotch;
    public double OldThrottle { get; init; } = oldThrottle;
    public double OldThrottleScaled { get; init; } = oldThrottleScaled;
}
