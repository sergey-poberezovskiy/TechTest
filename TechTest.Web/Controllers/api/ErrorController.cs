using Microsoft.AspNetCore.Mvc;

namespace TechTest.Web.Controllers.api
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}