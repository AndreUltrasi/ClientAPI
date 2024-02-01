using Core.Domain;
using Core.UseCases.DeleteClient.Boundaries;
using Core.UseCases.GetClient.Boundaries;
using Core.UseCases.UpsertClient.Boundaries;
using Microsoft.AspNetCore.Mvc;

//� respons�vel por retornar os status Code dependem do que foi recebido pelo Core
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
            //Os par�metros de consulta s�o utilizados com o objetivo de enviar informa��es extras numa solicita��o HTTP.
            //Se estes par�metros forem inseridos incorretamente ou se houver falhas na sua estrutura, o servidor pode retornar com o erro 400.
            //Outro fator importante � ressaltar que o HTTP error 400 n�o indica problema com o servidor em si, mas sim a solicita��o enviado pelo usu�rio.
            //O erro acontece porque algo n�o est� de acordo com o protocolo HTTP.
            //Exemplo: foi enviado algum parametro incorreto, como por exemplo id zero ou uma letra inv�s de numero (fazer teste pelo postman)

            //StatusCode 500 (Internal Server Error)
            //O erro 500 significa que h� um problema com alguma das bases que faz um site rodar.
            //Esse erro pode ser, basicamente, no servidor que mant�m as p�ginas no ar ou na comunica��o com o sistema de arquivos, que fornece a infraestrutura para o site.
            //Quando h� alguns desses problemas, automaticamente todas as p�ginas do site ficar�o indispon�veis.
            //� por isso que h� uma completa inatividade, com a indisponibilidade podendo ser verificada de forma ampla, mesmo que o usu�rio tente acessar diferentes links do site.
            //Exemplo: A conex�o com o banco de dados est� fora, a aplica��o est� com algum problema ou houve qualquer exception no codigo � retornado 500

            //Status Code 200 (OK)
            //A solicita��o foi bem-sucedida.O significado do resultado de "sucesso" depende do m�todo HTTP:
            //GET: O recurso foi obtido e transmitido no corpo da mensagem.
            //PUT ou POST: O recurso que descreve o resultado da a��o � transmitido no corpo da mensagem.
            //Exemplo: Quando � encontrado algum cliente � retornado 200 com o cliente no conteudo da mensagem

            //Status Code 204 (No Content)
            //N�o h� conte�do para enviar para esta solicita��o, mas os cabe�alhos podem ser �teis.
            //O agente do usu�rio pode atualizar seus cabe�alhos em cache para este recurso com os novos.
            //Exemplo: Foi solicitado um id de cliente que n�o existe, assim o response vem nulo

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