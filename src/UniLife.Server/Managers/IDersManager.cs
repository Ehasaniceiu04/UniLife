using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IDersManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(DersDto dersDto);
        Task<ApiResponse> Update(DersDto dersDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> GetDersByMufredatId(int mufredatId);
        Task<ApiResponse> GetAcilacakDersByFilterDto(DersFilterDto dersFilterDto);
        Task<ApiResponse> CreateDersAcilansByDersId(int dersId);
    }
}
