using GerenciadorDeTarefas.WebApi.Controllers;

using SimpleInjector;

namespace GerenciadorDeTarefas.WebApi
{
    public class WebApiInjectionConfig
    {
        public void Register(Container container)
        {
            //container.Register<TarefaController>(Lifestyle.Scoped);
            //container.Register<TesteController>(Lifestyle.Scoped);
            //container.Register<UsuarioController>(Lifestyle.Scoped);

            //container.Collection.Register<BaseApiController>(
            //    typeof(TarefaController),
            //    typeof(TesteController),
            //    typeof(UsuarioController)
            //    );

        }
    }
}
