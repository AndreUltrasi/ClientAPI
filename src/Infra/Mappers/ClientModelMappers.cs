using Core.Domain;

//Camada responsável por gravar/atualizar/deletar informações no banco, e responsável por acessar apis
namespace Infra.Mappers
{
    public static class ClientModelMappers
    {
        public static Client MapToDomain(this ClientModel clientModel)
        {
            var client = new Client(clientModel.AccountCode, clientModel.Name, clientModel.PersonType, clientModel.Age, clientModel.Gender, clientModel.Adress, clientModel.Number,
                                    clientModel.Complement, clientModel.City, clientModel.Country, clientModel.Cep, clientModel.Neighbourhood, clientModel.Uf);

            return client;
        }

        public static ClientModel MapToModel(this Client client)
        {
            var clientModel = new ClientModel()
            {
                Active = client.Active,
                AccountCode = client.AccountCode,
                Adress = client.Address,
                Age = client.Age,
                Cep = client.Cep,
                City = client.City,
                Complement = client.Complement,
                Country = client.Country,
                Gender = client.Gender,
                Name = client.Name,
                Number = client.Number,
                PersonType = client.PersonType,
                Neighbourhood = client.Neighbourhood,
                Uf= client.Uf
            };

            return clientModel;
        }
    }
}