using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using Server.Data;
using Shared;
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

            //if (!await context.Database.EnsureCreatedAsync())
            //    throw new InvalidOperationException("Database not found!");

            await CreateApplicationAsync(ct);
            await CretaeScopesAsync(ct);

            async Task CreateApplicationAsync(CancellationToken ct)
            {
                var manager = scope
                    .ServiceProvider
                    .GetRequiredService<IOpenIddictApplicationManager>();

                if (await manager.FindByClientIdAsync(ServiceDefaultConfig.WebClientId, ct) is null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = ServiceDefaultConfig.WebClientId,
                        DisplayName = ServiceDefaultConfig.WebClientDisplayName,
                        ClientSecret = ServiceDefaultConfig.WebClientSecret,
                        ConsentType = ConsentTypes.Explicit,
                        PostLogoutRedirectUris =
                        {
                            new Uri($"{ServiceDefaultConfig.WebClientUrl}signout-oidc")
                        },
                        RedirectUris =
                        {
                            new Uri($"{ServiceDefaultConfig.WebClientUrl}signin-oidc")
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
                            Permissions.Prefixes.Scope + ServiceDefaultConfig.Api1Id,
                            Permissions.Prefixes.Scope + ServiceDefaultConfig.Api2Id
                        },
                        Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                    };
                    await manager.CreateAsync(descriptor, ct);
                }

                if (await manager.FindByClientIdAsync(ServiceDefaultConfig.Api1Id, ct) is null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = ServiceDefaultConfig.Api1Id,
                        ClientSecret = ServiceDefaultConfig.Api1Secret,
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                    };
                    await manager.CreateAsync(descriptor, ct);
                }

                if (await manager.FindByClientIdAsync(ServiceDefaultConfig.Api2Id, ct) is null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = ServiceDefaultConfig.Api2Id,
                        ClientSecret = ServiceDefaultConfig.Api2Secret,
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                    };

                    await manager.CreateAsync(descriptor, ct);
                }
            }

            async Task CretaeScopesAsync(CancellationToken ct)
            {
                var manager = scope
                    .ServiceProvider
                    .GetRequiredService<IOpenIddictScopeManager>();

                if (await manager.FindByNameAsync(ServiceDefaultConfig.Api1Id, ct) is null)
                {
                    await manager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        Name = ServiceDefaultConfig.Api1Id,
                        Resources =
                        {
                            ServiceDefaultConfig.Api1Id
                        }
                    }, ct);
                }

                if (await manager.FindByNameAsync(ServiceDefaultConfig.Api2Id, ct) == null)
                {
                    await manager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        Name = ServiceDefaultConfig.Api2Id,
                        Resources =
                        {
                            ServiceDefaultConfig.Api2Id
                        }
                    }, ct);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}
