using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/teste")]
    public class TesteController : BaseApiController
    {
        [HttpGet, Route("teste")]
        public async Task<ActionResult> Teste()
        {
            await Task.CompletedTask;

            return Ok("teste");
        }
    }
}
