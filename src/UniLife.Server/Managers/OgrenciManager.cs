using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Definitions.Bussines;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class OgrenciManager : BaseManager<Ogrenci, OgrenciDto>, IOgrenciManager
    {

        private readonly IOgrenciStore _ogrenciStore;
        public OgrenciManager(IOgrenciStore ogrenciStore) : base(ogrenciStore)
        {
            _ogrenciStore = ogrenciStore;

        }

        public async Task<ApiResponse> GetOgrenciQuery(OgrenciDto ogrenci)
        {
            return new ApiResponse(Status200OK, "Retrieved OgrenciDto", await _ogrenciStore.GetOgrenciQuery(ogrenci));
        }

        public async Task<ApiResponse> GetOgrenciWithRelations(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved OgrenciDto", await _ogrenciStore.GetOgrenciWithRelations(id));
        }

        public async Task<ApiResponse> GetOgrenciListBySinavId(int sinavId)
        {
            return new ApiResponse(Status200OK, "Retrieved OgrenciDtos", await _ogrenciStore.GetOgrenciListBySinavId(sinavId));
        }

        public async Task<ApiResponse> GetOgrenciListByDersAcId(int dersAcId)
        {
            return new ApiResponse(Status200OK, "Retrieved OgrenciDtos", await _ogrenciStore.GetOgrenciListByDersAcId(dersAcId));
        }

        public async Task<ApiResponse> SetDanismanToOgrencis(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            await _ogrenciStore.SetDanismanToOgrencis(reqEntityIdWithOtherEntitiesIds);
            return new ApiResponse(Status200OK, "SetDanismanToOgrencis done!", null);
        }

        public async Task<ApiResponse> SetMufredatToOgrencis(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            await _ogrenciStore.SetMufredatToOgrencis(reqEntityIdWithOtherEntitiesIds);
            return new ApiResponse(Status200OK, "SetMufredatToOgrencis done!", null);
        }
        public async Task<ApiResponse> SetOgrDurumToOgrencis(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            await _ogrenciStore.SetOgrDurumToOgrencis(reqEntityIdWithOtherEntitiesIds);
            return new ApiResponse(Status200OK, "SetOgrDurumToOgrencis done!", null);
        }

        public async Task<ApiResponse> OgrencisSinifAtlat(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            await _ogrenciStore.OgrencisSinifAtlat(reqEntityIdWithOtherEntitiesIds);
            return new ApiResponse(Status200OK, "OgrencisSinifAtlat done!", null);
        }

        public async Task<ApiResponse> SinifAtlaTemizle(HedefKaynakDto hedefKaynakDto)
        {
            await _ogrenciStore.SinifAtlaTemizle(hedefKaynakDto);
            return new ApiResponse(Status200OK, "SinifAtlaTemizle done!");
        }

        public async Task<ApiResponse> GetOgrInfos(string kullaniciId)
        {
            return new ApiResponse(Status200OK, "Öğrenci bilgileri getirildi", await _ogrenciStore.GetOgrInfos(kullaniciId));
        }

        public async Task<ApiResponse> GetOgrenciBelgesi(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved OgrenciDto", await _ogrenciStore.GetOgrenciBelgesi(id));
        }

        public async Task<ApiResponse> UpdateOgrenciOnayBekle(MazunOnayDto mazunOnayDto)
        {
            await _ogrenciStore.UpdateOgrenciOnayBekle(mazunOnayDto);
            return new ApiResponse(Status200OK, "UpdateOgrenciOnayBekle done!", null);
        }

        public async Task<ApiResponse> UpdateOnay(int ogrId, int onayNo)
        {
            await _ogrenciStore.UpdateOnay(ogrId, onayNo);
            return new ApiResponse(Status200OK, "UpdateOnay done!");
        }
    }
}
