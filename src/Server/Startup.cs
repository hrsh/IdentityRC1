using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Shared;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment) =>
            (_configuration, _webHostEnvironment) = (configuration, webHostEnvironment);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseSqlite(_configuration.GetConnectionString("identity-rc1-sqlite"));
                //cfg.UseSqlServer(_configuration.GetConnectionString("identity-rc1-sql"));
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
                    core.UseEntityFrameworkCore(a =>
                    {
                        a.UseDbContext<AppDbContext>();
                    });
                    //core.UseMongoDb(a =>
                    //{
                    //    a.Configure(cfg =>
                    //    {
                            
                    //    });
                    //});
                })
                .AddServer(server =>
                {
                    server.SetAuthorizationEndpointUris("/connect/authorize")
                    .SetLogoutEndpointUris("/connect/logout")
                    .SetIntrospectionEndpointUris("/connect/introspect")
                    .SetUserinfoEndpointUris("/connect/userinfo")
                    .SetTokenEndpointUris("/connect/token");

                    server.RegisterScopes("email", "profile", "roles");

                    server.AllowAuthorizationCodeFlow();

                    ////
                    server.AllowClientCredentialsFlow();
                    server.SetTokenEndpointUris("");
                    ////

                    server.AddEncryptionKey(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(ServiceDefaultConfig.EncryptionKey)));

                    //server.AddEncryptionKey(
                    //    new SymmetricSecurityKey(
                    //        Convert.FromBase64String(ServiceDefaultConfig.DigitalKey)));

                    var fileName = _configuration.GetSection("Certificate:FileName").Value;
                    var password = _configuration.GetSection("Certificate:Password").Value;
                    var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, fileName);
                    server.AddSigningCertificate(
                        new X509Certificate2(fileName: filePath, password: password));

                    server.UseAspNetCore()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableTokenEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .EnableStatusCodePagesIntegration();
                })
                .AddValidation(opt =>
                {
                    opt.UseLocalServer();
                    opt.UseAspNetCore();
                });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddHostedService<IdentityWorker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
