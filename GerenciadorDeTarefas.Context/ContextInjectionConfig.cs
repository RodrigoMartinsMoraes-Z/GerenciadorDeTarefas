using GerenciadorDeTarefas.Domain.Contexto;

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
