using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vtbbook.Application.Domain;
using vtbbook.Application.Service;
using vtbbook.Core.DataAccess;

namespace vtbbook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DbRegistration(services);
            ServicesRegistration(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void DbRegistration(IServiceCollection services)
        {
            var connectionString = "Server=85.185.82.229;User Id=some_user;Password=some_password;Port=5432;Database=some_bd;";

            services
                .AddDbContext<IDataContext, DataContext>(options =>
                    options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
        }

        private void ServicesRegistration(IServiceCollection services)
        {
            services
                .AddScoped<ISomeService, SomeService>()
                .AddScoped<ISomeDomain, SomeDomain>();
        }
    }
}
