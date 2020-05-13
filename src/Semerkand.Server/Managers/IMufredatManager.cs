using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IMufredatManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(MufredatDto mufredatDto);
        Task<ApiResponse> Update(MufredatDto mufredatDto);
        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> Cokla(int id);
    }
}
