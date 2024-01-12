namespace Core.UseCases.GetClient.Boundaries
{
    public class DeleteClientInput
    {
        public DeleteClientInput(string name) { 
            Name = name;
        }

        public string Name { get; set; }
    }
}