using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GatewayService.Models;

namespace GatewayService.Repository
{
    public interface IConfigurationRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<List<ReRouteEntity>> GetReRoutes();

        Task<ReRouteEntity> GetReRouteById(int routeId);

        Task<GlobalConfigurationEntity> GetActiveGlobalConfigurations();
    }
}
