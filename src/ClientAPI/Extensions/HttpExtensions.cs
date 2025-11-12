using Core.Domain.Interfaces.Https;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
using FluentValidation;
using Refit;

namespace ClientAPI.Extensions;

public static class HttpExtensions
{
    public static IServiceCollection AddHttpsServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddRefitClient<IHttpAddressService>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://viacep.com.br/");
        });

        return services;
    }
}