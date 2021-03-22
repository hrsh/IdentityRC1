using Blog.WebClients;
using Client.WebClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/")]
        public IActionResult Index() => View();

        [Authorize, HttpGet("~/cataloges")]
        public async Task<IActionResult> Cataloges(
            [FromServices] CatalogClient catalogClient,
            CancellationToken ct)
        {
            var t = await catalogClient.GetCatalogesAsync(ct);
            return View(model: t);
        }

        [Authorize, HttpGet("~/blogs")]
        public async Task<IActionResult> Blogs(
            [FromServices] BlogClient blogClient,
            CancellationToken ct)
        {
            var t = await blogClient.GetBlogsAsync(ct);
            return View(model: t);
        }
    }
}
