using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Tarefas;

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
            if (model.IdObjetivo < 0 || model.IdProjeto < 0)
                return BadRequest("Objetivo ou Projeto não expecificado.");

            Tarefa tarefa = _mapper.Map<Tarefa>(model);
            Tarefa existente = _contexto.Tarefas.Find(model.Id);

            if (existente != null)
                _contexto.Tarefas.Update(tarefa);
            else
                _contexto.Tarefas.Add(tarefa);

            await _contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IHttpActionResult> DeletarTarefa ([FromUri]int id)
        {
            Tarefa tarefa = _contexto.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            if(tarefa.SubTarefas.Count > 0)
            {
                foreach(Tarefa subTarefa in tarefa.SubTarefas)
                {
                    _contexto.Tarefas.Remove(subTarefa);
                }
            }

            _contexto.Tarefas.Remove(tarefa);

            await _contexto.SaveChangesAsync();

            return Ok();
        }
    }
}
