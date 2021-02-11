using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Tarefas;

using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/tarefa")]
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
        public async Task<TarefaModel> Buscar(int id)
        {
            Tarefa tarefa = _contexto.Tarefas.Find(id);

            await Task.CompletedTask;

            return _mapper.Map<TarefaModel>(tarefa);
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarTarefa(TarefaModel model)
        {
            if (model.IdObjetivo < 0 || model.IdProjeto < 0)
                return BadRequest("Objetivo ou Projeto não expecificado.");

            Tarefa tarefa = _mapper.Map<Tarefa>(model);
            Tarefa existente = _contexto.Tarefas.Find(model.Id);

            if (existente != null)
                _contexto.Tarefas.Update(tarefa);
            else
                _contexto.Tarefas.Add(tarefa);

            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeletarTarefa(int id)
        {
            Tarefa tarefa = _contexto.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            tarefa.SubTarefas = _contexto.Tarefas.Where(t => t.IdTarefaPrincipal == tarefa.Id).ToList();

            if (tarefa.SubTarefas.Count > 0)
            {
                foreach (Tarefa subTarefa in tarefa.SubTarefas)
                {
                    _contexto.Tarefas.Remove(subTarefa);
                }
            }

            _contexto.Tarefas.Remove(tarefa);

            _contexto.SaveChanges();

            await Task.CompletedTask;
            return Ok();
        }
    }
}
