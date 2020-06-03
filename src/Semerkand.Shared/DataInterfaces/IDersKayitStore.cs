using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IDersKayitStore : IBaseStore<DersKayit, DersKayitDto>
    {
        Task OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos);
        Task DeleteByOgrId_DersId(int ogrenciId, int dersId);
    }
}
