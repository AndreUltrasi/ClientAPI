using Core;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClientContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Client"))
        );

        services.AddScoped<IClientRepository, ClientRepository>();
        return services;
    }
}