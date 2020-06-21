using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class SinavManager : BaseManager<Sinav, SinavDto>, ISinavManager
    {
        private readonly ISinavStore _sinavStore;
        public SinavManager(ISinavStore sinavStore) : base(sinavStore)
        {
            _sinavStore = sinavStore;
        }

        

        public async Task<ApiResponse> GetSinavListByAcilanDersId(int dersId)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved SinavDto", await _sinavStore.GetSinavListByAcilanDersId(dersId));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve SinavDto");
            }
        }
        public async Task<ApiResponse> PostBulkCreate(SinavDto sinavDto)
        {
            await _sinavStore.PostBulkCreate(sinavDto);
            return new ApiResponse(Status200OK, "Created Sinavs", null);
        }
        
    }
}
