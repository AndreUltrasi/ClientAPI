using Core.Domain.Enums;
using Core.Domain.Interfaces.Https;
using Core.Dto;
using Core.Extensions;
using Core.Interfaces.UseCases;
using Core.Mappers;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
using Microsoft.Extensions.Logging;

namespace Core.UseCases.UpsertClient
{
    public class UpsertClient : IUpsertClient
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAddressService _addressService;
        private readonly ILogger<UpsertClient> _logger;

        public UpsertClient(IClientRepository clientRepository,
                            IAddressService addressService,
                            ILogger<UpsertClient> logger)
        {
            _clientRepository = clientRepository;
            _addressService = addressService;
            _logger = logger;
        }

        public async Task<Output> Handle(UpsertClientInput input)
        {
            _logger.LogInformation("[{Class}] | [{Method}] | UseCase Started | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}, Name: {Name}",
                nameof(UpsertClient), Helpers.GetCallerName(), input.CorrelationId, input.Client.AccountCode, input.Client.Name);

            var output = new Output();

            var validationResult = new UpsertClientInputValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _logger.LogError("[{Class}] | [{Method}] | Validation Error | PropertyName: {PropertyName}, ErrorCode: {ErrorCode}, CorrelationId: {CorrelationId}, AccountCode: {AccountCode}, Name: {Name}",
                        nameof(UpsertClient), Helpers.GetCallerName(), error.PropertyName, error.ErrorCode, input.CorrelationId, input.Client.AccountCode, input.Client.Name);

                    output.AddErrorMessage(error.ErrorMessage);
                }
                return output;
            }

            var client = input.Client.MapToDomain();

            var response = await _addressService.GetAddressAsync(client.Cep);
            var addressDto = (AddressDto)response.Result;

            _logger.LogInformation("[{Class}] | [{Method}] | Address Got Successfully From Api | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}, Name: {Name}, City: {City}, Uf: {Uf}, Neighbourhood: {Neighbourhood}",
                nameof(UpsertClient), Helpers.GetCallerName(), input.CorrelationId, input.Client.AccountCode, input.Client.Name, addressDto.City, addressDto.Uf, addressDto.Neighbourhood);


            client.AddAddressProperties(addressDto);

            var upsertStatus = await _clientRepository.UpsertAsync(client);

            if (upsertStatus == UpsertStatus.Updated)
            {
                _logger.LogInformation("[{Class}] | [{Method}] | Client Was Updated Successfully | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}, Name: {Name}",
                    nameof(UpsertClient), Helpers.GetCallerName(), input.CorrelationId, input.Client.AccountCode, input.Client.Name);
                output.AddResult(client);
                return output;
            }

            _logger.LogInformation("[{Class}] | [{Method}] | Client Was Inserted Successfully | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}, Name: {Name}",
                    nameof(UpsertClient), Helpers.GetCallerName(), input.CorrelationId, input.Client.AccountCode, input.Client.Name);

            output.AddResult(client);
            return output;
        }
    }
}