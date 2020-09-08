using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
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
            await _dersKayitStore.DeleteByOgrId_DersId(ogrenciId, dersId);
            return new ApiResponse(Status200OK, "Soft Delete DersKayit");

        }

        public async Task<ApiResponse> OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos)
        {
            await _dersKayitStore.OgrenciKayitToDerss(dersKayitDtos);
            return new ApiResponse(Status200OK, "Bulk create result");


        }

        public async Task<ApiResponse> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto)
        {
            return new ApiResponse(Status200OK, "Updated PutUpdateOgrencisDersKayits", await _dersKayitStore.PutUpdateOgrencisDersKayits(putUpdateOgrencisDersKayitsDto));

        }

        public async Task<ApiResponse> PutUpdateOgrencisDersKayitsDeleteExSubes(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            return new ApiResponse(Status200OK, "Updated PutUpdateOgrencisDersKayitsDeleteExSubes", await _dersKayitStore.PutUpdateOgrencisDersKayitsDeleteExSubes(reqEntityIdWithOtherEntitiesIds));

        }

        public async Task<ApiResponse> GetOgrenciDersKayitsByDers(int dersAcilanId)
        {
            var ogrenciDersKayits = await _dersKayitStore.GetOgrenciDersKayitsByDers(dersAcilanId);
            return new ApiResponse(Status200OK, "GetOgrenciDersKayitsByDers fetched", ogrenciDersKayits);
        }

        public async Task<ApiResponse> HedefKaynakOgrAktar(HedefKaynakDto hedefKaynakDto)
        {
            await _dersKayitStore.HedefKaynakOgrAktar(hedefKaynakDto);
            return new ApiResponse(Status200OK, "Güncellendi HedefKaynakOgrAktar");
        }

        public async Task<ApiResponse> HedefKaynakOgrDersKayit(HedefKaynakDto hedefKaynakDto)
        {
            await _dersKayitStore.HedefKaynakOgrDersKayit(hedefKaynakDto);
            return new ApiResponse(Status200OK, "Güncellendi HedefKaynakOgrAktar");
        }
    }
}
