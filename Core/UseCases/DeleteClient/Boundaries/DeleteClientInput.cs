namespace Core.UseCases.DeleteClient.Boundaries
{
    public class DeleteClientInput
    {
        public DeleteClientInput(int id) { 
            Id = id;
        }

        public int Id { get; set; }
    }
}