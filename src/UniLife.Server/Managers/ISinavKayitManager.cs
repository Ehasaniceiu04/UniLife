using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface ISinavKayitManager
    {
        //Task<ApiResponse> Get();
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(SinavKayitDto sinavKayitDto);
        Task<ApiResponse> Update(SinavKayitDto sinavKayitDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> GetOgrenciNotlar(int ogrenciId);
        Task<ApiResponse> GetSinavKayitOgrenciNotlar(int sinavId, int dersAcilanId);
        Task<ApiResponse> GetOgrenciSinavsByDers(int ogrenciId, int dersAcilanId);
        Task<ApiResponse> GetSinavKayitsByOgrenciDers(int ogrenciId, int dersAcilanId);
        Task<ApiResponse> UpdateSinavKayit(int sinavkayitId, double orgNot);
        Task<ApiResponse> PutOgrenciSinavKayitNot(OgrenciNotlarDto ogrenciNotlarDto);
        Task<ApiResponse> PutAkaOgrenciSinavKayitNot(SinavOgrNotlarDto sinavOgrNotlarDto);
        Task<ApiResponse> UpdateOgrNotsBatch(System.Collections.Generic.List<SinavKayitNotBatch> sinavKayitNotBatches);

    }
}