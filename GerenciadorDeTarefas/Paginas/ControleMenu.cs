using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GerenciadorDeTarefas.Paginas
{
    public class ControleMenu
    {
        public static Task AtualizarMenu()
        {
            MessagingCenter.Send(new Master.Master(), "AtualizarMenu");

            return Task.CompletedTask;
        }
    }
}
