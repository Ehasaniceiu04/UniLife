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
    public class SinavKayitManager : ISinavKayitManager
    {
        private readonly ISinavKayitStore _sinavKayitStore;

        public SinavKayitManager(ISinavKayitStore sinavKayitStore)
        {
            _sinavKayitStore = sinavKayitStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved SinavKayitDtos", await _sinavKayitStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved SinavKayitDto", await _sinavKayitStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve SinavKayitDto");
            }
        }

        public async Task<ApiResponse> Create(SinavKayitDto sinavKayitDto)
        {
            var sinavKayit = await _sinavKayitStore.Create(sinavKayitDto);
            return new ApiResponse(Status200OK, "Created SinavKayitDto", sinavKayit);
        }

        public async Task<ApiResponse> Update(SinavKayitDto sinavKayitDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated SinavKayitDto", await _sinavKayitStore.Update(sinavKayitDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update SinavKayitDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _sinavKayitStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete SinavKayitDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update SinavKayitDto");
            }
        }
    }
}
