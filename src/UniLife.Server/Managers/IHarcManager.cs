using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
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
