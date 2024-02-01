namespace Core.UseCases.GetClient.Boundaries
{
    public class GetClientInput
    {
        public GetClientInput(int id) { 
            AccountCode = id;
        }

        public int AccountCode { get; private set; }
    }
}