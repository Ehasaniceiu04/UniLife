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
            return new ApiResponse(Status200OK, "Retrieved HarcDtos", await _harcStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved HarcDto", await _harcStore.GetById(id));
        }

        public async Task<ApiResponse> Create(HarcDto harcDto)
        {
            var universite = await _harcStore.Create(harcDto);
            return new ApiResponse(Status200OK, "Created HarcDto", universite);
        }

        public async Task<ApiResponse> Update(HarcDto harcDto)
        {
            return new ApiResponse(Status200OK, "Updated HarcDto", await _harcStore.Update(harcDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _harcStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete HarcDto");
        }
    }
}
