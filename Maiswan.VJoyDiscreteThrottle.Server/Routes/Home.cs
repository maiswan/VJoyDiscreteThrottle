using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Maiswan.VJoyDiscreteThrottle.Server;

[ApiController]
public class Home : ControllerBase
{
    [HttpGet("")]
    public IActionResult HomeRoute() => Ok(new
    {
        program = "VJoyDiscreteThrottle",
        author = "maiswan",
        version = Assembly.GetEntryAssembly()?.GetName().Version,
    });
}
