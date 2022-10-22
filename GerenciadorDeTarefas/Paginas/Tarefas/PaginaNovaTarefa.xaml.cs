using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Tarefas;
using GerenciadorDeTarefas.Common.Models.Teams;
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

        public static ProjectModel Projeto { get; set; }

        public PaginaNovaTarefa()
        {
            InitializeComponent();

            foreach (string prioridade in Prioridades)
            {
                Prioridade.Items.Add(prioridade);
            }
        }

        public List<string> Prioridades => Enum.GetNames(typeof(Priority)).Select(b => b.SplitCamelCase()).ToList();

        private async void SalvarTarefa(object sender, EventArgs args)
        {
            if (NomeDaTarefa.Text == null || NomeDaTarefa.Text.Length <= 0)
            {
                await DisplayAlert("Erro!", "O nome datarefa deve conter mais que 0 caracteres!", "OK");
                return;
            }

            Priority prioridade = Common.Models.Tarefas.Priority.Suggestion;

            if (Prioridade.SelectedItem != null)
                Enum.TryParse(Prioridade.SelectedItem.ToString(), out prioridade);
            else
            {
                await DisplayAlert("Erro!", "Selecione a prioridade da tarefa.", "OK");
                return;
            }

            TaskModel tarefa = new TaskModel
            {
                Added = DateTime.Today,
                Name = NomeDaTarefa.Text,
                Details = DetalhesDaTarefa.Text,
                Forecast = PrevisaoDeConclusao.Date,
                Priority = prioridade,
                Status = Status.New
            };

            try
            {
                TeamModel equipe = App.User.Teams.SingleOrDefault(e => e.Projects.Any(p => p.Name == Projeto.Name));
                ProjectModel projeto = equipe.Projects.SingleOrDefault(p => p.Name == Projeto.Name);
                projeto.Tasks.Add(tarefa);

                App.User.Teams.Remove(equipe);
                App.User.Teams.Add(equipe);
                //await App.Usuario.Salvar();
                await _controleMenu.AtualizarListaEquipes();
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro!", e.ToString(), "OK");
            }

        }
    }
}