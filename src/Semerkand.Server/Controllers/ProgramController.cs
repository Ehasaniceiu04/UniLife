using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.DataModels;
using Semerkand.CommonUI.Pages.Definations.Tabs;

namespace Semerkand.Server.Controllers
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
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _programManager.Get();

        // GET: api/Program/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _programManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");

        // POST: api/Program
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] ProgramDto programDto)
            => ModelState.IsValid ?
                await _programManager.Create(programDto) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");

        // Put: api/Program
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] ProgramDto programDto)
            => ModelState.IsValid ?
                await _programManager.Update(programDto) :
                new ApiResponse(Status400BadRequest, "Program Model is Invalid");

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Program.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _programManager.Delete(id);
    }
}
