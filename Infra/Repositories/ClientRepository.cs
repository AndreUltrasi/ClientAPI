using Core;
using Core.Domain;
using Core.Domain.Enums;
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

        public async Task<Client> GetAsync(int accountCode)
        {
            var clientModel = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(s => s.AccountCode == accountCode);

            if (clientModel == null)
            {
                return null!;
            }

            var client = clientModel.MapToDomain();

            return client;
        }

        public async Task<UpsertStatus> UpsertAsync(Client client)
        {
            var clientStatus = UpsertStatus.Inserted;
            if (await _context.Clients.AnyAsync(s => s.AccountCode == client.AccountCode))
            {
                clientStatus = UpsertStatus.Updated;
            };

            var clientModel = client.MapToModel();

            _context.Clients.Update(clientModel);

            await _context.SaveChangesAsync();

            return clientStatus;
        }

        public async Task<DeleteStatus> DeleteAsync(int accountCode)
        {
            var clientModel = _context.Clients.FirstOrDefault(s => s.AccountCode == accountCode);

            if(clientModel == null)
                throw new InvalidOperationException("Não Existe Cliente Com Este AccountCode");

            if (!clientModel.Active)
                return DeleteStatus.AlreadyDisabled;

            clientModel.Active = false;

            _context.Clients.Update(clientModel);

            await _context.SaveChangesAsync();

            return DeleteStatus.Disabled;
        }
    }
}