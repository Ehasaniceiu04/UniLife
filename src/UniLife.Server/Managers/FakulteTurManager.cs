using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class FakulteTurManager : IFakulteTurManager
    {
        private readonly IFakulteTurStore _fakulteTurStore;

        public FakulteTurManager(IFakulteTurStore fakulteTurStore)
        {
            _fakulteTurStore = fakulteTurStore;
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved FakulteTurDtos", await _fakulteTurStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved FakulteTurDto", await _fakulteTurStore.GetById(id));
        }

        public async Task<ApiResponse> Create(FakulteTurDto fakulteTurDto)
        {
            var fakulteTur = await _fakulteTurStore.Create(fakulteTurDto);
            return new ApiResponse(Status200OK, "Created FakulteTurDto", fakulteTur);
        }

        public async Task<ApiResponse> Update(FakulteTurDto fakulteTurDto)
        {
            return new ApiResponse(Status200OK, "Updated FakulteTurDto", await _fakulteTurStore.Update(fakulteTurDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _fakulteTurStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete FakulteTurDto");
        }
    }
}
