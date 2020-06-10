using GerenciadorDeTarefas.Models.Projetos;

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
                DisplayAlert("Erro 404", "Não foi possivel carregar as informações sobre o projeto selecionado, favor tentar novamente", "OK");
            else
                Title = Projeto.Nome;
        }
    }
}