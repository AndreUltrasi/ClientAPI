namespace Core.UseCases.DeleteClient.Boundaries
{
    public class DeleteClientInput
    {
        public DeleteClientInput(string name) { 
            Name = name;
        }

        public string Name { get; set; }
    }
}