using Core.Domain.Enums;
using Core.Extensions;
using Core.Interfaces.UseCases;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Microsoft.Extensions.Logging;

namespace Core.UseCases.DeleteClient
{
    public class DeleteClient : IDeleteClient
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<DeleteClient> _logger;
        public DeleteClient(IClientRepository clientRepository,
                            ILogger<DeleteClient> logger) {
            _clientRepository = clientRepository;
            _logger = logger;
        }
        public async Task<Output> Handle(DeleteClientInput input)
        {
            _logger.LogInformation("[{Class}] | [{Method}] | UseCase Started | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}",
                nameof(DeleteClient), Helpers.GetCallerName(), input.CorrelationId, input.AccountCode);

            var output = new Output();

            var validationResult = new DeleteClientInputValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _logger.LogError("[{Class}] | [{Method}] | Validation Error | PropertyName: {PropertyName}, ErrorCode: {ErrorCode}, CorrelationId: {CorrelationId}, AccountCode: {AccountCode}",
                        nameof(DeleteClient), Helpers.GetCallerName(), error.PropertyName, error.ErrorCode, input.CorrelationId, input.AccountCode);

                    output.AddErrorMessage(error.ErrorMessage);
                }
                return output;
            }

            var deleteStatus = await _clientRepository.DeleteAsync(input.AccountCode);

            if (deleteStatus == DeleteStatus.AlreadyDisabled)
            {
                _logger.LogInformation("[{Class}] | [{Method}] | Client Was Already Disabled | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}",
                    nameof(DeleteClient), Helpers.GetCallerName(), input.CorrelationId, input.AccountCode);
            }

            _logger.LogInformation("[{Class}] | [{Method}] | Client Was Disabled Successfully | CorrelationId: {CorrelationId}, AccountCode: {AccountCode}",
                    nameof(DeleteClient), Helpers.GetCallerName(), input.CorrelationId, input.AccountCode);

            return output;
        }
    }
}