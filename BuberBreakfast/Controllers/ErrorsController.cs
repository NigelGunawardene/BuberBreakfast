using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

//[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
