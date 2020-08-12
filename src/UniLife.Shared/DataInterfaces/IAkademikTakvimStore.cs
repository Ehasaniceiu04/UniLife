using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IAkademikTakvimStore : IBaseStore<AkademikTakvim, AkademikTakvimDto>
    {
        Task<List<AkademikTakvimDto>> GetAkaTakByFakIdDonId(int fakulteId, int donemId);
        Task<int> PostChangeAllFakultesTakvim(AkademikTakvimDto akademikTakvimDto);
    }
}
