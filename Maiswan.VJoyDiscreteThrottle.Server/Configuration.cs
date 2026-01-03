using Maiswan.VJoyDiscreteThrottle.Throttle;

namespace Maiswan.VJoyDiscreteThrottle.Server;

public record ServerConfiguration : Configuration
{
    public required uint ServerPort { get; init; }
}