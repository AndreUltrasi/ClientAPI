namespace Core.UseCases.DeleteClient.Boundaries
{
    public class DeleteClientInput
    {
        public DeleteClientInput(Guid correlationId,
                                int id) { 
            AccountCode = id;
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; private set; }
        public int AccountCode { get; private set; }
    }
}