using GerenciadorDeTarefas.Scripts.Calculadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private async void Somar(object sender, EventArgs args)
        {
            int a = int.Parse(valor1.Text);
            int b = int.Parse(valor2.Text);
            var c = await App.IoCConainer.GetInstance<ICalculadora>().Somar(a, b);
            resultado.Text = (c).ToString();
        }
    }
}