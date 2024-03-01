using Core.Dto;
using Refit;

namespace Core.Domain.Interfaces.Https
{
    public interface IHttpAddressService
    {
        [Get("/ws/{cep}/json/")]
        Task<AddressDto> GetAddress(string cep);
    }
}