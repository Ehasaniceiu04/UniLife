using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class ProgramManager : IProgramManager
    {
        private readonly IProgramStore _programStore;

        public ProgramManager(IProgramStore programStore)
        {
            _programStore = programStore;
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved ProgramDtos", await _programStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved ProgramDto", await _programStore.GetById(id));
        }

        public async Task<ApiResponse> Create(ProgramDto programDto)
        {
            var universite = await _programStore.Create(programDto);
            return new ApiResponse(Status200OK, "Created ProgramDto", universite);
        }

        public async Task<ApiResponse> Update(ProgramDto programDto)
        {
            return new ApiResponse(Status200OK, "Updated ProgramDto", await _programStore.Update(programDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _programStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete ProgramDto");
        }

        public async Task<ApiResponse> GetProgramByBolumIds(string[] bolumIds)
        {
            return new ApiResponse(Status200OK, "Retrieved ProgramDtos", await _programStore.GetProgramByBolumIds(bolumIds));
        }
    }
}
