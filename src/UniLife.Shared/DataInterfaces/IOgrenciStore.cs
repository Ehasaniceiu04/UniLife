using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IOgrenciStore : IBaseStore<Ogrenci, OgrenciDto>
    {
        Task<OgrenciDto> GetOgrenciWithRelations(int id);
        Task<List<OgrenciDto>> GetOgrenciQuery(OgrenciDto ogrenci);
    }
}
