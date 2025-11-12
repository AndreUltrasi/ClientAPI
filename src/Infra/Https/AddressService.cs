using Core.Extensions;
using Core.UseCases.UpsertClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Refit;
using System.Net;

namespace Core.Domain.Interfaces.Https
{
    public class AddressService : IAddressService
    {
        private readonly ILogger<AddressService> _logger;
        private readonly IHttpAddressService _httpAddressService;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly IEnumerable<HttpStatusCode> errorsStatusCode = new List<HttpStatusCode>() { HttpStatusCode.GatewayTimeout, HttpStatusCode.RequestTimeout, HttpStatusCode.BadGateway, HttpStatusCode.ServiceUnavailable };

        public AddressService(IHttpAddressService httpAddressService,
                              ILogger<AddressService> logger) {
            _logger = logger;
            _httpAddressService = httpAddressService;
            _retryPolicy = Policy
                .Handle<ApiException>(e => errorsStatusCode.Contains(e.StatusCode))
                .WaitAndRetryAsync(retryCount: 3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }


        public async Task<Output> GetAddressAsync(string cep)
        {
            var output = new Output();
            try
            {
                var addressResponse = await _retryPolicy.ExecuteAsync(async () =>
                    {
                        return await _httpAddressService.GetAddress(cep);
                    });

                output.AddResult(addressResponse);
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Class}] | [{Method}] | Error On Getting Address From Address API | ErrorMessage: {ErrorMessage}, InnerException: {InnerException}, Cep: {Cep}",
                    nameof(UpsertClient), Helpers.GetCallerName(), ex.Message, ex.InnerException, cep);
                output.AddErrorMessage("Error on getting Address From Address API");
                throw;
            }
        }
    }
}