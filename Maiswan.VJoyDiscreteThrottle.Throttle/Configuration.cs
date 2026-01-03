using System.Text.Json.Serialization;

namespace Maiswan.VJoyDiscreteThrottle.Throttle;

public record Configuration
{
	public required double[] Notches { get; init; }
	public required int DefaultNotch { get; init; }
	public required int NeutralNotch { get; init; }

	public required uint JoystickId { get; init; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public required HID_USAGES Axis { get; init; }
}
