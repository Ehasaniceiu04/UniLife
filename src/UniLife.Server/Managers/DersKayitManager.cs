using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DersKayitManager : BaseManager<DersKayit, DersKayitDto>, IDersKayitManager
    {

        private readonly IDersKayitStore _dersKayitStore;
        private readonly ILogger<AccountManager> _logger;
        public DersKayitManager(IDersKayitStore dersKayitStore, ILogger<AccountManager> logger) : base(dersKayitStore)
        {
            _dersKayitStore = dersKayitStore;
            _logger = logger;
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

        public async Task<ApiResponse> Onayla(List<int> ids)
        {
            await _dersKayitStore.Onayla(ids);
            return new ApiResponse(Status200OK, "Kayıtlı dersler onaylandı.");
        }
        public async Task<ApiResponse> OnayKaldir(List<int> ids)
        {
            await _dersKayitStore.OnayKaldir(ids);
            return new ApiResponse(Status200OK, "Kayıtlı derslerin onayı kaldırıldı.");
        }

        public async Task<ApiResponse> Harflendir(int dersAcilanId)
        {
            try
            {
                await _dersKayitStore.Harflendir(dersAcilanId);
                return new ApiResponse(Status200OK, "Öğrenci ders sonuçları harflendirildi.");
            }
            catch (DomainException ex)
            {
                _logger.LogError("Harflendirme hatası: {0}, {1}", ex.Description, ex.Message);
                return new ApiResponse(Status400BadRequest, $"Harflendirme hatası: {ex.Description} ");
            }
        }

        public async Task<ApiResponse> KurulHarflendir(int dersAcilanId)
        {
            try
            {
                await _dersKayitStore.KurulHarflendir(dersAcilanId);
                return new ApiResponse(Status200OK, "Öğrenci ders sonuçları harflendirildi.");
            }
            catch (DomainException ex)
            {
                _logger.LogError(" Kurul Harflendirme hatası: {0}, {1}", ex.Description, ex.Message);
                return new ApiResponse(Status400BadRequest, $"Kurul Harflendirme hatası: {ex.Description} ");
            }
        }

        public async Task<ApiResponse> GetOgrDersHarfs(int dersAcilanId)
        {
            return new ApiResponse(Status200OK, "Öğrenci ders sonuçları harflendirildi.", await _dersKayitStore.GetOgrDersHarfs(dersAcilanId));
        }
        public async Task<ApiResponse> GetOgrKurulSonDersHarfs(int dersAcilanId)
        {
            return new ApiResponse(Status200OK, "Öğrenci kurul ders sonuçları harflendirildi.", await _dersKayitStore.GetOgrKurulSonDersHarfs(dersAcilanId));
        }

        

        public async Task<ApiResponse> ButHarflendir(int dersAcilanId)
        {
            await _dersKayitStore.ButHarflendir(dersAcilanId);
            return new ApiResponse(Status200OK, "Öğrenci ders sonuçları harflendirildi.");
        }


    }
}
