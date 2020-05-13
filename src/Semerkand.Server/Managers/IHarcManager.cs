using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IHarcManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(HarcDto harcDto);
        Task<ApiResponse> Update(HarcDto harcDto);
        Task<ApiResponse> Delete(int id);
    }
}
