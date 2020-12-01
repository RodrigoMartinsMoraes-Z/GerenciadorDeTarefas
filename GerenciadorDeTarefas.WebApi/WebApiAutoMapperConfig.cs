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
using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.Usuarios;

using System.Linq;

namespace GerenciadorDeTarefas.WebApi
{
    public class WebApiAutoMapperConfig : Profile
    {
        public WebApiAutoMapperConfig()
        {
            //Equipes
            CreateMap<Equipe, EquipeModel>()
                .ForMember(e => e.Usuarios, dest => dest.Ignore());

            CreateMap<EquipeModel, Equipe>()
                .ForMember(e => e.Usuarios, dest => dest.Ignore());

            //Objetivos
            CreateMap<Objetivo, ObjetivoModel>();
            CreateMap<ObjetivoModel, Objetivo>();

            //Pessoas
            CreateMap<Pessoa, PessoaModel>();
            CreateMap<PessoaModel, Pessoa>();

            //Projetos
            CreateMap<Projeto, ProjetoModel>();
            CreateMap<ProjetoModel, Projeto>();

            //Tarefas
            CreateMap<Tarefa, TarefaModel>();
            CreateMap<TarefaModel, Tarefa>();

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
