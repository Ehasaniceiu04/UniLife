using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IFakulteManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(FakulteDto fakulteDto);
        Task<ApiResponse> Update(FakulteDto fakulteDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> GetOgrCountOfFakultesGYear();
    }
}