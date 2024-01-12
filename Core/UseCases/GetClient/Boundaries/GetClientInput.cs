namespace Core.UseCases.GetClient.Boundaries
{
    public class GetClientInput
    {
        public GetClientInput(string name) { 
            Name = name;
        }

        public string Name { get; private set; }
    }
}