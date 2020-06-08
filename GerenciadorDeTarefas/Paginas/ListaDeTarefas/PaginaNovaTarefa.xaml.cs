using GerenciadorDeTarefas.Models.Equipes;
using GerenciadorDeTarefas.Models.Tarefas;
using GerenciadorDeTarefas.Models.Usuarios;
using GerenciadorDeTarefas.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GerenciadorDeTarefas.Paginas.Master;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.ListaDeTarefas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaNovaTarefa : ContentPage
    {
        public PaginaNovaTarefa()
        {
            InitializeComponent();

            foreach (var prioridade in Prioridades)
            {
                Prioridade.Items.Add(prioridade);
            }
        }

        public List<string> Prioridades
        {
            get
            {
                return Enum.GetNames(typeof(Prioridade)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        private async void SalvarTarefa(object sender, EventArgs args)
        {
            Prioridade prioridade = Models.Tarefas.Prioridade.Sugestão;

            if (Prioridade.SelectedItem != null)
                Enum.TryParse(Prioridade.SelectedItem.ToString(), out prioridade);           

            TarefaModel tarefa = new TarefaModel
            {
                Adicionado = DateTime.Today,
                Nome = NomeDaTarefa.Text,
                Detalhes = DetalhesDaTarefa.Text,
                Previsao = PrevisaoDeConclusao.Date,
                Prioridade = prioridade,
                Situacao = Situacao.Novo
            };

            var equipe = App.Usuario.Equipes.FirstOrDefault(e => e.Nome == Master.Master.EquipeSelecionada.Nome);

            await App.Usuario.Salvar();

            MessagingCenter.Send(new Master.Master(), "AtualizarMenu");
        }
    }
}