using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Managers
{
    public class DersAcilanManager : BaseManager<DersAcilan, DersAcilanDto>, IDersAcilanManager
    {

        private readonly IDersAcilanStore _dersAcilanStore;
        public DersAcilanManager(IDersAcilanStore dersAcilanStore) : base(dersAcilanStore)
        {
            _dersAcilanStore = dersAcilanStore;
            
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

        public async Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId)
        {
            var dersAcilans = await _dersAcilanStore.GetAcilanDersByMufredatId(mufredatId);
            return new ApiResponse(Status200OK, "Selected DersAcilanDto", dersAcilans);
        }
    }
}
