using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UPCH.Bookstore.Application.Common.Behaviors;
using UPCH.Bookstore.Application.Libros.Commands.CreateLibro;
using UPCH.Bookstore.Infrastructure.Data;
using UPCH.Bookstore.Infrastructure.Repositories.Implementations;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Api
{
    public class Startup
    {
        // Propiedad Configuration para acceder a appsettings.json
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            var secretName = Configuration["Database:SecretName"] ?? "upch/dev/sqlserver"; // Nombre de tu secreto

            // Intentar construir el DbContext basado en el entorno
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                var secretClient = new AmazonSecretsManagerClient();
                var response = secretClient.GetSecretValueAsync(new GetSecretValueRequest
                {
                    SecretId = secretName
                }).GetAwaiter().GetResult(); // Ejecución síncrona en el Startup

                var secretData = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);

                var connectionString = $"Server={secretData["host"]},{secretData["port"]};" +
                                       $"Database=upchbookdev;" + // Nombre de tu BD (ver DbContext)
                                       $"User Id={secretData["username"]};" +
                                       $"Password={secretData["password"]};" +
                                       $"TrustServerCertificate=True;"; // Necesario por el error SSL

                // Registrar el DbContext con la cadena de Secrets Manager
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(connectionString)
                );
            }
            else
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(connectionString)
                );
            }

            services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(UPCH.Bookstore.Application.Libros.DTOs.LibroDto).Assembly)
            );
            services.AddValidatorsFromAssembly(typeof(CreateLibroCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<ILibrosRepository, LibrosRepository>();

            services.AddControllers();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("DefaultCors");
            app.UseAuthorization();

            // Configuración para que el Router sepa cómo mapear a los Controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
