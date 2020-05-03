using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Account;

namespace Semerkand.Server.Managers
{
    public interface IUserProfileManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Upsert(UserProfileDto userProfile);
        Task<string> GetLastPageVisited(string userName);
    }
}