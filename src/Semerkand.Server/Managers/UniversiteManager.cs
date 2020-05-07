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
    public class UniversiteManager : IUniversiteManager
    {
        private readonly IUniversiteStore _universiteStore;

        public UniversiteManager(IUniversiteStore universiteStore)
        {
            _universiteStore = universiteStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved UniversiteDtos", await _universiteStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved UniversiteDto", await _universiteStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve UniversiteDto");
            }
        }

        public async Task<ApiResponse> Create(UniversiteDto universiteDto)
        {
            var universite = await _universiteStore.Create(universiteDto);
            return new ApiResponse(Status200OK, "Created UniversiteDto", universite);
        }

        public async Task<ApiResponse> Update(UniversiteDto universiteDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated UniversiteDto", await _universiteStore.Update(universiteDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update UniversiteDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _universiteStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete UniversiteDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update UniversiteDto");
            }
        }
    }
}
