using Polly;
using Polly.Retry;
using Refit;
using System.Net;

namespace Core.Domain.Interfaces.Https
{
    public class AddressService : IAddressService
    {
        private readonly IHttpAddressService _httpAddressService;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly IEnumerable<HttpStatusCode> errorsStatusCode = new List<HttpStatusCode>() { HttpStatusCode.GatewayTimeout, HttpStatusCode.RequestTimeout, HttpStatusCode.BadGateway, HttpStatusCode.ServiceUnavailable };

        public AddressService(IHttpAddressService httpAddressService) { 
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
            catch (Exception)
            {
                output.AddErrorMessage("Error on getting Address From Address API");
                throw;
            }
        }
    }
}