using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Projetos;
using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Users;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/team")]
    public class TeamController : BaseApiController
    {
        private readonly IContext _contexto;
        private readonly IMapper _mapper;

        public TeamController(IContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(EquipeModel), 200)]
        public async Task<ActionResult> GetTeam(int id)
        {
            Team equipe = _contexto.Teams.Find(id);

            if (equipe == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<EquipeModel>(equipe));
        }

        [HttpGet, Route("{teamId}/users")]
        [ProducesResponseType(typeof(List<UsuarioModel>), 200)]
        public async Task<ActionResult> GetTeamUsers(int teamId)
        {
            List<Domain.ManyToMany.User> users = _contexto.TeamUser
                .Where((object eu) => eu.IdEquipe == teamId)
                .Select((object e) => e.Usuario)
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
            Team team = _contexto.Teams.Find(teamId);

            UserHasTeam(teamId);

            IQueryable<Project> projects = _contexto.Projects.Where(p => p.IdEquipe == teamId);

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
            IQueryable<Team> teams = _contexto.TeamUser.Where(eu => eu.IdUsuario == _usuarioLogado.IdPessoa).Select(e => e.Equipe);

            List<EquipeModel> models = new List<EquipeModel>();

            foreach (var team in teams)
                models.Add(_mapper.Map<EquipeModel>(team));

            await Task.CompletedTask;
            return Ok(models);
        }

        [HttpPut, Route("{teamId}/add-user")]
        public async Task<ActionResult> AddUser([FromBody] UsuarioModel usuarioModel, int teamId)
        {
            Role role = _contexto.TeamUser.FirstOrDefault(eu => eu.IdEquipe == teamId && eu.IdUsuario == _usuarioLogado.IdPessoa).PermissaoUsuario;

            if (role == Role.User)
                return Unauthorized();

            Team team = _contexto.Teams.Find(teamId);
            Domain.ManyToMany.User user = _contexto.Users.FirstOrDefault((object u) => u.Login == usuarioModel.Login);

            UserHasTeam(teamId);


            TeamUser userTeam = new()
            {
                Team = team,
                TeamId = team.Id,
                UserId = user.IdPessoa,
                User = user,
                UserRole = (Role)usuarioModel.Permissao
            };

            team.TeamUsers.Add(userTeam);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeam([FromBody] EquipeModel teamModel)
        {
            Team team = _contexto.Teams.FirstOrDefault(e => e.Id == teamModel.Id);

            if (team == null)
            {
                return NotFound();
            }

            if (UserHasTeam(team.Id))
                return Unauthorized();

            var role = _contexto.TeamUser.FirstOrDefault(eu => eu.IdEquipe == teamModel.Id && eu.IdUsuario == _usuarioLogado.IdPessoa).PermissaoUsuario;

            if (role == Role.User)
                return Unauthorized();

            team.Name = teamModel.Nome;

            _contexto.Update(team);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> NewTeam([FromBody] EquipeModel teamModel)
        {

            Team team = _mapper.Map<Team>(teamModel);

            await team.Validate();

            Domain.ManyToMany.User user = _contexto.Users.FirstOrDefault((object u) => u.Login == User.Identity.Name);

            _contexto.Add(team);
            _contexto.SaveChanges();

            TeamUser userTeam = new TeamUser
            {
                User = user,
                UserId = user.IdPessoa,
                Team = team,
                TeamId = team.Id,
                UserRole = Role.Administrator
            };

            user.Equipes.Add(userTeam);
            team.TeamUsers.Add(userTeam);

            _contexto.Update(user);
            _contexto.Update(team);
            _contexto.SaveChanges();

            return Ok();
        }

        [HttpDelete, Route("{teamId}")]
        public async Task<ActionResult> DeleteTeam(int teamId)
        {
            Team team = _contexto.Teams.Find(teamId);

            if (team == null)
                return NotFound();

            var role = _contexto.TeamUser.FirstOrDefault(eu => eu.IdEquipe == teamId && eu.IdUsuario == _usuarioLogado.IdPessoa).PermissaoUsuario;

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
            var temUsers = _contexto.TeamUser.FirstOrDefault(x => x.IdEquipe == teamId && x.IdUsuario == userId);

            if (temUsers == null)
                return NotFound();

            _contexto.Remove(temUsers);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        private bool UserHasTeam(int teamId)
        {
            return _contexto.TeamUser.Any(e => e.IdEquipe == teamId && e.IdUsuario == _usuarioLogado.IdPessoa);
        }

    }
}
