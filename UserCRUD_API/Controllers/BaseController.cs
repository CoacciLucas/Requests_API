using Application;
using Application.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace USERCRUD_API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {

        protected BaseController(IMediator handle)
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new IActionResult Response(object result = null)
        {
            if (IsOperationValid())
            {
                if (result != null && result.GetType() == typeof(CommandResult))
                {
                    CommandResult commandResult = (CommandResult)result;
                    if (commandResult.Data != null)
                    {
                        return Created("", commandResult.Data);
                    }

                    return NoContent();
                }

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            return BadRequest(new CommandResult("Error"));
        }

    }
}
