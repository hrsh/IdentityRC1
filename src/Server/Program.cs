using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Server.Data;
using System;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

            try
            {
                Log.Information("Creating host ...");
                using var host = CreateHostBuilder(args).Build();
                Log.Information("Creating scope ...");
                using var scope = host.Services.CreateScope();

                var context = scope
                    .ServiceProvider
                    .GetRequiredService<AppDbContext>();

                Log.Information("Start pending migration ...");
                await context.Database.MigrateAsync();

                var userManager = scope
                    .ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                var roleManager = scope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                Log.Information("Start databse seed method ...");
                await Seed.SeedDatabaseAsync(userManager, roleManager);

                Log.Information("Starting host ...");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
