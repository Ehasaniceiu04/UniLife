using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface IOsymManager : IBaseManager<Osym, OsymDto>
    {
        Task<ApiResponse> GetByAppId(Guid appId);
    }
}
