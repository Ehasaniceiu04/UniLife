using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UniLife.Server.Managers
{
    public interface IOgrenciManager : IBaseManager<Ogrenci, OgrenciDto>
    {
        Task<ApiResponse> GetOgrenciWithRelations(int id);
        Task<ApiResponse> GetOgrenciQuery(OgrenciDto ogrenci);
        Task<ApiResponse> GetOgrenciListBySinavId(int sinavId);
        Task<ApiResponse> GetOgrenciListByDersAcId(int dersAcId);
        Task<ApiResponse> SetDanismanToOgrencis(ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds);
        Task<ApiResponse> SetMufredatToOgrencis(ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds);
        Task<ApiResponse> OgrencisSinifAtlat(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task<ApiResponse> SetOgrDurumToOgrencis(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task<ApiResponse> SinifAtlaTemizle(HedefKaynakDto hedefKaynakDto);
        Task<ApiResponse> GetOgrInfos(string kullaniciId);
    }
}
