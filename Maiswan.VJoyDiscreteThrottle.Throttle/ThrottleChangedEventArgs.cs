namespace Maiswan.VJoyDiscreteThrottle.Throttle;

public class ThrottleChangedEventArgs(ThrottleState throttleState, ThrottleState oldThrottleState) : EventArgs
{
    public ThrottleState ThrottleState { get; } = throttleState;
    public ThrottleState OldThrottleState { get; } = oldThrottleState;
}
