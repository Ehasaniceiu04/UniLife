using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IOgrenciManager : IBaseManager<Ogrenci, OgrenciDto>
    {
        Task<ApiResponse> GetOgrenciWithRelations(int id);
        Task<ApiResponse> GetOgrenciQuery(OgrenciDto ogrenci);
        Task<ApiResponse> GetOgrenciListBySinavId(int sinavId);
        Task<ApiResponse> GetOgrenciListByDersAcId(int dersAcId);
    }
}
