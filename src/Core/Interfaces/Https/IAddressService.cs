
namespace Core.Domain.Interfaces.Https
{
    public interface IAddressService
    {
        Task<Output> GetAddressAsync(string cep);
    }
}