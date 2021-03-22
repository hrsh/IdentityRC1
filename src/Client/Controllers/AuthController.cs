using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("~/login")]
        public IActionResult Login() =>
            Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = "/"
                },
                OpenIdConnectDefaults.AuthenticationScheme);

        [HttpGet("~/logout"), HttpPost("~/logout")]
        public IActionResult Logout() =>
            SignOut(
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
    }
}
