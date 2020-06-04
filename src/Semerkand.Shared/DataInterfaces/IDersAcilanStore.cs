using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IDersAcilanStore : IBaseStore<DersAcilan, DersAcilanDto>
    {
        //BUlk insering bilgilerini dönebiliriz List<DersAcilan> 
        Task<bool> CreateDersAcilanByDers(DersAcDto dersAcDto);
        Task<List<DersAcilanDto>> GetAcilanDersByFilterDto(DersAcilanFilterDto dersAcilanFilterDto);
        Task<List<DersAcilanDto>> GetAcilanDersByMufredatId(int mufredatId);
        Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciId(int ogrenciId, int sinif,int donemId);
        Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId);
        Task<List<DersAcilanDto>> ByZorunlu(bool isZorunlu);
    }
}
