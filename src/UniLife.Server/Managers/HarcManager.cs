using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class HarcManager : IHarcManager
    {
        private readonly IHarcStore _harcStore;

        public HarcManager(IHarcStore harcStore)
        {
            _harcStore = harcStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved HarcDtos", await _harcStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved HarcDto", await _harcStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve HarcDto");
            }
        }

        public async Task<ApiResponse> Create(HarcDto harcDto)
        {
            var universite = await _harcStore.Create(harcDto);
            return new ApiResponse(Status200OK, "Created HarcDto", universite);
        }

        public async Task<ApiResponse> Update(HarcDto harcDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated HarcDto", await _harcStore.Update(harcDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update HarcDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _harcStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete HarcDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update HarcDto");
            }
        }
    }
}
