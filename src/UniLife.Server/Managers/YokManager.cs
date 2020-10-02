using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class YokManager : IYokManager
    {
        private readonly IYokStore _yokStore;

        public YokManager(IYokStore yokStore)
        {
            _yokStore = yokStore;
        }

        public async Task<ApiResponse> AskerlikErtelemeTalepGonder()
        {
            var askerlikErt = await _yokStore.AskerlikErtelemeTalepGonder();
            return new ApiResponse(Status200OK, "Talep GÖnderildi", askerlikErt);
        }

    }
}
