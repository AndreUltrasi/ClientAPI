using Core.Interfaces.UseCases;
using Core.UseCases.DeleteClient;
using Core.UseCases.GetClient;
using Core.UseCases.UpsertClient;

namespace ClientAPI.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetClient, GetClient>();
        services.AddScoped<IUpsertClient, UpsertClient>();
        services.AddScoped<IDeleteClient, DeleteClient>();

        return services;
    }
}