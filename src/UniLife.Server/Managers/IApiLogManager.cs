using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;

namespace UniLife.Server.Managers
{
    public interface IApiLogManager
    {
        Task Log(ApiLogItem apiLogItem);
        Task<ApiResponse> Get();
        Task<ApiResponse> GetByApplicationUserId(Guid applicationUserId);
    }
}