using Core.Domain;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IGetClient _getClient;
        private readonly IDeleteClient _deleteClient;
        private readonly IUpsertClient _upsertClient;

        public ClientController(IGetClient getClient,
                                IDeleteClient deleteClient,
                                IUpsertClient upsertClient)
        {
            _getClient = getClient;
            _deleteClient = deleteClient;
            _upsertClient = upsertClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetClient(string name)
        {
            var input = new GetClientInput(name);
            var response = await _getClient.Handle(input);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(string name)
        {
            var input = new DeleteClientInput(name);
            await _deleteClient.Handle(input);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpsertClient(Client client)
        {
            var input = new UpsertClientInput(client);
            await _upsertClient.Handle(input);
            return Ok();
        }
    }
}