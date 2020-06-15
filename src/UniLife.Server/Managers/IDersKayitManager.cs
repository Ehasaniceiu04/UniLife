﻿using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IDersKayitManager : IBaseManager<DersKayit, DersKayitDto>
    {
        Task<ApiResponse> OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos);
        Task<ApiResponse> DeleteByOgrId_DersId(int ogrenciId, int dersId);
    }
}