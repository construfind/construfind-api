using Microsoft.AspNetCore.Mvc;

namespace ConstruFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpPost]
        public IActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}
