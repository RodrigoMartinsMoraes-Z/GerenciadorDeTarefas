using GerenciadorDeTarefas.Domain.Contexto;

using System.Threading.Tasks;
using System.Web.Http;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [RoutePrefix("api/tarefa")]
    public class TarefaController : ApiController
    {
        public readonly IContextoDeDados _contexto;

        public TarefaController(IContextoDeDados contexto)
        {
            _contexto = contexto;
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Buscar(int id)
        {
            return Ok();
        }
    }
}
