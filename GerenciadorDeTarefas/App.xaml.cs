using GerenciadorDeTarefas.Models.Usuarios;
using GerenciadorDeTarefas.Paginas;
using Newtonsoft.Json;
using SimpleInjector;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GerenciadorDeTarefas
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Task.WaitAll(
                IoCRegister(),
                CarregaUsuario(),
                CarregaFontAwesome()
            );

            MainPage = new Paginas.Master.Master();
        }

        public static Container IoCConainer { get; set; }
        public static Usuario Usuario { get; set; }
        public static string FontAwesomeBrands { get; set; }
        public static string FontAwesomeSolid { get; set; }
        public static string FontAwesomeRegular { get; set; }

        private Task IoCRegister()
        {
            IoCConainer = new Container();
            IoCConainer.Register<IControleMenu, ControleMenu>();

            return Task.CompletedTask;
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

        private Task CarregaUsuario()
        {
            if (App.Current.Properties.ContainsKey("Usuario"))
            {
                Usuario = JsonConvert.DeserializeObject<Usuario>(Current.Properties["Usuario"].ToString());
            }
            else
            {
                Usuario = new Usuario();
            }

            return Task.CompletedTask;
        }

        private Task CarregaFontAwesome()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    FontAwesomeRegular = "FontAwesome5Regular.otf#Regular";
                    FontAwesomeSolid = "FontAwesome5Solid.otf#Regular";
                    FontAwesomeBrands = "FontAwesome5Brands.otf#Regular";
                    break;

                case Device.UWP:
                    FontAwesomeRegular = "/Assets/FontAwesome5Regular.otf#Font Awesome 5 Free";
                    FontAwesomeSolid = "/Assets/FontAwesome5Solid.otf#Font Awesome 5 Free";
                    FontAwesomeBrands = "/Assets/FontAwesome5Brands.otf#Font Awesome 5 Brands";
                    break;

                case Device.iOS:
                    FontAwesomeRegular = "FontAwesome5Brands-Regular";
                    FontAwesomeSolid = "FontAwesome5Free-Solid";
                    FontAwesomeBrands = "FontAwesome5Free-Regular";
                    break;
            }


            return Task.CompletedTask;
        }

        public Task SalvarUsuario()
        {
            string json = JsonConvert.SerializeObject(this);

            if (App.Current.Properties.ContainsKey("Usuario"))
                App.Current.Properties.Remove("Usuario");

            App.Current.Properties.Add("Usuario", json);

            return Task.CompletedTask;
        }

    }
}
