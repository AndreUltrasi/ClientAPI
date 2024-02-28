using Core.Domain;
using Core.UseCases.UpsertClient.Boundaries;
using System.Threading.Tasks;

namespace Core.UseCases.UpsertClient
{
    public class UpsertClient : IUpsertClient
    {
        private readonly IClientRepository _clientRepository;

        public UpsertClient(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Output> Handle(UpsertClientInput input)
        {

            var output = new Output();


            if (input.Client == null)
            {

                output.AddErrorMessage("O objeto Client não pode ser nulo");
            }

            var sucess = await _clientRepository.PostAsync(input.Client);

            output.AddResult(sucess);

            return output;


        }
    }
}
