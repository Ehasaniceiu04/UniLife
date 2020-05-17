using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Managers
{
    public class MufredatManager : IMufredatManager
    {
        private readonly IMufredatStore _mufredatStore;

        public MufredatManager(IMufredatStore mufredatStore)
        {
            _mufredatStore = mufredatStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved MufredatDto", await _mufredatStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve MufredatDto");
            }
        }

        public async Task<ApiResponse> Create(MufredatDto mufredatDto)
        {
            var universite = await _mufredatStore.Create(mufredatDto);
            return new ApiResponse(Status200OK, "Created MufredatDto", universite);
        }

        public async Task<ApiResponse> Update(MufredatDto mufredatDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated MufredatDto", await _mufredatStore.Update(mufredatDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update MufredatDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _mufredatStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete MufredatDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update MufredatDto");
            }
        }

        public async Task<ApiResponse> Cokla(int id)
        {
            try
            {
                await _mufredatStore.Cokla(id);
                return new ApiResponse(Status200OK, "Cokla MufredatDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Cokla MufredatDto");
            }
        }

        public async Task<ApiResponse> GetMufredatByProgramIds(string[] programIds)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetMufredatByProgramIds(programIds));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve MufredatDtos");
            }
        }
    }
}
