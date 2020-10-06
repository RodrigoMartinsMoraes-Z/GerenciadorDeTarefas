using GerenciadorDeTarefas.Domain.Contexto;

using SimpleInjector;

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
