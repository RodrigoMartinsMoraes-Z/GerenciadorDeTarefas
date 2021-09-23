using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Objetivos;
using GerenciadorDeTarefas.Common.Models.Pessoas;
using GerenciadorDeTarefas.Common.Models.Projetos;
using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Tasks;
using GerenciadorDeTarefas.Domain.Usuarios;

namespace GerenciadorDeTarefas.WebApi
{
    public class WebApiAutoMapperConfig : Profile
    {
        public WebApiAutoMapperConfig()
        {
            //Equipes
            CreateMap<Team, EquipeModel>()
                .ForMember(e => e.Usuarios, dest => dest.Ignore())
                .ReverseMap();

            //Objetivos
            CreateMap<Objective, ObjetivoModel>()
                .ReverseMap();

            //Pessoas
            CreateMap<Person, PessoaModel>()
                .ReverseMap();

            //Projetos
            CreateMap<Project, ProjetoModel>()
                .ReverseMap();

            //Tarefas
            CreateMap<Task, TarefaModel>()
                .ReverseMap();

            //Usuarios
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(u => u.Senha, dest => dest.Ignore())
                .ForMember(u => u.Equipes, dest => dest.Ignore());
            //.ForMember(
            //dest => dest.Equipes,
            //src => src.MapFrom( 
            //    s => s.Equipes.Where(
            //        eu => eu.IdUsuario == s.IdPessoa).Select(u => u.Equipe).ToList()));
            CreateMap<UsuarioModel, Usuario>()
                .ForMember(u => u.Equipes, dest => dest.Ignore());
        }
    }
}
