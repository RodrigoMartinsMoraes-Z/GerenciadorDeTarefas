using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Tarefas;

using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/task")]
    public class TaskController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public TaskController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(TarefaModel), 200)]
        public async Task<ActionResult> GetTask(int id)
        {
            Tarefa task = _contexto.Tarefas.Find(id);

            if (task == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<TarefaModel>(task));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask(TarefaModel model)
        {
            if (model.IdObjetivo < 0 || model.IdProjeto < 0)
                return BadRequest("Objective or Project is required!.");

            Tarefa task = _mapper.Map<Tarefa>(model);
            Tarefa exist = _contexto.Tarefas.Find(model.Id);

            if (exist != null)
                _contexto.Tarefas.Update(task);
            else
                _contexto.Tarefas.Add(task);

            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            Tarefa task = _contexto.Tarefas.Find(id);

            if (task == null)
                return NotFound();

            task.SubTarefas = _contexto.Tarefas.Where(t => t.IdTarefaPrincipal == task.Id).ToList();

            if (task.SubTarefas.Count > 0)
            {
                foreach (Tarefa subTask in task.SubTarefas)
                {
                    _contexto.Tarefas.Remove(subTask);
                }
            }

            _contexto.Tarefas.Remove(task);

            _contexto.SaveChanges();

            await Task.CompletedTask;
            return Ok();
        }
    }
}
