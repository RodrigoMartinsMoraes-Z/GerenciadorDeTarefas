using GerenciadorDeTarefas.Scripts.Calculadora;
using SimpleInjector;
using Xamarin.Forms;

namespace GerenciadorDeTarefas
{
    public partial class App : Application
    {

        public App()
        {            
            InitializeComponent();
            IoCRegister();            

            MainPage = new NavigationPage(new Paginas.Master.Master());
        }

        public static Container IoCConainer { get; set; }

        private void IoCRegister()
        {
            IoCConainer = new Container();
            IoCConainer.Register<ICalculadora, Calculadora>();
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
