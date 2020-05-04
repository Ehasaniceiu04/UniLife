using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.Dto.Sample;

namespace Semerkand.Server.Managers
{
    public interface IUniversiteManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(UniversiteDto universiteDto);
        Task<ApiResponse> Update(UniversiteDto universiteDto);
        Task<ApiResponse> Delete(int id);
    }
}