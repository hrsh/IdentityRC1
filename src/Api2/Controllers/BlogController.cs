using Api2.Context;
using Api2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api2.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [Route("~/")] public IActionResult Index() => Ok(nameof(BlogController));

        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Route("list")]
        public async Task<IEnumerable<Blog>> Index(CancellationToken ct) =>
            await _context.Blogs.ToListAsync(ct);
    }
}
