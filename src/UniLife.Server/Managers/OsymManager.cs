using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class OsymManager : BaseManager<Osym, OsymDto>, IOsymManager
    {
        private readonly IOsymStore _osymStore;
        public OsymManager(IOsymStore osymStore) : base(osymStore)
        {
            _osymStore = osymStore;
        }

        public async Task<ApiResponse> GetByAppId(Guid appId)
        {
            return new ApiResponse(Status200OK, "Retrieved OgrenciDto", await _osymStore.GetByAppId(appId));
        }
    }
}
