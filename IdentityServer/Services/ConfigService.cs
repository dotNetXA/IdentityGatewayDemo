using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Services
{
    public class ConfigService
    {
        public static void ConfigServiceInMemory(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(InitMemoryData.GetApiResources())
                .AddInMemoryClients(InitMemoryData.GetClients())
                .AddTestUsers(InitMemoryData.GetUsers());
        }
    }
}
