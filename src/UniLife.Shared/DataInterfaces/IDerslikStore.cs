using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IDerslikStore : IBaseStore<Derslik, DerslikDto>
    {
        Task<DersliksAndDerslikRezervsDto> GetDersliksAndDerslikRezsByMufredatId(int mufredatId, int ogrenciId, bool isSinav);
        Task<DersliksAndDerslikRezervsDto> GetDersliksAndDerslikRezsByAkaId(int akaId, bool isSinav);
        
    }
}
