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
using Semerkand.CommonUI.Pages.Definitions.Tabs;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MufredatController : ControllerBase
    {
        private readonly IMufredatManager _mufredatManager;

        public MufredatController(IMufredatManager mufredatManager)
        {
            _mufredatManager = mufredatManager;
        }

        // GET: api/Mufredat
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _mufredatManager.Get();

        // GET: api/Mufredat/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _mufredatManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        // POST: api/Mufredat
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] MufredatDto mufredatDto)
            => ModelState.IsValid ?
                await _mufredatManager.Create(mufredatDto) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        // Put: api/Mufredat
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] MufredatDto mufredatDto)
            => ModelState.IsValid ?
                await _mufredatManager.Update(mufredatDto) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        // DELETE: api/Mufredat/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Mufredat.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _mufredatManager.Delete(id);


        // Müfredat çokla: api/Mufredat/Cokla/5
        //[HttpGet("{id}")]
        [HttpGet]
        [AllowAnonymous]
        [Route("Cokla/{id}")]
        public async Task<ApiResponse> Cokla(int id)
            => ModelState.IsValid ?
                await _mufredatManager.Cokla(id) :
                new ApiResponse(Status400BadRequest, "MUfreadt Model is Invalid");


        [HttpGet]
        [AllowAnonymous]
        [Route("GetMufredatByProgramIds/{programIds}")]
        public async Task<ApiResponse> GetMufredatByProgramIds(string programIds)
        => ModelState.IsValid ?
                await _mufredatManager.GetMufredatByProgramIds(programIds.Replace(" ", "").Split(',')) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");
    }
}
