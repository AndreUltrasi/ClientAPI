using Core;
using Infra.Repositories;

namespace ClientAPI.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}