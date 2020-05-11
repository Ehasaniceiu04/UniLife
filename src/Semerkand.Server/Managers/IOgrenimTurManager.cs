using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IOgrenimTurManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(OgrenimTurDto ugrenimTurDto);
        Task<ApiResponse> Update(OgrenimTurDto ugrenimTurDto);
        Task<ApiResponse> Delete(int id);
    }
}
