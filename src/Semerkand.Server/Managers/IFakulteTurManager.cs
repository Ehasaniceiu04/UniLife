using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.Dto.Sample;

namespace Semerkand.Server.Managers
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