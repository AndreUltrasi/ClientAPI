using Core.UseCases.GetClient.Boundaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediatr;

        public ClientController(ILogger<ClientController> logger,
                                IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetClients(GetClientInput input)
        {
            await _mediatr.Send(input);
        }

        [HttpPost]
        public IEnumerable<WeatherForecast> DeleteClient(DeleteClientInput input)
        {
            await _mediatr.Send(input);
        }

        [HttpPost]
        public async IEnumerable<WeatherForecast> UpsertClient(UpsertClientInput input)
        {
            await _mediatr.Upsert(input);
        }
    }
}