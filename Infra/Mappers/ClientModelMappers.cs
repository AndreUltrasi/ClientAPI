using Core.Domain;

//Camada responsável por gravar/atualizar/deletar informações no banco, e responsável por acessar apis
namespace Infra.Mappers
{
    public static class ClientModelMappers
    {
        public static Client MapToDomain(this ClientModel clientModel)
        {
            var client = new Client()
            {
                Active = clientModel.Active,
                AccountCode = clientModel.AccountCode,
                Adress = clientModel.Adress,
                Age = clientModel.Age,
                Cep = clientModel.Cep,
                City = clientModel.City,
                Complement = clientModel.Complement,
                Country = clientModel.Country,
                Gender = clientModel.Gender,
                Name = clientModel.Name,
                Number = clientModel.Number,
                PersonType = clientModel.PersonType,
                State = clientModel.State
            };

            return client;
        }
    }
}