using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Sample;

namespace Semerkand.Server.Managers
{
    public interface ITodoManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(long id);
        Task<ApiResponse> Create(TodoDto todo);
        Task<ApiResponse> Update(TodoDto todo);
        Task<ApiResponse> Delete(long id);
    }
}