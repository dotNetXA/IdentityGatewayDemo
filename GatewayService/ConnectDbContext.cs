using GatewayService.Models;
using Microsoft.EntityFrameworkCore;

namespace GatewayService
{
    public class ConnectDbContext: DbContext
    {
        public DbSet<ReRouteEntity> ReRoutes { get; set; }

        public DbSet<GlobalConfigurationEntity> GlobalConfigurations { get; set; }

        public ConnectDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
