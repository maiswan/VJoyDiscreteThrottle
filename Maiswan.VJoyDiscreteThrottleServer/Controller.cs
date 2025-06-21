using Microsoft.AspNetCore.Mvc;

namespace Maiswan.vJoyThrottleServer;

[ApiController]
[Route("api/throttle")]
public class Controller : ControllerBase
{
    private readonly ILogger<Controller> logger;
	private readonly DiscreteThrottle throttle;

	public Controller(ILogger<Controller> logger, DiscreteThrottle throttle)
    {
        this.logger = logger;
        this.throttle = throttle;
    }

    [HttpGet]
    public IActionResult GetRaw()
    {
        return Ok(throttle.Throttle);
	}

	[HttpGet("normalized")]
	public IActionResult GetNormalized()
	{
		return Ok(throttle.NormalizedThrottle);
	}

	[HttpPost("increment")]
	public IActionResult IncrementThrottle()
	{
		throttle.Increment();
		return GetRaw();
	}

	[HttpPost("decrement")]
	public IActionResult DecrementThrottle()
	{
		throttle.Decrement();
		return GetRaw();
	}

	[HttpPost("neutral")]
	public IActionResult SetNeutralThrottle()
	{
		throttle.SetNeutral();
		return GetRaw();
	}
}
