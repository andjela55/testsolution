using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthController : Controller
    {

        [HttpGet]
        public ActionResult<string> Health()
        {
            return Ok("Healthy app");
        }
    }
}
