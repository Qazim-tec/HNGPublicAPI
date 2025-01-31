using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HngApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDetails()
        {
            return Ok(new
            {
                email = "yetkemsupper@gmail.com", 
                current_datetime = DateTime.UtcNow.ToString("o"), // ISO 8601 format
                github_url = "https://github.com/Qazim-tec/HNGPublicAPI.git" 
            });
        }
    }
}
