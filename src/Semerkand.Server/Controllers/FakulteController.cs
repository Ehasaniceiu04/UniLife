using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakulteController : ControllerBase
    {
        private readonly IFakulteManager _fakulteManager;

        public FakulteController(IFakulteManager fakulteManager)
        {
            _fakulteManager = fakulteManager;
        }

        // GET: api/Fakulte
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _fakulteManager.Get();

        // GET: api/Fakulte/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _fakulteManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Fakulte Model is Invalid");

        // POST: api/Fakulte
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] FakulteDto fakulteDto)
            => ModelState.IsValid ?
                await _fakulteManager.Create(fakulteDto) :
                new ApiResponse(Status400BadRequest, "Fakulte Model is Invalid");

        // Put: api/Fakulte
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] FakulteDto fakulteDto)
            => ModelState.IsValid ?
                await _fakulteManager.Update(fakulteDto) :
                new ApiResponse(Status400BadRequest, "Fakulte Model is Invalid");

        // DELETE: api/Fakulte/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Fakulte.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _fakulteManager.Delete(id);
    }
}
