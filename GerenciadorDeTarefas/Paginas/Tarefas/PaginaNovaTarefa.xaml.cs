using GerenciadorDeTarefas.Models.Equipes;
using GerenciadorDeTarefas.Models.Projetos;
using GerenciadorDeTarefas.Models.Tarefas;
using GerenciadorDeTarefas.Util;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Tarefas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaNovaTarefa : ContentPage
    {
        private readonly IControleMenu _controleMenu = App.IoCConainer.GetInstance<IControleMenu>();

        public static ProjetoModel Projeto { get; set; }

        public PaginaNovaTarefa()
        {
            InitializeComponent();

            foreach (string prioridade in Prioridades)
            {
                Prioridade.Items.Add(prioridade);
            }
        }

        public List<string> Prioridades => Enum.GetNames(typeof(Prioridade)).Select(b => b.SplitCamelCase()).ToList();

        private async void SalvarTarefa(object sender, EventArgs args)
        {
            if (NomeDaTarefa.Text == null || NomeDaTarefa.Text.Length <= 0)
            {
                await DisplayAlert("Erro!", "O nome datarefa deve conter mais que 0 caracteres!", "OK");
                return;
            }

            Prioridade prioridade = Models.Tarefas.Prioridade.Sugestão;

            if (Prioridade.SelectedItem != null)
                Enum.TryParse(Prioridade.SelectedItem.ToString(), out prioridade);
            else
            {
                await DisplayAlert("Erro!", "Selecione a prioridade da tarefa.", "OK");
                return;
            }

            TarefaModel tarefa = new TarefaModel
            {
                Adicionado = DateTime.Today,
                Nome = NomeDaTarefa.Text,
                Detalhes = DetalhesDaTarefa.Text,
                Previsao = PrevisaoDeConclusao.Date,
                Prioridade = prioridade,
                Situacao = Situacao.Novo
            };

            try
            {
                EquipeModel equipe = App.Usuario.Equipes.SingleOrDefault(e => e.Projetos.Any(p => p.Nome == Projeto.Nome));
                ProjetoModel projeto = equipe.Projetos.SingleOrDefault(p => p.Nome == Projeto.Nome);
                projeto.Tarefas.Add(tarefa);

                App.Usuario.Equipes.Remove(equipe);
                App.Usuario.Equipes.Add(equipe);
                await App.Usuario.Salvar();
                await _controleMenu.AtualizarListaEquipes();
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro!", e.ToString(), "OK");
            }

        }
    }
}