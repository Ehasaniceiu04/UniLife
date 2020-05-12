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
    public class DonemTipManager : BaseManager<DonemTip,DonemTipDto>, IDonemTipManager
    {
        public DonemTipManager(IDonemTipStore donemTipStore) : base(donemTipStore)
        {

        }

        //private readonly IDonemTipStore _donemTipStore;

        //public DonemTipManager(IDonemTipStore donemTipStore)
        //{
        //    _donemTipStore = donemTipStore;
        //}

        //public async Task<ApiResponse> Get()
        //{
        //    try
        //    {
        //        return new ApiResponse(Status200OK, "Retrieved DonemTipDtos", await _donemTipStore.GetAll());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse(Status400BadRequest, ex.Message);
        //    }
        //}

        //public async Task<ApiResponse> Get(int id)
        //{
        //    try
        //    {
        //        return new ApiResponse(Status200OK, "Retrieved DonemTipDto", await _donemTipStore.GetById(id));
        //    }
        //    catch (Exception e)
        //    {
        //        return new ApiResponse(Status400BadRequest, "Failed to Retrieve DonemTipDto");
        //    }
        //}

        //public async Task<ApiResponse> Create(DonemTipDto donemTipDto)
        //{
        //    var universite = await _donemTipStore.Create(donemTipDto);
        //    return new ApiResponse(Status200OK, "Created DonemTipDto", universite);
        //}

        //public async Task<ApiResponse> Update(DonemTipDto donemTipDto)
        //{
        //    try
        //    {
        //        return new ApiResponse(Status200OK, "Updated DonemTipDto", await _donemTipStore.Update(donemTipDto));
        //    }
        //    catch (InvalidDataException dataException)
        //    {
        //        return new ApiResponse(Status400BadRequest, "Failed to update DonemTipDto");
        //    }
        //}

        //public async Task<ApiResponse> Delete(int id)
        //{
        //    try
        //    {
        //        await _donemTipStore.DeleteById(id);
        //        return new ApiResponse(Status200OK, "Soft Delete DonemTipDto");
        //    }
        //    catch (InvalidDataException dataException)
        //    {
        //        return new ApiResponse(Status400BadRequest, "Failed to update DonemTipDto");
        //    }
        //}
    }
}
