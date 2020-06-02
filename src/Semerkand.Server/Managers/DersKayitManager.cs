using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Managers
{
    public class DersKayitManager : BaseManager<DersKayit, DersKayitDto>, IDersKayitManager
    {

        private readonly IDersKayitStore _dersKayitStore;
        public DersKayitManager(IDersKayitStore dersKayitStore) : base(dersKayitStore)
        {
            _dersKayitStore = dersKayitStore;
            
        }

        public async Task<ApiResponse> OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos)
        {
            await _dersKayitStore.OgrenciKayitToDerss(dersKayitDtos);
            return new ApiResponse(Status200OK, "Bulk create result");
        }
    }
}
