using Core.Domain;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
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
            //Status Code 400 (Bad Request)
            //Os parâmetros de consulta são utilizados com o objetivo de enviar informações extras numa solicitação HTTP.
            //Se estes parâmetros forem inseridos incorretamente ou se houver falhas na sua estrutura, o servidor pode retornar com o erro 400.
            //Outro fator importante é ressaltar que o HTTP error 400 não indica problema com o servidor em si, mas sim a solicitação enviado pelo usuário.
            //O erro acontece porque algo não está de acordo com o protocolo HTTP.
            //Exemplo: foi enviado algum parametro incorreto, como por exemplo id zero ou uma letra invés de numero (fazer teste pelo postman)

            //StatusCode 500 (Internal Server Error)
            //O erro 500 significa que há um problema com alguma das bases que faz um site rodar.
            //Esse erro pode ser, basicamente, no servidor que mantém as páginas no ar ou na comunicação com o sistema de arquivos, que fornece a infraestrutura para o site.
            //Quando há alguns desses problemas, automaticamente todas as páginas do site ficarão indisponíveis.
            //É por isso que há uma completa inatividade, com a indisponibilidade podendo ser verificada de forma ampla, mesmo que o usuário tente acessar diferentes links do site.
            //Exemplo: A conexão com o banco de dados está fora, a aplicação está com algum problema ou houve qualquer exception no codigo é retornado 500

            //Status Code 200 (OK)
            //A solicitação foi bem-sucedida.O significado do resultado de "sucesso" depende do método HTTP:
            //GET: O recurso foi obtido e transmitido no corpo da mensagem.
            //PUT ou POST: O recurso que descreve o resultado da ação é transmitido no corpo da mensagem.
            //Exemplo: Quando é encontrado algum cliente é retornado 200 com o cliente no conteudo da mensagem

            //Status Code 204 (No Content)
            //Não há conteúdo para enviar para esta solicitação, mas os cabeçalhos podem ser úteis.
            //O agente do usuário pode atualizar seus cabeçalhos em cache para este recurso com os novos.
            //Exemplo: Foi solicitado um id de cliente que não existe, assim o response vem nulo

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var input = new DeleteClientInput(id);
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