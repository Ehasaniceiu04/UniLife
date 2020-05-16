using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Managers
{
    public class BolumManager : IBolumManager
    {
        private readonly IBolumStore _bolumStore;

        public BolumManager(IBolumStore bolumStore)
        {
            _bolumStore = bolumStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved BolumDtos", await _bolumStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved BolumDto", await _bolumStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve BolumDto");
            }
        }

        public async Task<ApiResponse> Create(BolumDto bolumDto)
        {
            var universite = await _bolumStore.Create(bolumDto);
            return new ApiResponse(Status200OK, "Created BolumDto", universite);
        }

        public async Task<ApiResponse> Update(BolumDto bolumDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated BolumDto", await _bolumStore.Update(bolumDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update BolumDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _bolumStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete BolumDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update BolumDto");
            }
        }

        public async Task<ApiResponse> GetDersByMufredatId(string[] fakulteIds)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved BolumDtos", await _bolumStore.GetDersByMufredatId(fakulteIds));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve BolumDtos");
            }
        }
    }
}
