using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IDersAcilanManager : IBaseManager<DersAcilan, DersAcilanDto>
    {
        Task<ApiResponse> CreateDersAcilanByDers(DersAcDto dersAcDto);
        Task<ApiResponse> GetAcilanDersByFilterDto(DersAcilanFilterDto dersAcilanFilterDto);
        Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId,int sinif,int donemId);
        Task<ApiResponse> GetKayitliDerssByOgrenciId(int ogrenciId, int sinif,int donemId);
        Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId);
        Task<ApiResponse> ByZorunlu(bool isZorunlu);
        Task<ApiResponse> PostDersAcilansByFilters(SinavDersAcDto sinavDersAcDto);
        Task<ApiResponse> PostCreateNewSubesAndUpdateOgrenciSubes(SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto);
        Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(string dersKod);
        Task<ApiResponse> GetDersAcilanSpecByDersAcId(int dersAcilanId);
        Task<ApiResponse> UpdateDersAcilanAkademsiyen(int dersAcilanId, int akademisyenId);
    }
}
