using Core;
using Core.Domain;

//Camada responsável por gravar/atualzar/deletar ifnromações no banco, e responsável por acessar apis
namespace Infra
{
    public class ClientRepository : IClientRepository
    {
        //_context Client 
        public async Task<bool> SaveAsync(Client client)
        {
            //await _context.SaveAsync(client);

            //return true;
        }
    }
}