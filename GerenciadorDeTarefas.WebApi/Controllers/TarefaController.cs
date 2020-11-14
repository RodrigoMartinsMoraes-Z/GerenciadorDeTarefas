using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Tarefas;

using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [RoutePrefix("api/tarefa")]
    public class TarefaController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public TarefaController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Buscar(int id)
        {
            Tarefa tarefa = _contexto.Tarefas.Find(id);

            await Task.CompletedTask;

            return Ok(_mapper.Map<TarefaModel>(tarefa));
        }

        [HttpPut, Route]
        public async Task<IHttpActionResult> AtualizarTarefa(TarefaModel model)
        {
            if (model.Objetivo == null || model.Projeto == null)
                return BadRequest("Objetivo ou Projeto não expecificado.");

            Tarefa existente = null;

            if (model.Objetivo != null)
            {
                Objetivo objetivo = _contexto.Objetivos.Find(model.Objetivo.Id);

                if(model.Id > 0)
                {
                    existente = _contexto.Tarefas.Find(model.Id);
                }

                if(existente != null)
                {

                }
                
            }

            return Ok();
        }
    }
}
