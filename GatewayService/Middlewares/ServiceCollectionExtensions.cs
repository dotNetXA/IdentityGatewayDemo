using System;
using GatewayService.DataBase;
using GatewayService.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;

namespace GatewayService.Middlewares
{
    public static class ServiceCollectionExtensions
    {
        public static IOcelotBuilder AddOcelotWithConfigurationInDb(this IOcelotBuilder builder, Action<DataBaseConfiguration> option)
        {
            builder.Services.Configure(option);
            //配置信息
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<DataBaseConfiguration>>().Value);
            //配置文件仓储注入
            builder.Services.AddSingleton<IFileConfigurationRepository, SqlServerFileConfigurationConfigurationRepository>();

            return builder;
        }
    }
}
