using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GatewayService.Models;
using Microsoft.EntityFrameworkCore;

namespace GatewayService.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ConnectDbContext _context;

        public ConfigurationRepository(ConnectDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<ReRouteEntity>> GetReRoutes()
        {
            return _context.ReRoutes.ToListAsync();
        }

        public Task<ReRouteEntity> GetReRouteById(int routeId)
        {
            return _context.ReRoutes.Where(x => x.ReRouteId == routeId).FirstAsync();
        }

        public Task<GlobalConfigurationEntity> GetActiveGlobalConfigurations()
        {
            return _context.GlobalConfigurations.FirstOrDefaultAsync(x => x.IsDefault == 1 && x.InfoStatus == 1);
        }
    }
}
