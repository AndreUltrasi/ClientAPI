using Core;
using Core.Domain;
using Infra.Mappers;
using Microsoft.EntityFrameworkCore;

//Camada responsável por gravar/atualizar/deletar informações no banco, e responsável por acessar apis
namespace Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        //private readonly IClientContext _clientContext;
        public async Task<Client?> GetAsync(int id)
        {
            if (id == 2)
            {
                return null;
            }

            var clientModel = new ClientModel()
            {
                Active = true,
                Adress = "Avenida X",
                Age = 18,
                Cep = "07181000",
                City = "Guarulhos",
                Complement = "Bloco 3",
                Country = "Brasil",
                Gender = Gender.Male,
                Id = Guid.NewGuid(),
                Name = "Adriano",
                Number = "48",
                PersonType = PersonType.Person,
                State = "São Paulo"
            };

            var client = clientModel.MapToDomain();

            return client;
        }

        }

        public class DataContext : DbContext
    {

        public DbSet<ClientModel> ClientModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
            .UseSqlServer("Server=127.0.0.1,1433;Initial Catalog=DATABASE;User ID=sa;Password=SqlServer2019!;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientModel>();
        }
    }

    

}