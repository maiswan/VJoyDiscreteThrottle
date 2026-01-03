using Maiswan.VJoyDiscreteThrottle.Throttle;
using Microsoft.AspNetCore.Mvc;

namespace Maiswan.VJoyDiscreteThrottle.Server;

[ApiController]
[Route("api/v1")]
public class Controller : ControllerBase
{
    private readonly ILogger<Controller> logger;
	private readonly DiscreteThrottle throttle;

	public Controller(ILogger<Controller> logger, DiscreteThrottle throttle)
    {
        this.logger = logger;
        this.throttle = throttle;
    }

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
