using Core.Domain;

namespace Core
{
    public interface IClientRepository
    {
        Task<Client?> GetAsync(int id);
        Task UpsertAsync(Client client);
    }
}