using ImpressioApi_.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ImpressioApi_.WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public abstract class ImpressioController : ControllerBase
{
    protected new IActionResult Response(CommandResult result)
    {
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}