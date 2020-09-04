using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IDersKayitStore : IBaseStore<DersKayit, DersKayitDto>
    {
        Task OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos);
        Task DeleteByOgrId_DersId(int ogrenciId, int dersId);
        Task<int> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto);
        Task<int> PutUpdateOgrencisDersKayitsDeleteExSubes(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task<List<OgrenciDersKayitDto>> GetOgrenciDersKayitsByDers(int dersAcilanId);
        Task HedefKaynakOgrAktar(HedefKaynakDto hedefKaynakDto);
    }
}
