using System;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IOsymStore : IBaseStore<Osym, OsymDto>
    {
        Task<OsymDto> GetByAppId(Guid appId);
    }
}
