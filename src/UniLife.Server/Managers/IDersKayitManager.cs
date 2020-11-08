using UniLife.Server.Middleware.Wrappers;
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
        Task<ApiResponse> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto);
        Task<ApiResponse> PutUpdateOgrencisDersKayitsDeleteExSubes(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task<ApiResponse> GetOgrenciDersKayitsByDers(int dersAcilanId);
        Task<ApiResponse> HedefKaynakOgrAktar(HedefKaynakDto hedefKaynakDto);
        Task<ApiResponse> HedefKaynakOgrDersKayit(HedefKaynakDto hedefKaynakDto);
        Task<ApiResponse> Onayla(List<int> ids);
        Task<ApiResponse> OnayKaldir(List<int> ids);
        Task<ApiResponse> Harflendir( int dersAcilanId);
        Task<ApiResponse> KurulHarflendir(int dersAcilanId);

        Task<ApiResponse> GetOgrDersHarfs(int dersAcilanId);
        Task<ApiResponse> ButHarflendir(int dersAcilanId);
        
    }
}
