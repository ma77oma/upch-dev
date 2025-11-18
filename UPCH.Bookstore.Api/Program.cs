using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UPCH.Bookstore.Api; // Asegúrate de que este es tu namespace

namespace UPCH.Bookstore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Construye el host a través del método estático y lo ejecuta.
            CreateHostBuilder(args).Build().Run();
        }

        // Método estático que crea y configura el host, apuntando a Startup.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Apunta a la clase Startup que ahora encapsulará la configuración.
                    webBuilder.UseStartup<Startup>();
                });
    }
}