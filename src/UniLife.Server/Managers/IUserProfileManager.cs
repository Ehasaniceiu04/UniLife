using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Account;

namespace UniLife.Server.Managers
{
    public interface IUserProfileManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Upsert(UserProfileDto userProfile);
        Task<string> GetLastPageVisited(string userName);
        Task<ApiResponse> GetAkademisyenState();
        Task<ApiResponse> GetOgrenciState();
        Task<ApiResponse> GetDonemState();
    }
}