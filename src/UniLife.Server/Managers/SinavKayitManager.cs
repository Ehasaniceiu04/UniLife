using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class SinavKayitManager : ISinavKayitManager
    {
        private readonly ISinavKayitStore _sinavKayitStore;

        public SinavKayitManager(ISinavKayitStore sinavKayitStore)
        {
            _sinavKayitStore = sinavKayitStore;
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved SinavKayitDtos", await _sinavKayitStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved SinavKayitDto", await _sinavKayitStore.GetById(id));
        }

        public async Task<ApiResponse> Create(SinavKayitDto sinavKayitDto)
        {
            var sinavKayit = await _sinavKayitStore.Create(sinavKayitDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto", sinavKayit);
        }

        public async Task<ApiResponse> Update(SinavKayitDto sinavKayitDto)
        {
            return new ApiResponse(Status200OK, "Updated SinavKayitDto", await _sinavKayitStore.Update(sinavKayitDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _sinavKayitStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete SinavKayitDto");
        }

        public async Task<ApiResponse> GetOgrenciNotlar(int ogrenciId)
        {
            var ogrenciNots = await _sinavKayitStore.GetOgrenciNotlar(ogrenciId);
            return new ApiResponse(Status200OK, "Öğrenci notları getirildi", ogrenciNots);
        }

        public async Task<ApiResponse> GetSinavKayitOgrenciNotlar(int sinavId)
        {
            var ogrenciNots = await _sinavKayitStore.GetSinavKayitOgrenciNotlar(sinavId);
            return new ApiResponse(Status200OK, "Sınav öğrenci notları getirildi", ogrenciNots);
        }

        public async Task<ApiResponse> GetOgrenciSinavsByDers(int ogrenciId, int dersAcilanId)
        {
            var ogrenciNots = await _sinavKayitStore.GetOgrenciSinavsByDers(ogrenciId, dersAcilanId);
            return new ApiResponse(Status200OK, "Sınav öğrenci notları getirildi", ogrenciNots);
        }

        public async Task<ApiResponse> GetSinavKayitsByOgrenciDers(int ogrenciId, int dersAcilanId)
        {
            var ogrenciNots = await _sinavKayitStore.GetSinavKayitsByOgrenciDers(ogrenciId, dersAcilanId);
            return new ApiResponse(Status200OK, "Sınav öğrenci notları getirildi", ogrenciNots);
        }

        public async Task<ApiResponse> UpdateSinavKayit(int sinavkayitId, double orgNot)
        {
            await _sinavKayitStore.UpdateSinavKayit(sinavkayitId, orgNot);
            return new ApiResponse(Status200OK, "Sinav kayıdı güncellendi");
        }

        public async Task<ApiResponse> PutOgrenciSinavKayitNot(OgrenciNotlarDto ogrenciNotlarDto)
        {
            return new ApiResponse(Status200OK, "Updated SinavKayitDto", await _sinavKayitStore.PutOgrenciSinavKayitNot(ogrenciNotlarDto));
        }
    }
}
