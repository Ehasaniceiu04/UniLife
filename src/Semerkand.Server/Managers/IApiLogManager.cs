using System;
using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataModels;

namespace Semerkand.Server.Managers
{
    public interface IApiLogManager
    {
        Task Log(ApiLogItem apiLogItem);
        Task<ApiResponse> Get();
        Task<ApiResponse> GetByApplicationUserId(Guid applicationUserId);
    }
}