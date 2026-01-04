using System;
namespace Maiswan.VJoyDiscreteThrottle.Throttle;

public class ThrottleState
{
    public double Throttle { get; internal set; }
    public double ThrottleScaled { get; internal set; }
    public int Notch { get; internal set; }

    public ThrottleState DeepClone() => new()
    {
        Throttle = Throttle,
        ThrottleScaled = ThrottleScaled,
        Notch = Notch,
    };
}
