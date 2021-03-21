using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")] public IActionResult Index() => View();
    }
}
