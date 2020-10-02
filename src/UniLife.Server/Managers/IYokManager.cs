using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IYokManager
    {
        Task<ApiResponse> AskerlikErtelemeTalepGonder();
    }
}