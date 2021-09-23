using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Teams;
using GerenciadorDeTarefas.Common.Models.Users;
using GerenciadorDeTarefas.Domain.Context;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Teams;
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
        [ProducesResponseType(typeof(TeamModel), 200)]
        public async Task<ActionResult> GetTeam(int id)
        {
            Team equipe = _contexto.Teams.Find(id);

            if (equipe == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<TeamModel>(equipe));
        }

        [HttpGet, Route("{teamId}/users")]
        [ProducesResponseType(typeof(List<UserModel>), 200)]
        public async Task<ActionResult> GetTeamUsers(int teamId)
        {
            List<Domain.Users.User> users = _contexto.TeamUser
                .Where(eu => eu.TeamId == teamId)
                .Select(e => e.User)
                .ToList();

            List<UserModel> models = new();

            foreach (User user in users)
            {
                UserModel model = _mapper.Map<UserModel>(user);

                models.Add(model);
            }

            await Task.CompletedTask;
            return Ok(models);
        }

        [HttpGet, Route("{teamId}/projects")]
        [ProducesResponseType(typeof(List<ProjectModel>), 200)]
        public async Task<ActionResult> TeamProjects(int teamId)
        {
            Team team = _contexto.Teams.Find(teamId);

            UserHasTeam(teamId);

            IQueryable<Project> projects = _contexto.Projects.Where(p => p.IdEquipe == teamId);

            List<ProjectModel> models = new List<ProjectModel>();

            foreach (Project project in projects)
            {
                ProjectModel model = _mapper.Map<ProjectModel>(project);
                models.Add(model);
            }

            await Task.CompletedTask;

            return Ok(models);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TeamModel>), 200)]
        public async Task<ActionResult> UserTeams()
        {
            IQueryable<Team> teams = _contexto.TeamUser.Where(eu => eu.UserId == _usuarioLogado.PersonId).Select(e => e.Team);

            List<TeamModel> models = new();

            foreach (Team team in teams)
                models.Add(_mapper.Map<TeamModel>(team));

            await Task.CompletedTask;
            return Ok(models);
        }

        [HttpPut, Route("{teamId}/add-user")]
        public async Task<ActionResult> AddUser([FromBody] UserModel usuarioModel, int teamId)
        {
            Role role = _contexto.TeamUser.FirstOrDefault(eu => eu.TeamId == teamId && eu.UserId == _usuarioLogado.PersonId).UserRole;

            if (role == Role.User)
                return Unauthorized();

            Team team = _contexto.Teams.Find(teamId);
            Domain.Users.User user = _contexto.Users.FirstOrDefault(u => u.Login == usuarioModel.Login);

            UserHasTeam(teamId);


            TeamUser userTeam = new()
            {
                Team = team,
                TeamId = team.Id,
                UserId = user.PersonId,
                User = user,
                UserRole = (Role)usuarioModel.Permissao
            };

            team.TeamUsers.Add(userTeam);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeam([FromBody] TeamModel teamModel)
        {
            Team team = _contexto.Teams.FirstOrDefault(e => e.Id == teamModel.Id);

            if (team == null)
            {
                return NotFound();
            }

            if (UserHasTeam(team.Id))
                return Unauthorized();

            Role role = _contexto.TeamUser.FirstOrDefault(eu => eu.TeamId == teamModel.Id && eu.UserId == _usuarioLogado.PersonId).UserRole;

            if (role == Role.User)
                return Unauthorized();

            team.Name = teamModel.Name;

            _contexto.Update(team);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> NewTeam([FromBody] TeamModel teamModel)
        {

            Team team = _mapper.Map<Team>(teamModel);

            await team.Validate();

            User user = _contexto.Users.FirstOrDefault(u => u.Login == User.Identity.Name);

            _contexto.Add(team);
            _contexto.SaveChanges();

            TeamUser userTeam = new()
            {
                User = user,
                UserId = user.PersonId,
                Team = team,
                TeamId = team.Id,
                UserRole = Role.Administrator
            };

            user.Teams.Add(userTeam);
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

            Role role = _contexto.TeamUser.FirstOrDefault(eu => eu.TeamId == teamId && eu.UserId == _usuarioLogado.PersonId).UserRole;

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
            TeamUser temUsers = _contexto.TeamUser.FirstOrDefault(x => x.TeamId == teamId && x.UserId == userId);

            if (temUsers == null)
                return NotFound();

            _contexto.Remove(temUsers);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        private bool UserHasTeam(int teamId)
        {
            return _contexto.TeamUser.Any(e => e.TeamId == teamId && e.UserId == _usuarioLogado.PersonId);
        }

    }
}
