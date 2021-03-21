using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using Server.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Server
{
    public class IdentityWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public IdentityWorker(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken ct)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            if (!await context.Database.EnsureCreatedAsync())
                throw new InvalidOperationException("Database not found!");

            await CreateApplicationAsync();
            await CretaeScopesAsync();

            async Task CreateApplicationAsync()
            {
                var manager = scope
                    .ServiceProvider
                    .GetRequiredService<IOpenIddictApplicationManager>();

                if (await manager.FindByClientIdAsync("web_client") is null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "web_client",
                        DisplayName = "Web Client",
                        PostLogoutRedirectUris =
                        {
                            new Uri("https://localhost:6004/signout-oidc")
                        },
                        RedirectUris =
                        {
                            new Uri("https://localhost:6004/signin-oidc")
                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.ResponseTypes.IdToken,
                            Permissions.ResponseTypes.IdTokenToken,
                            Permissions.ResponseTypes.Token,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "api1",
                            Permissions.Prefixes.Scope + "api2"
                        }
                    };
                    await manager.CreateAsync(descriptor, ct);
                }

                if (await manager.FindByClientIdAsync("api1") is null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "api1",
                        ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                    };

                    await manager.CreateAsync(descriptor);
                }

                if (await manager.FindByClientIdAsync("api2") is null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "api2",
                        ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                    };

                    await manager.CreateAsync(descriptor);
                }
            }

            async Task CretaeScopesAsync()
            {
                var manager = scope
                    .ServiceProvider
                    .GetRequiredService<IOpenIddictScopeManager>();

                if (await manager.FindByNameAsync("api1") is null)
                {
                    await manager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        Name = "api1",
                        Resources =
                        {
                            "api1"
                        }
                    });
                }

                if (await manager.FindByNameAsync("api2") == null)
                {
                    await manager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        Name = "api2",
                        Resources =
                        {
                            "api2"
                        }
                    });
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
