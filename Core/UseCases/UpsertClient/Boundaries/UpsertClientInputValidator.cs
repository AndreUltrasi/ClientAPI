using Core.UseCases.UpsertClient.Boundaries;
using FluentValidation;

namespace Core.UseCases.GetClient.Boundaries
{
    public class UpsertClientInputValidator : AbstractValidator<UpsertClientInput>
    {
        public UpsertClientInputValidator()
        {
            RuleFor(x => x.Client).NotNull();
            RuleFor(x => x.Client.AccountCode).GreaterThan(0);
            RuleFor(x => x.Client.Age).GreaterThan(0);
            RuleFor(x => x.Client.Cep).NotEmpty();
            RuleFor(x => x.Client.Gender).NotEmpty();
            RuleFor(x => x.Client.Name).NotEmpty();
            RuleFor(x => x.Client.Number).NotEmpty();
            RuleFor(x => x.Client.PersonType).NotEmpty();
        }
    }
}
