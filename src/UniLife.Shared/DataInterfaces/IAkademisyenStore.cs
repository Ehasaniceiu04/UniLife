using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;
using System;

namespace UniLife.Shared.DataInterfaces
{
    public interface IAkademisyenStore : IBaseStore<Akademisyen, AkademisyenDto>
    {
        Task<AkademisyenDto> GetAkademisyenState(Guid userId);
        Task<long> GetLastAkaNo();
    }
}
