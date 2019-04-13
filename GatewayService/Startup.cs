using System;
using GatewayService.Middlewares;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewayService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Action<IdentityServerAuthenticationOptions> orderClient = option =>
            {
                option.Authority = "http://192.168.191.3:5008";
                option.RequireHttpsMetadata = false;
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiName = "orderapi";
                option.ApiSecret = "ordersecret";
            };

            Action<IdentityServerAuthenticationOptions> productClient = option =>
            {
                option.Authority = "http://192.168.191.3:5008";
                option.RequireHttpsMetadata = false;
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiName = "productapi";
                option.ApiSecret = "productsecret";
            };

            services.AddAuthentication()
                .AddIdentityServerAuthentication("OrderIdentityKey", orderClient)
                .AddIdentityServerAuthentication("ProductIdentityKey", productClient);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<ConnectDbContext>(databaseOptions =>
                    {
                        databaseOptions.UseSqlServer(connectionString,
                            sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });
                    }
                );

            services.AddOcelot().AddOcelotWithConfigurationInDb(option =>
                {
                    option.DbConnectionString = connectionString;
                });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseOcelotConfigWithDataBase().Wait();
        }
    }
}
