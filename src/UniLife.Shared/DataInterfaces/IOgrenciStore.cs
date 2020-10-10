using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace UniLife.Shared.DataInterfaces
{
    public interface IOgrenciStore : IBaseStore<Ogrenci, OgrenciDto>
    {
        Task<OgrenciDto> GetOgrenciWithRelations(int id);
        Task<List<OgrenciDto>> GetOgrenciQuery(OgrenciDto ogrenci);
        Task<List<OgrenciDto>> GetOgrenciListBySinavId(int sinavId);
        Task<List<OgrenciDto>> GetOgrenciListByDersAcId(int dersAcId);
        Task SetDanismanToOgrencis(ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds);
        Task SetMufredatToOgrencis(ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds);
        Task SetOgrDurumToOgrencis(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task OgrencisSinifAtlat(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);

        Task<long> GetLastOgrNo(int fakId,int BolId);
        Task<OgrenciDto> GetOgrenciState(Guid userId);
        Task SinifAtlaTemizle(HedefKaynakDto hedefKaynakDto);
        Task<OgrenciInfoDto> GetOgrInfos(string kullaniciId);
        Task<OgrenciBelgesiDto> GetOgrenciBelgesi(int id);
    }
}
