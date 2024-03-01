using Core.Domain;
using Core.UseCases.GetClient.Boundaries;

//Core lida com regras de negocio
namespace Core.UseCases.GetClient
{
    public class GetClient : IGetClient
    {
        private readonly IClientRepository _clientRepository;

        public GetClient(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Output> Handle(GetClientInput input)
        {
            var output = new Output();

            var validationResult = new GetClientInputValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    output.AddErrorMessage(error.ErrorMessage);
                }
                return output;
            }

            var client = await _clientRepository.GetAsync(input.AccountCode);

            output.AddResult(client!);
            return output;
        }
    }
}