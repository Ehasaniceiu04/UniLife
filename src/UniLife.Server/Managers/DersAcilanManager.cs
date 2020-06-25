using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DersAcilanManager : BaseManager<DersAcilan, DersAcilanDto>, IDersAcilanManager
    {

        private readonly IDersAcilanStore _dersAcilanStore;
        public DersAcilanManager(IDersAcilanStore dersAcilanStore) : base(dersAcilanStore)
        {
            _dersAcilanStore = dersAcilanStore;
            
        }

        public async Task<ApiResponse> ByZorunlu(bool isZorunlu)
        {
            var dersAcilans = await _dersAcilanStore.ByZorunlu(isZorunlu);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }

        public async Task<ApiResponse> CreateDersAcilanByDers(DersAcDto dersAcDto)
        {
            var boolResult = await _dersAcilanStore.CreateDersAcilanByDers(dersAcDto);
            return new ApiResponse(Status200OK, "Bulk create result", boolResult);
        }

        public async Task<ApiResponse> GetAcilanDersByFilterDto(DersAcilanFilterDto dersAcilanFilterDto)
        {
            var dersAcilans = await _dersAcilanStore.GetAcilanDersByFilterDto(dersAcilanFilterDto);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }

        public async Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId,int sinif,int donemId)
        {
            var dersAcilans = await _dersAcilanStore.GetAcilanDersByMufredatId(mufredatId, sinif, donemId);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }

        public async Task<ApiResponse> PostDersAcilansByFilters(SinavDersAcDto sinavDersAcDto)
        {
            var dersAcilans = await _dersAcilanStore.PostDersAcilansByFilters(sinavDersAcDto);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }
        public async Task<ApiResponse> PostCreateNewSubesAndUpdateOgrenciSubes(SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto)
        {
            await _dersAcilanStore.PostCreateNewSubesAndUpdateOgrenciSubes(subeDersAcilanOgrenciCreateDto);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto");
        }
        

        public async Task<ApiResponse> GetKayitliDerssByOgrenciId(int ogrenciId, int sinif,int donemId)
        {
            var dersAcilans = await _dersAcilanStore.GetKayitliDerssByOgrenciId(ogrenciId,sinif, donemId);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }

        public async Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId)
        {
            var dersAcilans = await _dersAcilanStore.GetKayitliDerssByOgrenciIdDonemId(ogrenciId, donemId);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }

        public async Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(string dersKod)
        {
            var dersAcilans = await _dersAcilanStore.GetKayitliDerssByOgrenciIdDonemId(dersKod);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }

        public async Task<ApiResponse> GetDersAcilanSpecByDersAcId(int dersAcilanId)
        {
            var dersAcilan = await _dersAcilanStore.GetDersAcilanSpecByDersAcId(dersAcilanId);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilan);
        }

        
    }
}
