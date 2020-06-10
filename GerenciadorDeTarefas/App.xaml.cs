using GerenciadorDeTarefas.Models.Usuarios;
using GerenciadorDeTarefas.Paginas;
using Newtonsoft.Json;
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
            CarregaUsuario();

            MainPage = new NavigationPage(new Paginas.Master.Master());
        }

        public static Container IoCConainer { get; set; }
        public static Usuario Usuario { get; set; }

        private void IoCRegister()
        {
            IoCConainer = new Container();
            IoCConainer.Register<IControleMenu, ControleMenu>();
        }

        protected override void OnStart()
        {
            CarregaUsuario();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void CarregaUsuario()
        {
            if (App.Current.Properties.ContainsKey("Usuario"))
            {
                Usuario = JsonConvert.DeserializeObject<Usuario>(Current.Properties["Usuario"].ToString());
            }
            else
            {
                Usuario = new Usuario();
            }
        }
    }
}
