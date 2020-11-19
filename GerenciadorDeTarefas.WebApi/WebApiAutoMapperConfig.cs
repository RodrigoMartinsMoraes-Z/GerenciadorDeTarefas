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

namespace GerenciadorDeTarefas.WebApi
{
    public class WebApiAutoMapperConfig : Profile
    {
        public WebApiAutoMapperConfig()
        {
            //Equipes
            CreateMap<Equipe, EquipeModel>();
            CreateMap<EquipeModel, Equipe>();

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
            CreateMap<Usuario, UsuarioModel>().ForSourceMember(u => u.Senha, opt => opt.DoNotValidate());
            CreateMap<UsuarioModel, Usuario>();
        }
    }
}
