using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface IAkademikTakvimManager : IBaseManager<AkademikTakvim, AkademikTakvimDto>
    {
        Task<ApiResponse> GetAkaTakByFakIdDonId(int fakulteId, int donemId);
        Task<ApiResponse> PostChangeAllFakultesTakvim(AkademikTakvimDto akademikTakvimDto);
    }
}
