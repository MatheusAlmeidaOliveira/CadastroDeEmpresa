using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using CadastroDeEmpresas.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroDeEmpresas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adiciona a instância do banco de dados através da connectionstring
            services.AddDbContext<CadastroContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //adiciona na memória uma instância das interfaces
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                //define a rota padrão inicial da aplicação
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Cadastro}/{action=Menu}/{cnpj?}");
            });

        }
    }
}
