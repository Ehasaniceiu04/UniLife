using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.DataModels;
using UniLife.CommonUI.Pages.Definitions.Tabs;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramManager _programManager;

        public ProgramController(IProgramManager programManager)
        {
            _programManager = programManager;
        }

        // GET: api/Program
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _programManager.Get();

        // GET: api/Program/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _programManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");

        // POST: api/Program
        [HttpPost]
        [Authorize(Permissions.Program.Create)]
        public async Task<ApiResponse> Post([FromBody] ProgramDto programDto)
            => ModelState.IsValid ?
                await _programManager.Create(programDto) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");

        // Put: api/Program
        [HttpPut]
        [Authorize(Permissions.Program.Update)]
        public async Task<ApiResponse> Put([FromBody] ProgramDto programDto)
            => ModelState.IsValid ?
                await _programManager.Update(programDto) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Program.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _programManager.Delete(id);

        [HttpGet]
        [Authorize]
        [Route("GetProgramByBolumIds/{bolumIds}")]
        public async Task<ApiResponse> GetProgramByBolumIds(string bolumIds)
        => ModelState.IsValid ?
                await _programManager.GetProgramByBolumIds(bolumIds.Replace(" ", "").Split(',')) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");
    }
}
