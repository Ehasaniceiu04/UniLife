using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Tenant;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface ITenantManager
    {
        Task<ApiResponse> Get();

        Task<ApiResponse> Get(int id);

        Task<ApiResponse> Create(TenantDto tenant);

        Task<ApiResponse> Update(TenantDto tenant);

        Task<ApiResponse> Delete(int id);
    }
}
