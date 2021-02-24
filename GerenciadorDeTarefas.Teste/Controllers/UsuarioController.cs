using Api.GerenciadorDeTarefas;

using GerenciadorDeTarefas.Common.Models.Usuarios;

using System;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Teste.Controllers
{
    internal class UsuarioController
    {
        private readonly ServicoDoGerenciadorDeTarefas _servico;

        public UsuarioController(ServicoDoGerenciadorDeTarefas servico)
        {
            _servico = servico;
        }

        public async Task<Relatorios.Relatorio> CriarUsuario()
        {
            UsuarioModel usuarioModel = new UsuarioModel
            {
                Email = "rodrigo_moraes@hotmail.com.br",
                Login = "rodrigo_adm",
                Senha = "123",
                Pessoa = new Common.Models.Pessoas.PessoaModel
                {
                    Nascimento = new System.DateTime(1995, 02, 26),
                    Nome = "Rodrigo Martins Moraes"
                }
            };

            try
            {
                await _servico.NovoUsuario(usuarioModel);

                return new Relatorios.Relatorio { Metodo = "Criar Usuario", Resposta = "Cadastrado com Sucesso", Sucesso = true };
            }
            catch (Exception e)
            {
                return new Relatorios.Relatorio { Metodo = "Criar Usuario", Sucesso = false, Resposta = e.Message };
            }
        }
    }
}