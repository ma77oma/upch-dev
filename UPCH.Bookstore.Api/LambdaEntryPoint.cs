using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Amazon.Lambda.AspNetCoreServer;

namespace UPCH.Bookstore.Api
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
            })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseSetting(WebHostDefaults.ApplicationKey, GetType().Assembly.FullName)
            .UseStartup<Startup>();

            builder.UseSetting(WebHostDefaults.ApplicationKey, typeof(Program).Assembly.FullName);
        }
    }
}
