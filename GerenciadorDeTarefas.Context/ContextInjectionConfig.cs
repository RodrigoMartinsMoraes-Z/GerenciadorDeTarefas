using GerenciadorDeTarefas.Context.Migrations;
using GerenciadorDeTarefas.Domain.Contexto;

using SimpleInjector;

namespace GerenciadorDeTarefas.Context
{
    public class ContextInjectionConfig
    {
        public void Register(Container container)
        {
            //container.Register<IContextoDeDados, ContextoDeDados>(Lifestyle.Scoped);
        }
    }
}
