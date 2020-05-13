using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IDersManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(DersDto dersDto);
        Task<ApiResponse> Update(DersDto dersDto);
        Task<ApiResponse> Delete(int id);
    }
}
