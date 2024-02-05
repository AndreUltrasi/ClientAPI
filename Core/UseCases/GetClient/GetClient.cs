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
            if(input.AccountCode <= 0)
            {
                output.AddErrorMessage("Não é possível retornar cliente com número de conta menor ou igual a zero !");
                return output;
            }
            var client = await _clientRepository.GetAsync(input.AccountCode);
            return new Output(client);
        }
    }
}