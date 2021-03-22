using Api1.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Api1
{
    public static class Installer
    {
        public static async Task<IHost> StartServerAsync(this IHost host)
        {
            using var scope = host
                .Services
                .CreateScope();

            using var context = scope
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();
            return host;
        }
    }
}
