namespace Core.UseCases.GetClient.Boundaries
{
    public class GetClientInput
    {
        public GetClientInput(Guid correlationId,
                              int id) { 
            AccountCode = id;
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; private set; }
        public int AccountCode { get; private set; }
    }
}