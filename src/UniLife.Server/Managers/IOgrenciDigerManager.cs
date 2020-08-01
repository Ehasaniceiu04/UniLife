using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface IOgrenciDigerManager : IBaseManager<OgrenciDiger, OgrenciDigerDto>
    {
        Task<ApiResponse> PostSaveGecis(OgrenciDigerDto ogrenciDigerDto);
        Task<ApiResponse> PostSaveKayitDond(OgrenciDigerDto ogrenciDigerDto);
        Task<ApiResponse> PostSaveKayitCeza(OgrenciDigerDto ogrenciDigerDto);
        Task<ApiResponse> PostSaveKayitStaj(OgrenciDigerDto ogrenciDigerDto);
        Task<ApiResponse> PostSaveKayitTez(OgrenciDigerDto ogrenciDigerDto);
    }
}
