using Core;
using Core.Domain;
using Infra.Mappers;
using Microsoft.EntityFrameworkCore;

//Camada responsável por gravar/atualizar/deletar informações no banco, e responsável por acessar apis
namespace Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;

        public ClientRepository(ClientContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetAsync(int accountCode)
        {
            var clientModel = await _context.Clients.AsNoTracking().FirstAsync(s => s.AccountCode == accountCode);

            if (clientModel == null)
            {
                return null;
            }

            var client = clientModel.MapToDomain();

            return client;
        }

        public async Task UpsertAsync(Client client)
        {
            var clientModel = client.MapToModel();

            _context.Clients.Update(clientModel);

            await _context.SaveChangesAsync();
        }
    }
}