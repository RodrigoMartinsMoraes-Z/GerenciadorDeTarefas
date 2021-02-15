using AutoMapper;

using GerenciadorDeTarefas.Context;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Domain.Contexto;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json.Converters;

using SimpleInjector;
using SimpleInjector.Integration.WebApi;

using System.Text;
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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenService.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<IContextoDeDados, ContextoDeDados>(options =>
                     options
                     .UseLazyLoadingProxies()
                     .UseNpgsql(
                         Configuration.GetConnectionString("GerenciadorDeTarefas")),
                        ServiceLifetime.Scoped
                        );

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerGenNewtonsoftSupport();

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

            services.AddSwaggerGen();


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gerenciador De Tarefas");
                c.RoutePrefix = string.Empty;

            });

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
