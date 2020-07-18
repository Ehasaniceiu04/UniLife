using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class BaseManager<T, TDto> : IBaseManager<T, TDto> where TDto : EntityDto<int>, new()
                                                             where T : Entity<int>, new()
    {
        private readonly IBaseStore<T, TDto> _baseStore;

        public BaseManager(IBaseStore<T, TDto> baseStore)
        {
            _baseStore = baseStore;
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved Dtos", await _baseStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved Dto", await _baseStore.GetById(id));
        }

        public async Task<ApiResponse> Create(TDto tDto)
        {
            var t = await _baseStore.Create(tDto);
            return new ApiResponse(Status200OK, "Created Dto", t);
        }

        public async Task<ApiResponse> Update(TDto tDto)
        {
            return new ApiResponse(Status200OK, "Updated Dto", await _baseStore.Update(tDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _baseStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete Dto");
        }

    }
}
