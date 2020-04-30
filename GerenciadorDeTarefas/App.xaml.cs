using GerenciadorDeTarefas.Scripts.Calculadora;
using SimpleInjector;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas
{
    public partial class App : Application
    {

        public App()
        {
            Container container = new Container();
            InitializeComponent();
            Container(container);

            MainPage = new NavigationPage(new Paginas.Master.Master());
        }

        private void Container(Container container)
        {
            container.Register<ICalculadora, Calculadora>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
