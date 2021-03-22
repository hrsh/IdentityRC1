using Api1.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Validation.AspNetCore;
using Shared;

namespace Api1
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseSqlite(_configuration.GetConnectionString("identity-rc1-sqlite"));
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    options.SetIssuer(ServiceDefaultConfig.ServerUrl);
                    options.AddAudiences(ServiceDefaultConfig.Api1Id);
                    options.UseIntrospection()
                        .SetClientId(ServiceDefaultConfig.Api1Id)
                        .SetClientSecret(ServiceDefaultConfig.Api1Secret);
                    options.UseSystemNetHttp();
                    options.UseAspNetCore();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
