using Core.Domain;
using Core.UseCases.GetClient.Boundaries;

namespace Core.UseCases.GetClient
{
    public class GetClient : IGetClient
    {
        public async Task<Client> Handle(GetClientInput input)
        {
            return (Client)null;
        }
    }
}