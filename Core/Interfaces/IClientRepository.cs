using Core.Domain;
using Core.Domain.Enums;

namespace Core
{
    public interface IClientRepository
    {
        Task<Client> GetAsync(int id);
        Task<UpsertStatus> UpsertAsync(Client client);
        Task<DeleteStatus> DeleteAsync(int accountCode);
    }
}