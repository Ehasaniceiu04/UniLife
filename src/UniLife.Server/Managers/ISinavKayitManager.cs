using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface ISinavKayitManager
    {
        //Task<ApiResponse> Get();
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(SinavKayitDto sinavKayitDto);
        Task<ApiResponse> Update(SinavKayitDto sinavKayitDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> GetOgrenciNotlar(int ogrenciId);
    }
}