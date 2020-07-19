using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
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
            return new ApiResponse(Status200OK, "Retrieved DonemDtos", await _donemStore.GetAll());

        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved DonemDto", await _donemStore.GetById(id));

        }

        public async Task<ApiResponse> Create(DonemDto donemDto)
        {
            var universite = await _donemStore.Create(donemDto);
            return new ApiResponse(Status200OK, "Created DonemDto", universite);
        }

        public async Task<ApiResponse> Update(DonemDto donemDto)
        {
            return new ApiResponse(Status200OK, "Updated DonemDto", await _donemStore.Update(donemDto));

        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _donemStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete DonemDto");

        }

        public async Task<ApiResponse> Current()
        {
            return new ApiResponse(Status200OK, "Retrieved DonemDtos", await _donemStore.Current());

        }
    }
}
