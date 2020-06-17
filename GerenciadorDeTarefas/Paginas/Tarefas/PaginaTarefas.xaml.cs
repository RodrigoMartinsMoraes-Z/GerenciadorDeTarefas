using FontAwesome;
using GerenciadorDeTarefas.Models.Projetos;
using GerenciadorDeTarefas.Models.Tarefas;
using System;
using System.Collections;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Tarefas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaTarefas : ContentPage
    {
        public static ProjetoModel Projeto { get; set; }
        public PaginaTarefas()
        {
            InitializeComponent();

            if (Projeto == null)
                DisplayAlert("Erro!", "Não foi possivel carregar as informações sobre o projeto selecionado, favor tentar novamente", "OK");
            else
                Task.WaitAny(CarregarTarefas());
        }

        private async Task CarregarTarefas()
        {
            Title = Projeto.Nome;

            foreach (var tarefa in Projeto.Tarefas)
            {
                StackLayout stackLayout = await CriarLayoutDaTarefa(tarefa, layoutTarefas);
                layoutTarefas.Children.Add(stackLayout);
            }

        }

        private async Task<StackLayout> CriarLayoutDaTarefa(TarefaModel tarefa, StackLayout layoutTarefas)
        {
            StackLayout layout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal

            };

            layout.Children.Add(
                new Label
                {
                    Text = tarefa.Nome
                }
                );

            layout.Children.Add(
                new Button
                {
                    Text = FontAwesomeIcons.Eye,
                    FontFamily = "{StaticResource FontAwesomeSolid}"
                }
                );

            if (tarefa.SubTarefas != null && tarefa.SubTarefas.Count > 0)
            {
                foreach (TarefaModel tarefa2 in tarefa.SubTarefas)
                {
                    StackLayout layoutSubTarefa = await CriarLayoutDaTarefa(tarefa2, layout);
                    layout.Children.Add(layoutSubTarefa);
                }
            }

            return layout;
        }

        private void AddNovaTarefa(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaNovaTarefa());
            PaginaNovaTarefa.Projeto = Projeto;
        }
    }
}