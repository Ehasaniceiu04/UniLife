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
    public class BolumController : ControllerBase
    {
        private readonly IBolumManager _bolumManager;

        public BolumController(IBolumManager bolumManager)
        {
            _bolumManager = bolumManager;
        }

        // GET: api/Bolum
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _bolumManager.Get();

        // GET: api/Bolum/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _bolumManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        // POST: api/Bolum
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] BolumDto bolumDto)
            => ModelState.IsValid ?
                await _bolumManager.Create(bolumDto) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        // Put: api/Bolum
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] BolumDto bolumDto)
            => ModelState.IsValid ?
                await _bolumManager.Update(bolumDto) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        // DELETE: api/Bolum/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Bolum.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _bolumManager.Delete(id);




        [HttpGet]
        [AllowAnonymous]
        [Route("GetBolumByFakulteIds/{fakulteIds}")]
        public async Task<ApiResponse> GetBolumByFakulteIds(string fakulteIds)
        => ModelState.IsValid ?
                await _bolumManager.GetDersByMufredatId(fakulteIds.Replace(" ", "").Split(',')) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");
    }
}
