using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Sample;

namespace UniLife.Server.Managers
{
    public interface IUniversiteManager
    {
        //Task<ApiResponse> Get();
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(UniversiteDto universiteDto);
        Task<ApiResponse> Update(UniversiteDto universiteDto);
        Task<ApiResponse> Delete(int id);
    }
}