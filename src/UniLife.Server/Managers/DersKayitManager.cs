using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
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
            try
            {
                await _dersKayitStore.OgrenciKayitToDerss(dersKayitDtos);
                return new ApiResponse(Status200OK, "Bulk create result");
            }
            catch (InvalidDataException dataException)
            {
                throw;
            }
            
        }

        public async Task<ApiResponse> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated Todo", await _dersKayitStore.PutUpdateOgrencisDersKayits(putUpdateOgrencisDersKayitsDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update DersKayit");
            }
        }
    }
}
