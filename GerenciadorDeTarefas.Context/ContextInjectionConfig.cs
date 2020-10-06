using GerenciadorDeTarefas.Domain.Contexto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Context
{
    public class ContextInjectionConfig
    {
        public void Register(SimpleInjector.Container container)
        {
            container.Register<IContextoDeDados, ContextoDeDados>();
        }
    }
}
