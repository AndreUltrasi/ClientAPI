using Core.Domain;
using Core.Domain.Interfaces.Https;
using Core.Dto;
using Core.Mappers;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;

namespace Core.UseCases.UpsertClient
{
    public class UpsertClient : IUpsertClient
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAddressService _addressService;

        public UpsertClient(IClientRepository clientRepository,
                            IAddressService addressService)
        {
            _clientRepository = clientRepository;
            _addressService = addressService;
        }

        public async Task<Output> Handle(UpsertClientInput input)
        {
            var output = new Output();

            var validationResult = new UpsertClientInputValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    output.AddErrorMessage(error.ErrorMessage);
                }
                return output;
            }

            var client = input.Client.MapToDomain();

            var response = await _addressService.GetAddressAsync(client.Cep);
            var addressDto = (AddressDto)response.Result;

            client.AddAddressProperties(addressDto);

            await _clientRepository.UpsertAsync(client);
            output.AddResult(client);
            return output;
        }
    }
}