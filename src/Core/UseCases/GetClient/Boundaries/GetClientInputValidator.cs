using FluentValidation;

namespace Core.UseCases.GetClient.Boundaries
{
    public class GetClientInputValidator : AbstractValidator<GetClientInput>
    {
        public GetClientInputValidator()
        {
            RuleFor(x => x.AccountCode).GreaterThan(0);
        }
    }
}
