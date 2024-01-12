namespace Core.UseCases.GetClient.Boundaries
{
    public class UpsertClientInput
    {
        public UpsertClientInput(string name) { 
            Name = name;
        }

        public string Name { private get; set; }
    }
}