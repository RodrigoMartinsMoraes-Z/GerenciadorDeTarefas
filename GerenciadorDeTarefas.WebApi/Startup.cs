using AutoMapper;

using GerenciadorDeTarefas.Context;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Domain.Contexto;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SimpleInjector;
using SimpleInjector.Integration.WebApi;

using System.Web.Http;

namespace GerenciadorDeTarefas.WebApi
{
    public class Startup
    {
        private readonly Container _container = new Container();

        private readonly WebApiInjectionConfig _webApiInjectionConfig = new WebApiInjectionConfig();
        private readonly DomainInjectionConfig _domainInjectionConfig = new DomainInjectionConfig();
        private readonly ContextInjectionConfig _contextInjectionConfig = new ContextInjectionConfig();
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _container.Options.ResolveUnregisteredConcreteTypes = false;

            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IContextoDeDados, ContextoDeDados>(options =>
                     options
                     .UseLazyLoadingProxies()
                     .UseNpgsql(
                         Configuration.GetConnectionString("GerenciadorDeTarefas")),
                        ServiceLifetime.Scoped
                        );

            services.AddControllers();

            services.AddSimpleInjector(_container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope and
                // allows request-scoped framework services to be resolved.
                options.AddAspNetCore();

            });

            InitializeContainer();

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WebApiAutoMapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddMvc();
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
            app.UseSimpleInjector(_container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

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
