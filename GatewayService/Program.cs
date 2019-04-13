using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace GatewayService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:8081")
//                .ConfigureAppConfiguration((host,builder) => {
//                 builder.SetBasePath(host.HostingEnvironment.ContentRootPath)
//                .AddJsonFile("ocelot.json"); })
                .UseStartup<Startup>();
    }
}
