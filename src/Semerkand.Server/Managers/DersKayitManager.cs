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

        public async Task<ApiResponse> DeleteByOgrId_DersId(int ogrenciId, int dersId)
        {
            try
            {
                await _dersKayitStore.DeleteByOgrId_DersId(ogrenciId, dersId);
                return new ApiResponse(Status200OK, "Soft Delete DersKayit");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to delete DersKayit");
            }
        }

        public async Task<ApiResponse> OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos)
        {
            await _dersKayitStore.OgrenciKayitToDerss(dersKayitDtos);
            return new ApiResponse(Status200OK, "Bulk create result");
        }
    }
}
