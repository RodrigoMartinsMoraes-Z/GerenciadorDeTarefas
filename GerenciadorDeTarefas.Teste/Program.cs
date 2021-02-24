using Api.GerenciadorDeTarefas;

using GerenciadorDeTarefas.Teste.Controllers;
using GerenciadorDeTarefas.Teste.Relatorios;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Teste
{
    class Program
    {
        private static ServicoDoGerenciadorDeTarefas _servico = new ServicoDoGerenciadorDeTarefas();
        private static List<Relatorio> _relatorios = new List<Relatorio>();

        static void Main(string[] args)
        {

            _servico.DefinirUrlDoSistema("http://localhost:65312");

            Testes();

            Finalizacao();

        }

        private static void Finalizacao()
        {
            Console.WriteLine("Processo finalizado");
            Console.WriteLine("Exibir relatorio? (s/n)");

            string resposta = Console.ReadLine();
            if (resposta == "n")
                Environment.Exit(0);

            else if (resposta == "s")
                ExibirRelatorio();

            Finalizacao();
        }

        private static void ExibirRelatorio()
        {
            CriarCabecalho();

           foreach(var relatorio in _relatorios)
            {
                Console.WriteLine($"|{relatorio.Metodo,-23}|{relatorio.Resposta,-49}|{(relatorio.Sucesso?"Ok":"Erro"),-9}|");
                SepararLinha();
            }

            Environment.Exit(0);
        }

        private static void CriarCabecalho()
        {
            Console.WriteLine("|        Metodo         |                     Mensagem                    | Sucesso |");
            SepararLinha();
        }

        private static void SepararLinha() => Console.WriteLine("|".PadRight(24, '_') + "|".PadRight(50, '_') + "|".PadRight(10, '_') + "|");

        private static void Testes()
        {
            Console.WriteLine("Selecione o que deseja testar");
            Console.WriteLine("1 - ContaController");
            //Console.WriteLine("Selecione o que deseja testar");
            //Console.WriteLine("Selecione o que deseja testar");
            //Console.WriteLine("Selecione o que deseja testar");
            Console.WriteLine("0 - Sair");

            string resposta = Console.ReadLine();

            if (resposta == "0")
                Environment.Exit(0);

            else if (resposta == "1")
                TesteContaController();

            else
                Testes();

        }

        private static async void TesteContaController()
        {
            UsuarioController _controller = new UsuarioController(_servico);

            Console.WriteLine("Novo Usuario");
            Relatorio relatorio = await _controller.CriarUsuario();

            _relatorios.Add(relatorio);
        }
    }
}
