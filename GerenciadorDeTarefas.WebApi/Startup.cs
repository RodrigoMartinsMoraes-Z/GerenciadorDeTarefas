using GerenciadorDeTarefas.Context;
using GerenciadorDeTarefas.Domain;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SimpleInjector;

namespace GerenciadorDeTarefas.WebApi
{
    public class Startup
    {
        private Container _container = new Container();

        private readonly WebApiInjectionConfig webApiInjectionConfig = new WebApiInjectionConfig();
        private readonly DomainInjectionConfig domainInjectionConfig = new DomainInjectionConfig();
        private readonly ContextInjectionConfig contextInjectionConfig = new ContextInjectionConfig();

        public Startup(IConfiguration configuration)
        {
            _container.Options.ResolveUnregisteredConcreteTypes = false;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextoDeDados>();

            services.AddControllers();

            services.AddSimpleInjector(_container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope and
                // allows request-scoped framework services to be resolved.
                options.AddAspNetCore()

                    // Ensure activation of a specific framework type to be created by
                    // Simple Injector instead of the built-in configuration system.
                    // All calls are optional. You can enable what you need. For instance,
                    // ViewComponents, PageModels, and TagHelpers are not needed when you
                    // build a Web API.
                    .AddControllerActivation();
                
            });

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            webApiInjectionConfig.Register(_container);
            domainInjectionConfig.Register(_container);
            contextInjectionConfig.Register(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _container.Verify();
        }
    }
}
