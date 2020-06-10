using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GerenciadorDeTarefas.Paginas
{
    public interface IControleMenu
    {
        Task AtualizarListaEquipes();
    }
    public class ControleMenu:IControleMenu
    {
        public Task AtualizarListaEquipes()
        {
            MessagingCenter.Send(new Master.Master(), "AtualizarMenu");

            return Task.CompletedTask;
        }
    }
}
