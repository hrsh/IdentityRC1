using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseSqlite(Configuration.GetConnectionString("identity-rc1"));
                cfg.UseOpenIddict();
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.ClaimsIdentity.UserNameClaimType = "name";
                cfg.ClaimsIdentity.UserIdClaimType = "sub";
                cfg.ClaimsIdentity.RoleClaimType = "role";
            });

            services.AddOpenIddict()
                .AddCore(core =>
                {
                    core.UseEntityFrameworkCore()
                    .UseDbContext<AppDbContext>();
                })
                .AddServer(server =>
                {
                    server.SetAuthorizationEndpointUris("")
                    .SetLogoutEndpointUris("")
                    .SetIntrospectionEndpointUris("")
                    .SetUserinfoEndpointUris("");

                    server.RegisterScopes("email", "profile", "roles");

                    server.AddEncryptionKey(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Consts.EncryptionKey)));

                    server.AddSigningCertificate(new X509Certificate2(fileName: ""));
                });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddHostedService<IdentityWorker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
