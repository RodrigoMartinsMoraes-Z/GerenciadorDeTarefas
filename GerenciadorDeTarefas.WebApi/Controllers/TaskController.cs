using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Tasks;

using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/task")]
    public class TaskController : BaseApiController
    {
        private readonly IContext _contexto;
        private readonly IMapper _mapper;

        public TaskController(IContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(TarefaModel), 200)]
        public async Task<ActionResult> GetTask(int id)
        {
            Domain.Tasks.Task task = _contexto.Tasks.Find(id);

            if (task == null)
                return NotFound();

            await System.Threading.Tasks.Task.CompletedTask;

            return Ok(_mapper.Map<TarefaModel>(task));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask(TarefaModel model)
        {
            if (model.IdObjetivo < 0 || model.IdProjeto < 0)
                return BadRequest("Objective or Project is required!.");

            Domain.Tasks.Task task = _mapper.Map<Domain.Tasks.Task>(model);
            Domain.Tasks.Task exist = _contexto.Tasks.Find(model.Id);

            if (exist != null)
                _contexto.Tasks.Update(task);
            else
                _contexto.Tasks.Add(task);

            _contexto.SaveChanges();

            await System.Threading.Tasks.Task.CompletedTask;

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            Domain.Tasks.Task task = _contexto.Tasks.Find(id);

            if (task == null)
                return NotFound();

            task.SubTask = _contexto.Tasks.Where(t => t.IdTarefaPrincipal == task.Id).ToList();

            if (task.SubTask.Count > 0)
            {
                foreach (Domain.Tasks.Task subTask in task.SubTask)
                {
                    _contexto.Tasks.Remove(subTask);
                }
            }

            _contexto.Tasks.Remove(task);

            _contexto.SaveChanges();

            await System.Threading.Tasks.Task.CompletedTask;
            return Ok();
        }
    }
}
