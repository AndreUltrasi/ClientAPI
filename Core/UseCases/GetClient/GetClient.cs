using Core.Domain;
using Core.UseCases.GetClient.Boundaries;

//Core lida com regras de negocio
namespace Core.UseCases.GetClient
{
    public class GetClient : IGetClient
    {
        public async Task<Client> Handle(GetClientInput input)
        {
            //Consultar api de cep, e encontrar endereço, complemente, numero, cidade e estado
            //mapear propriedades e criar um objeto Client com todas as informações
            // dadosCep = consultarApiCep();

            //Verificar se os dados retornados na api cecp estão ok, se não estiverem retorna erro

            //TODO:
            //Validar se o PersonType for "Person", e o documento não tiver 11 caracteres, retornar erro
            //Validar se o PersonType for "Company", e o documento não tiver 14 caracteres, retornar erro

            //var client = MapToInfra(input);

            //Salvar no banco Client
            //var saved = await _repository.SaveAsync(client);
            //verifica se foi salvo
        }


        //public Client MapToInfra(GetClientInput input, dadosCep)
        //{
        //    return new Client()
        //    {
        //        Name = input.Name,
        //        //Age = input.Age
        //        //Gender = input.Gender
        //        Endereco = dadosCep.Endereco,
        //        Numero = dadosCep.Numero
        //    };
        //}
    }
}