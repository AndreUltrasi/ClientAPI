using Core.Domain;
using Core.ViewModel;

namespace Core.UseCases.UpsertClient.Boundaries
{
    public class UpsertClientInput
    {
        public UpsertClientInput(ClientViewModel client) {
            Client = client;
        }

        public ClientViewModel Client { get; private set; }
    }
}