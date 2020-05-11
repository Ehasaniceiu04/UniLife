using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Managers
{
    public class FakulteTurManager : IFakulteTurManager
    {
        private readonly IFakulteTurStore _fakulteTurStore;

        public FakulteTurManager(IFakulteTurStore fakulteTurStore)
        {
            _fakulteTurStore = fakulteTurStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved FakulteTurDtos", await _fakulteTurStore.GetAll());
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status400BadRequest, ex.Message);
            }
        }

        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved FakulteTurDto", await _fakulteTurStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve FakulteTurDto");
            }
        }

        public async Task<ApiResponse> Create(FakulteTurDto fakulteTurDto)
        {
            var fakulteTur = await _fakulteTurStore.Create(fakulteTurDto);
            return new ApiResponse(Status200OK, "Created FakulteTurDto", fakulteTur);
        }

        public async Task<ApiResponse> Update(FakulteTurDto fakulteTurDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated FakulteTurDto", await _fakulteTurStore.Update(fakulteTurDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update FakulteTurDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _fakulteTurStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete FakulteTurDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update FakulteTurDto");
            }
        }
    }
}
