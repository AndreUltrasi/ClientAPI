namespace Core.UseCases.GetClient.Boundaries
{
    public class GetClientInput
    {
        public GetClientInput(string name) { 
            Name = name;
        }

        public string Name { get; private set; }
        // Colocar propriedades, cep, PersonType, Age, Gender, document (cpf or cnpj)
        //Active não é enviado
    }
}