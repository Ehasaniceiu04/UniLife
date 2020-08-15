using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IDonemManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(DonemDto donemDto);
        Task<ApiResponse> Update(DonemDto donemDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> Current();
        Task<ApiResponse> CreateNewDonemWithTakvim(DonemDto donemDto);
    }
}
