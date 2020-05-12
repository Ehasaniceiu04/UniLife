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
    public class DonemController : ControllerBase
    {
        private readonly IDonemManager _donemManager;

        public DonemController(IDonemManager donemManager)
        {
            _donemManager = donemManager;
        }

        // GET: api/Donem
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _donemManager.Get();

        // GET: api/Donem/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _donemManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");

        // POST: api/Donem
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] DonemDto donemDto)
            => ModelState.IsValid ?
                await _donemManager.Create(donemDto) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");

        // Put: api/Donem
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] DonemDto donemDto)
            => ModelState.IsValid ?
                await _donemManager.Update(donemDto) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");

        // DELETE: api/Donem/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Donem.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _donemManager.Delete(id);
    }
}
