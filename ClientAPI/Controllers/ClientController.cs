using Core.Domain;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;

//É responsável por retornar os status Code dependem do que foi recebido pelo Core
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
        public async Task<IActionResult> GetClient(int accountCode)
        {
            var input = new GetClientInput(accountCode);
            var response = await _getClient.Handle(input);
            if(!response.IsValid)
                return BadRequest(response.ErrorMessage);

            if(response.Result == null)
                return NoContent();

            return Ok(response.Result);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int accountCode)
        {
            var input = new DeleteClientInput(accountCode);
            var response = await _deleteClient.Handle(input);
            if (!response.IsValid)
                return BadRequest(response.ErrorMessage);

            if (response.Result == null)
                return NoContent();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpsertClient(ClientViewModel clientViewModel)
        {
            var input = new UpsertClientInput(clientViewModel);
            var response = await _upsertClient.Handle(input);
            if (!response.IsValid)
                return BadRequest(response.ErrorMessage);

            return Ok(response.Result);
        }
    }
}