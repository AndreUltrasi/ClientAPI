using Core.Extensions;
using Core.Interfaces.UseCases;
using Core.UseCases.GetClient.Boundaries;
using Microsoft.Extensions.Logging;

//Core lida com regras de negocio
namespace Core.UseCases.GetClient
{
    public class GetClient : IGetClient
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<GetClient> _logger;

        public GetClient(IClientRepository clientRepository,
                         ILogger<GetClient> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<Output> Handle(GetClientInput input)
        {
            _logger.LogInformation("[{Class}] | [{Method}] | UseCase Started | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}",
                nameof(GetClient), Helpers.GetCallerName(), input.CorrelationId, input.AccountCode);

            var output = new Output();

            var validationResult = new GetClientInputValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    _logger.LogError("[{Class}] | [{Method}] | Validation Error | PropertyName: {PropertyName}, ErrorCode: {ErrorCode}, CorrelationId: {CorrelationId}, AccountCode: {AccountCode}",
                        nameof(GetClient), Helpers.GetCallerName(), error.PropertyName, error.ErrorCode, input.CorrelationId, input.AccountCode);

                    output.AddErrorMessage(error.ErrorMessage);
                }
                return output;
            }

            var client = await _clientRepository.GetAsync(input.AccountCode);

            _logger.LogInformation("[{Class}] | [{Method}] | Client Was Got Successfully | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}, Name: {Name}",
                    nameof(GetClient), Helpers.GetCallerName(), input.CorrelationId, client.AccountCode, client.Name);

            output.AddResult(client!);
            return output;
        }
    }
}