using Maiswan.VJoyDiscreteThrottle.Throttle;
using Microsoft.AspNetCore.Mvc;

namespace Maiswan.VJoyDiscreteThrottle.Server;

[ApiController]
[Route("api/v2")]
public class Controller(SseExtensions sse, DiscreteThrottle throttle) : ControllerBase
{
    private readonly SseExtensions sse = sse;
	private readonly DiscreteThrottle throttle = throttle;

    [HttpGet("throttle/stream")]
    public async Task GetThrottleStream() => await sse.NewStreamAsync(HttpContext, Response);

    [HttpGet("throttle")]
    public IActionResult GetThrotle() => Ok(throttle.ThrottleState);

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
