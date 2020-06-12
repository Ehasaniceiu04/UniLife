using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IFakulteTurManager
    {
        //Task<ApiResponse> Get();
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(FakulteTurDto fakulteTurDto);
        Task<ApiResponse> Update(FakulteTurDto fakulteTurDto);
        Task<ApiResponse> Delete(int id);
    }
}