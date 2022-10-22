using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Objectives;
using GerenciadorDeTarefas.Common.Models.Pessoas;
using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Common.Models.Teams;
using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Objectives;
using GerenciadorDeTarefas.Domain.People;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Tasks;
using GerenciadorDeTarefas.Domain.Teams;
using GerenciadorDeTarefas.Domain.Users;

namespace GerenciadorDeTarefas.WebApi
{
    public class WebApiAutoMapperConfig : Profile
    {
        public WebApiAutoMapperConfig()
        {
            //Equipes
            CreateMap<Team, TeamModel>()
                .ForMember(e => e.Users, dest => dest.Ignore())
                .ReverseMap();

            //Objetivos
            CreateMap<Objective, ObjectiveModel>()
                .ReverseMap();

            //Pessoas
            CreateMap<Person, PersonModel>()
                .ReverseMap();

            //Projetos
            CreateMap<Project, ProjectModel>()
                .ReverseMap();

            //Tarefas
            CreateMap<Task, TaskModel>()
                .ReverseMap();

            //Usuarios
            CreateMap<User, UserModel>()
                .ForMember(u => u.Password, dest => dest.Ignore())
                .ForMember(u => u.Teams, dest => dest.Ignore());
            //.ForMember(
            //dest => dest.Equipes,
            //src => src.MapFrom( 
            //    s => s.Equipes.Where(
            //        eu => eu.IdUsuario == s.IdPessoa).Select(u => u.Equipe).ToList()));
            CreateMap<UserModel, User>()
                .ForMember(u => u.Team, dest => dest.Ignore());
        }
    }
}
