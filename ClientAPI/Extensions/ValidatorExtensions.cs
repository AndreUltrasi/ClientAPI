using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
using FluentValidation;

namespace ClientAPI.Extensions;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetClientInput>, GetClientInputValidator>();
        services.AddScoped<IValidator<UpsertClientInput>, UpsertClientInputValidator>();
        services.AddScoped<IValidator<DeleteClientInput>, DeleteClientInputValidator>();

        return services;
    }
}