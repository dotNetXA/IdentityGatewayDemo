using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GatewayService.Extension;
using GatewayService.Models;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;

namespace GatewayService.DataBase
{
    public class SqlServerFileConfigurationConfigurationRepository : IFileConfigurationRepository
    {
        private readonly DataBaseConfiguration _context;

        public SqlServerFileConfigurationConfigurationRepository(DataBaseConfiguration context)
        {
            _context = context;
        }

        public async Task<Response<FileConfiguration>> Get()
        {
            var configurationFile = new FileConfiguration();

            using (var connection = new SqlConnection(_context.DbConnectionString))
            {
                var globalConfigurationSql = "select * from GlobalConfigurations where IsDefault = 1 and InfoStatus = 1";
                var result = await connection.QueryFirstOrDefaultAsync<GlobalConfigurationEntity>(globalConfigurationSql);

                if (result != null)
                {
                    SetGlobalConfigurations(result, configurationFile);

                    SetReRoutes(result, connection, configurationFile);
                }
                else
                {
                    throw new Exception("No configurations can be found!");
                }

                if (configurationFile.ReRoutes == null || configurationFile.ReRoutes.Count == 0)
                {
                    return new OkResponse<FileConfiguration>(null);
                }

            }

            return new OkResponse<FileConfiguration>(configurationFile);
        }

        private static void SetReRoutes(GlobalConfigurationEntity result, SqlConnection connection,
            FileConfiguration configurationFile)
        {
            var routeResult = connection.QueryAsync<ReRouteEntity>($"select * from ReRoutes").Result.ToList();
            if (routeResult.Count > 0)
            {
                var reRoutes = new List<FileReRoute>();
                foreach (var model in routeResult)
                {
                    SetRouteValue(model, reRoutes);
                }

                configurationFile.ReRoutes = reRoutes;
            }
        }

        private static void SetGlobalConfigurations(GlobalConfigurationEntity result, FileConfiguration configurationFile)
        {
            var globalConfiguration = new FileGlobalConfiguration
            {
                BaseUrl = result.BaseUrl,
                DownstreamScheme = result.DownstreamScheme,
                RequestIdKey = result.RequestIdKey
            };
            if (!string.IsNullOrEmpty(result.HttpHandlerOptions))
            {
                globalConfiguration.HttpHandlerOptions =
                    result.HttpHandlerOptions.ToObject<FileHttpHandlerOptions>();
            }

            if (!string.IsNullOrEmpty(result.LoadBalancerOptions))
            {
                globalConfiguration.LoadBalancerOptions =
                    result.LoadBalancerOptions.ToObject<FileLoadBalancerOptions>();
            }

            if (!string.IsNullOrEmpty(result.QoSOptions))
            {
                globalConfiguration.QoSOptions = result.QoSOptions.ToObject<FileQoSOptions>();
            }

            if (!string.IsNullOrEmpty(result.ServiceDiscoveryProvider))
            {
                globalConfiguration.ServiceDiscoveryProvider =
                    result.ServiceDiscoveryProvider.ToObject<FileServiceDiscoveryProvider>();
            }

            configurationFile.GlobalConfiguration = globalConfiguration;
        }

        private static void SetRouteValue(ReRouteEntity model, List<FileReRoute> reRoutes)
        {
            var route = new FileReRoute();

            if (!string.IsNullOrEmpty(model.AuthenticationOptions))
            {
                route.AuthenticationOptions =
                    model.AuthenticationOptions.ToObject<FileAuthenticationOptions>();
            }

            if (!string.IsNullOrEmpty(model.CacheOptions))
            {
                route.FileCacheOptions = model.CacheOptions.ToObject<FileCacheOptions>();
            }

            if (!string.IsNullOrEmpty(model.DelegatingHandlers))
            {
                route.DelegatingHandlers = model.DelegatingHandlers.ToObject<List<string>>();
            }

            if (!string.IsNullOrEmpty(model.LoadBalancerOptions))
            {
                route.LoadBalancerOptions = model.LoadBalancerOptions.ToObject<FileLoadBalancerOptions>();
            }

            if (!string.IsNullOrEmpty(model.QoSOptions))
            {
                route.QoSOptions = model.QoSOptions.ToObject<FileQoSOptions>();
            }

            if (!string.IsNullOrEmpty(model.DownstreamHostAndPorts))
            {
                route.DownstreamHostAndPorts =
                    model.DownstreamHostAndPorts.ToObject<List<FileHostAndPort>>();
            }

            route.DownstreamPathTemplate = model.DownstreamPathTemplate;
            route.DownstreamScheme = model.DownstreamScheme;
            route.Key = model.RequestIdKey;
            route.Priority = model.Priority ?? 0;
            route.RequestIdKey = model.RequestIdKey;
            route.ServiceName = model.ServiceName;
            route.UpstreamHost = model.UpstreamHost;
            route.UpstreamHttpMethod = model.UpstreamHttpMethod?.ToObject<List<string>>();
            route.UpstreamPathTemplate = model.UpstreamPathTemplate;
            reRoutes.Add(route);
        }

        public async Task<Response> Set(FileConfiguration fileConfiguration)
        {
            return new OkResponse();
        }
    }
}
