using Core.Domain;

namespace Core.UseCases.UpsertClient.Boundaries
{
    public class UpsertClientInput
    {
        public UpsertClientInput(Client client) {
            Client = client;
        }

        public Client Client { private get; set; }
    }
}