using Core.Domain;
using Core.UseCases.GetClient.Boundaries;

//Core lida com regras de negocio
namespace Core.UseCases.GetClient
{
    public class GetClient : IGetClient
    {
        public async Task<Client> Handle(GetClientInput input)
        {

        }



    }
}