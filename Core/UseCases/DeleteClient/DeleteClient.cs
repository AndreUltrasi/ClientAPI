using Core.Domain;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using FluentValidation;

namespace Core.UseCases.DeleteClient
{
    public class DeleteClient : IDeleteClient
    { 
        public async Task<Output> Handle(DeleteClientInput input)
        {
            var output = new Output();

            var validationResult = new DeleteClientInputValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    output.AddErrorMessage(error.ErrorMessage);
                }
                return output;
            }

            //var success = await _clientRepository.DeleteAsync(input.AccountCode);
            return output;
        }
    }
}