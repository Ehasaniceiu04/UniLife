using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class OgrenimTurManager : IOgrenimTurManager
    {
        private readonly IOgrenimTurStore _ogrenimTurStore;

        public OgrenimTurManager(IOgrenimTurStore ogrenimTurStore)
        {
            _ogrenimTurStore = ogrenimTurStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved OgrenimTurDtos", await _ogrenimTurStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved OgrenimTurDto", await _ogrenimTurStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve OgrenimTurDto");
            }
        }

        public async Task<ApiResponse> Create(OgrenimTurDto ogrenimTurDto)
        {
            var ogrenimTur = await _ogrenimTurStore.Create(ogrenimTurDto);
            return new ApiResponse(Status200OK, "Created OgrenimTurDto", ogrenimTur);
        }

        public async Task<ApiResponse> Update(OgrenimTurDto ogrenimTurDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated OgrenimTurDto", await _ogrenimTurStore.Update(ogrenimTurDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update OgrenimTurDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _ogrenimTurStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete OgrenimTurDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update OgrenimTurDto");
            }
        }
    }
}
