using Core.UseCases.DeleteClient.Boundaries;
using FluentValidation;

namespace Core.UseCases.GetClient.Boundaries
{
    public class DeleteClientInputValidator : AbstractValidator<DeleteClientInput>
    {
        public DeleteClientInputValidator()
        {
            RuleFor(x => x.AccountCode).GreaterThan(0);
        }
    }
}
