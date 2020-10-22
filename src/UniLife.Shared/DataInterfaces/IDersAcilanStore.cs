using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IDersAcilanStore : IBaseStore<DersAcilan, DersAcilanDto>
    {
        //BUlk insering bilgilerini dönebiliriz List<DersAcilan> 
        Task<bool> CreateDersAcilanByDers(DersAcDto dersAcDto);
        Task<List<DersAcilanDto>> GetAcilanDersByFilterDto(DersAcilanFilterDto dersAcilanFilterDto);
        Task<List<DersAcilanDto>> GetAcilanDerssByOgrenciId(int ogrenciId, int pageSinif, int pageDonemId);
        Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciId(int ogrenciId, int donemId);
        Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId);
        Task<List<DersAcilanDto>> ByZorunlu(bool isZorunlu);
        Task<List<SubeDersAcilanDto>> PostDersAcilansByFilters(SinavDersAcDto sinavDersAcDto);
        Task PostCreateNewSubesAndUpdateOgrenciSubes(SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto);
        Task<List<DersAcilanDto>> GetDersAcilanSubelerByDersKod(string dersKod,int donemId,int programId);
        Task<DersAcilanDto> GetDersAcilanSpecByDersAcId(int dersAcilanId);
        Task UpdateDersAcilanAkademsiyen(int dersAcilanId, int akademisyenId);
        Task<List<ResDersAcilansByLongFilters>> DersAcilansByLongFilters(ReqDersAcilansByLongFilters reqDersAcilansByLongFilters);
        Task<List<OgrenciDerslerDto>> GetMufredatDersByOgrenciId(int ogrenciId);
        Task<List<DersAcilanDto>> GetDersAcilansByMufredat(int mufredatId, int donemId,int? sinif);
        Task<List<OgrenciDerslerDto>> GetDonemDersByOgrenciId(int ogrenciId);
    }
}
