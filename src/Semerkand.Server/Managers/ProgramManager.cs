using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Managers
{
    public class ProgramManager : IProgramManager
    {
        private readonly IProgramStore _programStore;

        public ProgramManager(IProgramStore programStore)
        {
            _programStore = programStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved ProgramDtos", await _programStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved ProgramDto", await _programStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve ProgramDto");
            }
        }

        public async Task<ApiResponse> Create(ProgramDto programDto)
        {
            var universite = await _programStore.Create(programDto);
            return new ApiResponse(Status200OK, "Created ProgramDto", universite);
        }

        public async Task<ApiResponse> Update(ProgramDto programDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated ProgramDto", await _programStore.Update(programDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update ProgramDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _programStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete ProgramDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update ProgramDto");
            }
        }
    }
}
