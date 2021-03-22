using Api1.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Api1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CatalogController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity is null) return BadRequest();

            var list = await _context.Catalogs.ToListAsync();

            return Ok(list);
        }

        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken ct)
        {
            var t = await _context.Catalogs.FirstOrDefaultAsync(a => a.Id == id, ct);
            return t is null ? NoContent() : Ok(t);
        }
    }
}
