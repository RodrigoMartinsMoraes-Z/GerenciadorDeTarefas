using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Models.Tarefas
{
    public enum Status
    {
        Invalido = 0,
        Finalizado,
        Novo,
        Adiado,
        Em_Analise,
        Cancelado,
        Atrasado
    }
}
