using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class FakulteManager : IFakulteManager
    {
        private readonly IFakulteStore _fakulteStore;

        public FakulteManager(IFakulteStore fakulteStore)
        {
            _fakulteStore = fakulteStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved FakulteDtos", await _fakulteStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved FakulteDto", await _fakulteStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve FakulteDto");
            }
        }

        public async Task<ApiResponse> Create(FakulteDto fakulteDto)
        {
            var universite = await _fakulteStore.Create(fakulteDto);
            return new ApiResponse(Status200OK, "Created FakulteDto", universite);
        }

        public async Task<ApiResponse> Update(FakulteDto fakulteDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated FakulteDto", await _fakulteStore.Update(fakulteDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update FakulteDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _fakulteStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete FakulteDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update FakulteDto");
            }
        }
    }
}
