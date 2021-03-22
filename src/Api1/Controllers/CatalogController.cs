using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        public CatalogController()
        {

        }

        [HttpGet("index")]
        public IActionResult Index() => Ok("Catalog controller index");
    }
}
