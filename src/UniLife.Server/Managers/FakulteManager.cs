using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
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
            return new ApiResponse(Status200OK, "Retrieved FakulteDtos", await _fakulteStore.GetAll());

        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved FakulteDto", await _fakulteStore.GetById(id));
        }

        public async Task<ApiResponse> Create(FakulteDto fakulteDto)
        {
            var universite = await _fakulteStore.Create(fakulteDto);
            return new ApiResponse(Status200OK, "Created FakulteDto", universite);
        }

        public async Task<ApiResponse> Update(FakulteDto fakulteDto)
        {
            return new ApiResponse(Status200OK, "Updated FakulteDto", await _fakulteStore.Update(fakulteDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _fakulteStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete FakulteDto");
        }

        public async Task<ApiResponse> GetOgrCountOfFakultesGYear()
        {
            return new ApiResponse(Status200OK, "Retrieved FakulteDtos", await _fakulteStore.GetOgrCountOfFakultesGYear());
        }
    }
}
