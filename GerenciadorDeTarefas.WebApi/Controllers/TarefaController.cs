using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using GerenciadorDeTarefas.Domain.Contexto;

using Microsoft.AspNetCore.Http;

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
        public async Task<IHttpActionResult> Buscar (int id)
        {
            return Ok();
        }
    }
}
