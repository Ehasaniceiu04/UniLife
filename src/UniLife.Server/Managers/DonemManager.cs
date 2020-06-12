using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DonemManager : IDonemManager
    {
        private readonly IDonemStore _donemStore;

        public DonemManager(IDonemStore donemStore)
        {
            _donemStore = donemStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved DonemDtos", await _donemStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved DonemDto", await _donemStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve DonemDto");
            }
        }

        public async Task<ApiResponse> Create(DonemDto donemDto)
        {
            var universite = await _donemStore.Create(donemDto);
            return new ApiResponse(Status200OK, "Created DonemDto", universite);
        }

        public async Task<ApiResponse> Update(DonemDto donemDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated DonemDto", await _donemStore.Update(donemDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update DonemDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _donemStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete DonemDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update DonemDto");
            }
        }

        public async Task<ApiResponse> Current()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved DonemDtos", await _donemStore.Current());
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status400BadRequest, ex.Message);
            }
        }
    }
}
