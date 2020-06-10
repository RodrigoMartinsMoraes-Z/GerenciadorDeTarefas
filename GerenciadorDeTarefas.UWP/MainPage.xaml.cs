namespace GerenciadorDeTarefas.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new GerenciadorDeTarefas.App());
        }
    }
}
