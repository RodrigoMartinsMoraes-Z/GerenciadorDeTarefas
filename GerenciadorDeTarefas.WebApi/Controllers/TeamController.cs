using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Projetos;
using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/team")]
    public class TeamController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public TeamController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(EquipeModel), 200)]
        public async Task<ActionResult> GetTeam(int id)
        {
            Equipe equipe = _contexto.Equipes.Find(id);

            if (equipe == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<EquipeModel>(equipe));
        }

        [HttpGet, Route("{teamId}/users")]
        [ProducesResponseType(typeof(List<UsuarioModel>), 200)]
        public async Task<ActionResult> GetTeamUsers(int teamId)
        {
            List<Usuario> users = _contexto.EquipeUsuario
                .Where(eu => eu.IdEquipe == teamId)
                .Select(e => e.Usuario)
                .ToList();

            List<UsuarioModel> models = new List<UsuarioModel>();

            foreach (var user in users)
            {
                var model = _mapper.Map<UsuarioModel>(user);

                models.Add(model);
            }

            await Task.CompletedTask;
            return Ok(models);
        }

        [HttpGet, Route("{teamId}/projects")]
        [ProducesResponseType(typeof(List<ProjetoModel>), 200)]
        public async Task<ActionResult> TeamProjects(int teamId)
        {
            Equipe team = _contexto.Equipes.Find(teamId);

            UserHasTeam(teamId);

            IQueryable<Domain.Projetos.Projeto> projects = _contexto.Projetos.Where(p => p.IdEquipe == teamId);

            List<ProjetoModel> models = new List<ProjetoModel>();

            foreach (var project in projects)
            {
                var model = _mapper.Map<ProjetoModel>(project);
                models.Add(model);
            }

            await Task.CompletedTask;

            return Ok(models);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EquipeModel>), 200)]
        public async Task<ActionResult> UserTeams()
        {
            IQueryable<Equipe> teams = _contexto.EquipeUsuario.Where(eu => eu.IdUsuario == _usuarioLogado.IdPessoa).Select(e => e.Equipe);

            List<EquipeModel> models = new List<EquipeModel>();

            foreach (var team in teams)
                models.Add(_mapper.Map<EquipeModel>(team));

            await Task.CompletedTask;
            return Ok(models);
        }

        [HttpPut, Route("{teamId}/add-user")]
        public async Task<ActionResult> AddUser([FromBody] UsuarioModel usuarioModel, int teamId)
        {
            Role role = _contexto.EquipeUsuario.FirstOrDefault(eu => eu.IdEquipe == teamId && eu.IdUsuario == _usuarioLogado.IdPessoa).PermissaoUsuario;

            if (role == Role.User)
                return Unauthorized();

            Equipe team = _contexto.Equipes.Find(teamId);
            Usuario user = _contexto.Usuarios.FirstOrDefault(u => u.Login == usuarioModel.Login);

            UserHasTeam(teamId);


            EquipeUsuario userTeam = new()
            {
                Equipe = team,
                IdEquipe = team.Id,
                IdUsuario = user.IdPessoa,
                Usuario = user,
                PermissaoUsuario = (Role)usuarioModel.Permissao
            };

            team.Usuarios.Add(userTeam);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeam([FromBody] EquipeModel teamModel)
        {
            Equipe team = _contexto.Equipes.FirstOrDefault(e => e.Id == teamModel.Id);

            if (team == null)
            {
                return NotFound();
            }

            if (UserHasTeam(team.Id))
                return Unauthorized();

            var role = _contexto.EquipeUsuario.FirstOrDefault(eu => eu.IdEquipe == teamModel.Id && eu.IdUsuario == _usuarioLogado.IdPessoa).PermissaoUsuario;

            if (role == Role.User)
                return Unauthorized();

            team.Nome = teamModel.Nome;

            _contexto.Update(team);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> NewTeam([FromBody] EquipeModel teamModel)
        {

            Equipe team = _mapper.Map<Equipe>(teamModel);

            await team.Validate();

            Usuario user = _contexto.Usuarios.FirstOrDefault(u => u.Login == User.Identity.Name);

            _contexto.Add(team);
            _contexto.SaveChanges();

            EquipeUsuario userTeam = new EquipeUsuario
            {
                Usuario = user,
                IdUsuario = user.IdPessoa,
                Equipe = team,
                IdEquipe = team.Id,
                PermissaoUsuario = Role.Administrator
            };

            user.Equipes.Add(userTeam);
            team.Usuarios.Add(userTeam);

            _contexto.Update(user);
            _contexto.Update(team);
            _contexto.SaveChanges();

            return Ok();
        }

        [HttpDelete, Route("{teamId}")]
        public async Task<ActionResult> DeleteTeam(int teamId)
        {
            Equipe team = _contexto.Equipes.Find(teamId);

            if (team == null)
                return NotFound();

            var role = _contexto.EquipeUsuario.FirstOrDefault(eu => eu.IdEquipe == teamId && eu.IdUsuario == _usuarioLogado.IdPessoa).PermissaoUsuario;

            if (!UserHasTeam(teamId) || role != Role.Administrator)
                return Unauthorized();

            _contexto.Remove(team);
            _contexto.SaveChanges();
            await Task.CompletedTask;
            return Ok();
        }

        [HttpDelete, Route("{teamId}/remove-user/{userId}")]
        public async Task<ActionResult> RemoveUser(int teamId, int userId)
        {
            var temUsers = _contexto.EquipeUsuario.FirstOrDefault(x => x.IdEquipe == teamId && x.IdUsuario == userId);

            if (temUsers == null)
                return NotFound();

            _contexto.Remove(temUsers);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        private bool UserHasTeam(int teamId)
        {
            return _contexto.EquipeUsuario.Any(e => e.IdEquipe == teamId && e.IdUsuario == _usuarioLogado.IdPessoa);
        }

    }
}
