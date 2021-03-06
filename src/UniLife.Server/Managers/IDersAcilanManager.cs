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
        Task<ApiResponse> GetAcilanDerssByOgrenciId(int ogrenciId, int pageSinif, int pageDonemId);
        Task<ApiResponse> GetKayitliDerssByOgrenciId(int ogrenciId, int donemId);
        Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId);
        Task<ApiResponse> ByZorunlu(bool isZorunlu);
        Task<ApiResponse> PostDersAcilansByFilters(SinavDersAcDto sinavDersAcDto);
        Task<ApiResponse> PostCreateNewSubesAndUpdateOgrenciSubes(SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto);
        Task<ApiResponse> GetDersAcilanSubelerByDersKod(string dersKod,int donemId,int programId);
        Task<ApiResponse> GetDersAcilanSpecByDersAcId(int dersAcilanId);
        Task<ApiResponse> UpdateDersAcilanAkademsiyen(int dersAcilanId, int akademisyenId);
        Task<ApiResponse> DersAcilansByLongFilters(ReqDersAcilansByLongFilters reqDersAcilansByLongFilters);
        Task<ApiResponse> GetMufredatDersByOgrenciId(int ogrenciId);
        Task<ApiResponse> GetDersAcilansByMufredat(int mufredatId, int donemId,int? sinif);
        Task<ApiResponse> GetDonemDersByOgrenciId(int ogrenciId);
    }
}
