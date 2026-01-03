using Maiswan.VJoyDiscreteThrottle.Throttle;
using Microsoft.AspNetCore.Mvc;

namespace Maiswan.VJoyDiscreteThrottle.Server;

[ApiController]
[Route("api/v1")]
public class Controller(SseExtensions sse, DiscreteThrottle throttle) : ControllerBase
{
    private readonly SseExtensions sse = sse;
	private readonly DiscreteThrottle throttle = throttle;

    [HttpGet("throttle/stream")]
    public async Task GetThrottleStream() => await sse.NewStreamAsync(
        HttpContext, Response,
        e => new { e.Throttle, e.OldThrottle }
    );

    [HttpGet("throttle/scaled/stream")]
    public async Task GetThrottleScaledStream() => await sse.NewStreamAsync(
        HttpContext, Response,
        e => new { e.ThrottleScaled, e.OldThrottleScaled }
    );

    [HttpGet("notch/stream")]
    public async Task GetNotchStream() => await sse.NewStreamAsync(
        HttpContext, Response,
        e => new { e.Notch, e.OldNotch }
    );

    [HttpGet("throttle")]
    public IActionResult GetThrotle() => Ok(throttle.Throttle);

	[HttpGet("throttle/scaled")]
	public IActionResult GetThrottleScaled() => Ok(throttle.ThrottleScaled);

	[HttpPost("notch/increment")]
	public IActionResult IncrementNotch() => Ok(throttle.IncrementNotch());

	[HttpPost("notch/decrement")]
	public IActionResult DecrementNotch() => Ok(throttle.DecrementNotch());

	[HttpPost("notch/neutral")]
	public IActionResult SetNeutralNotch() => Ok(throttle.SetNeutralNotch());

    [HttpPost("notch/neutral/toward")]
    public IActionResult SetTowardNeutralNotch() => Ok(throttle.SetTowardNeutralNotch());

    [HttpPost("notch/min")]
    public IActionResult SetMinNotch() => Ok(throttle.SetMinNotch());

    [HttpPost("notch/max")]
    public IActionResult SetMaxNotch() => Ok(throttle.SetMaxNotch());
}
