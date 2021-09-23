using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeTarefas.Context.Migrations
{
    internal class Configuration
    {
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            DbContext context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
            context.Database.EnsureCreated();
        }
    }
}
