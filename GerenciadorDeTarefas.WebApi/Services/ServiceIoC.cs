using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeTarefas.WebApi.Services
{
    public static class ServiceIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
