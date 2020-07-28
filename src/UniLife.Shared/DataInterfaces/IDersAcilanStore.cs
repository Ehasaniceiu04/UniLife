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
        Task<List<DersAcilanDto>> GetAcilanDersByMufredatId(int mufredatId,int sinif,int donemId);
        Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciId(int ogrenciId, int sinif,int donemId);
        Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId);
        Task<List<DersAcilanDto>> ByZorunlu(bool isZorunlu);
        Task<List<SubeDersAcilanDto>> PostDersAcilansByFilters(SinavDersAcDto sinavDersAcDto);
        Task PostCreateNewSubesAndUpdateOgrenciSubes(SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto);
        Task<List<DersAcilanDto>> GetDersAcilanSubelerByDersKod(string dersKod);
        Task<DersAcilanDto> GetDersAcilanSpecByDersAcId(int dersAcilanId);
        Task UpdateDersAcilanAkademsiyen(int dersAcilanId, int akademisyenId);
        Task<List<ResDersAcilansByLongFilters>> DersAcilansByLongFilters(ReqDersAcilansByLongFilters reqDersAcilansByLongFilters);
        Task<List<OgrenciDerslerDto>> GetDersSonucByOgrenciId(int ogrenciId);
    }
}
