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
    public class MufredatController : ControllerBase
    {
        private readonly IMufredatManager _mufredatManager;

        public MufredatController(IMufredatManager mufredatManager)
        {
            _mufredatManager = mufredatManager;
        }

        // GET: api/Mufredat
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _mufredatManager.Get();

        // GET: api/Mufredat/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _mufredatManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        // POST: api/Mufredat
        [HttpPost]
        [Authorize(Permissions.Mufredat.Create)]
        public async Task<ApiResponse> Post([FromBody] MufredatDto mufredatDto)
            => ModelState.IsValid ?
                await _mufredatManager.Create(mufredatDto) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        // Put: api/Mufredat
        [HttpPut]
        [Authorize(Permissions.Mufredat.Update)]
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
        [Authorize(Permissions.Mufredat.Create)]
        [Route("Cokla/{id}")]
        public async Task<ApiResponse> Cokla(int id)
            => ModelState.IsValid ?
                await _mufredatManager.Cokla(id) :
                new ApiResponse(Status400BadRequest, "MUfreadt Model is Invalid");


        [HttpGet]
        [Authorize]
        [Route("GetMufredatByProgramIds/{programIds}")]
        public async Task<ApiResponse> GetMufredatByProgramIds(string programIds)
        => ModelState.IsValid ?
                await _mufredatManager.GetMufredatByProgramIds(programIds.Replace(" ", "").Split(',')) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        [HttpGet]
        [Authorize]
        [Route("GetMufredatState/{mufredatId}")]
        public async Task<ApiResponse> GetMufredatState(int mufredatId)
        => ModelState.IsValid ?
                await _mufredatManager.GetMufredatState(mufredatId) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");

        [HttpPost]
        [Authorize(Permissions.Mufredat.Create)]
        [Route("CreateDersAcilansByMufredatIds")]
        public async Task<ApiResponse> CreateDersAcilansByMufredatIds([FromBody] ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
            => ModelState.IsValid ?
                await _mufredatManager.CreateDersAcilansByMufredatIds(reqEntityIdWithOtherEntitiesIds) :
                new ApiResponse(Status400BadRequest, "Mufredat Model is Invalid");
        
    }
}
