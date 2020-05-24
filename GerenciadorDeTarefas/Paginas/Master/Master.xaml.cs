using GerenciadorDeTarefas.Paginas.ListaDeTarefas;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public Master()
        {
            InitializeComponent();
        }

        private void ChamaPaginaAFazer(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new PaginaTarefasAFazer());
        }
    }
}