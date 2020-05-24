using GerenciadorDeTarefas.Models.Tarefas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.ListaDeTarefas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaTarefasAFazer : ContentPage
    {
        public PaginaTarefasAFazer()
        {
            InitializeComponent();

            List<Tarefa> tarefas = new List<Tarefa>();
            tarefas.Add(new Tarefa
            {
                Nome = "Tarefa",
                Adicionado = DateTime.Today,
                Previsao = DateTime.Today.AddDays(3),
                Prioridade = Prioridade.Importante_Não_Urgente,
                Situacao = Status.Novo
            });
            tarefas.Add(new Tarefa
            {
                Nome = "Tarefa",
                Adicionado = DateTime.Today,
                Previsao = DateTime.Today.AddDays(3),
                Prioridade = Prioridade.Importante_Não_Urgente,
                Situacao = Status.Novo
            });
            tarefas.Add(new Tarefa
            {
                Nome = "Tarefa",
                Adicionado = DateTime.Today,
                Previsao = DateTime.Today.AddDays(3),
                Prioridade = Prioridade.Importante_Não_Urgente,
                Situacao = Status.Novo
            });
            tarefas.Add(new Tarefa
            {
                Nome = "Tarefa",
                Adicionado = DateTime.Today,
                Previsao = DateTime.Today.AddDays(3),
                Prioridade = Prioridade.Importante_Não_Urgente,
                Situacao = Status.Novo
            });
            tarefas.Add(new Tarefa
            {
                Nome = "Tarefa",
                Adicionado = DateTime.Today,
                Previsao = DateTime.Today.AddDays(3),
                Prioridade = Prioridade.Importante_Não_Urgente,
                Situacao = Status.Novo
            });

            ListaTarefas.ItemsSource = tarefas;
        }
    }
}