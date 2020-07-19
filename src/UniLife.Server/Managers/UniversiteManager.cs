using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
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
            return new ApiResponse(Status200OK, "Retrieved UniversiteDtos", await _universiteStore.GetAll());

        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved UniversiteDto", await _universiteStore.GetById(id));
        }

        public async Task<ApiResponse> Create(UniversiteDto universiteDto)
        {
            var universite = await _universiteStore.Create(universiteDto);
            return new ApiResponse(Status200OK, "Created UniversiteDto", universite);
        }

        public async Task<ApiResponse> Update(UniversiteDto universiteDto)
        {
            return new ApiResponse(Status200OK, "Updated UniversiteDto", await _universiteStore.Update(universiteDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _universiteStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete UniversiteDto");
        }
    }
}
