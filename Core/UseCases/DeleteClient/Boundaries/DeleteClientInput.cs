namespace Core.UseCases.DeleteClient.Boundaries
{
    public class DeleteClientInput
    {
        public DeleteClientInput(int id) { 
            AccountCode = id;
        }

        public int AccountCode { get; private set; }
    }
}