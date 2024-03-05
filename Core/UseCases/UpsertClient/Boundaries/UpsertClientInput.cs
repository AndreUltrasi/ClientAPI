using Core.Domain;
using Core.ViewModel;

namespace Core.UseCases.UpsertClient.Boundaries
{
    public class UpsertClientInput
    {
        public UpsertClientInput(Guid correlationId, ClientViewModel client) {
            Client = client;
            CorrelationId = correlationId;
        }

        public ClientViewModel Client { get; private set; }
        public Guid CorrelationId { get; private set; }
    }
}