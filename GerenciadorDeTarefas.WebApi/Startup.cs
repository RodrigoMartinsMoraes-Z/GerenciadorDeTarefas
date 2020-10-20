using GerenciadorDeTarefas.Context;
using GerenciadorDeTarefas.Domain;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SimpleInjector;

namespace GerenciadorDeTarefas.WebApi
{
    public class Startup
    {
        private readonly Container _container = new Container();

        private readonly WebApiInjectionConfig _webApiInjectionConfig = new WebApiInjectionConfig();
        private readonly DomainInjectionConfig _domainInjectionConfig = new DomainInjectionConfig();
        private readonly ContextInjectionConfig _contextInjectionConfig = new ContextInjectionConfig();

        public Startup(IConfiguration configuration)
        {
            _container.Options.ResolveUnregisteredConcreteTypes = false;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextoDeDados>(options =>
                    options.UseNpgsql(
                        Configuration.GetConnectionString("GerenciadorDeTarefas")),
                        ServiceLifetime.Scoped
                        );

            services.AddControllers();

            services.AddSimpleInjector(_container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope and
                // allows request-scoped framework services to be resolved.
                options.AddAspNetCore()
                    .AddControllerActivation();

            });

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            _webApiInjectionConfig.Register(_container);
            _domainInjectionConfig.Register(_container);
            _contextInjectionConfig.Register(_container);
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
