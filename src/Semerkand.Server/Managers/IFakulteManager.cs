using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.Dto.Sample;

namespace Semerkand.Server.Managers
{
    public interface IFakulteManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(FakulteDto fakulteDto);
        Task<ApiResponse> Update(FakulteDto fakulteDto);
        Task<ApiResponse> Delete(int id);
    }
}