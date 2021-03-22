using Client.WebClients;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
            var token = await HttpContext.GetTokenAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectParameterNames.AccessToken);
            if (string.IsNullOrWhiteSpace(token)) return BadRequest("Request not permitted");

            var t = await catalogClient.GetCatalogesAsync(token, ct);
            return View(model: t);
        }

        [Authorize, HttpGet("~/blogs")]
        public async Task<IActionResult> Blogs(
            [FromServices] BlogClient blogClient,
            CancellationToken ct)
        {
            var token = await HttpContext.GetTokenAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectParameterNames.AccessToken);
            if (string.IsNullOrWhiteSpace(token)) return BadRequest("Request not permitted");
            var t = await blogClient.GetBlogsAsync(token, ct);
            return View(model: t);
        }
    }
}
