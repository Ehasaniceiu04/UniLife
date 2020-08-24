using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class OgrenciDigerManager : BaseManager<OgrenciDiger, OgrenciDigerDto>, IOgrenciDigerManager
    {
        private readonly IOgrenciDigerStore _ogrenciDigerStore;
        public OgrenciDigerManager(IOgrenciDigerStore ogrenciDigerStore) : base(ogrenciDigerStore)
        {
            _ogrenciDigerStore = ogrenciDigerStore;
        }

        public async Task<ApiResponse> PostSaveGecis(OgrenciDigerDto ogrenciDigerDto)
        {
            await _ogrenciDigerStore.PostSaveGecis(ogrenciDigerDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto");
        }
        public async Task<ApiResponse> PostSaveKayitDond(OgrenciDigerDto ogrenciDigerDto)
        {
            await _ogrenciDigerStore.PostSaveKayitDond(ogrenciDigerDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto");
        }
        public async Task<ApiResponse> PostSaveKayitCeza(OgrenciDigerDto ogrenciDigerDto)
        {
            await _ogrenciDigerStore.PostSaveKayitCeza(ogrenciDigerDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto");
        }
        public async Task<ApiResponse> PostSaveKayitStaj(OgrenciDigerDto ogrenciDigerDto)
        {
            await _ogrenciDigerStore.PostSaveKayitStaj(ogrenciDigerDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto");
        }
        public async Task<ApiResponse> PostSaveKayitTez(OgrenciDigerDto ogrenciDigerDto)
        {
            await _ogrenciDigerStore.PostSaveKayitTez(ogrenciDigerDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto");
        }
    }
}
