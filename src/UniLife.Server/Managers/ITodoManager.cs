using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Sample;

namespace UniLife.Server.Managers
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