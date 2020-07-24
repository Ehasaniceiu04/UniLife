using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Controllers
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
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _fakulteManager.Get();

        // GET: api/Fakulte/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _fakulteManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Fakulte Model is Invalid");

        // POST: api/Fakulte
        [HttpPost]
        [Authorize(Permissions.Fakulte.Create)]
        public async Task<ApiResponse> Post([FromBody] FakulteDto fakulteDto)
            => ModelState.IsValid ?
                await _fakulteManager.Create(fakulteDto) :
                new ApiResponse(Status400BadRequest, "Fakulte Model is Invalid");

        // Put: api/Fakulte
        [HttpPut]
        [Authorize(Permissions.Fakulte.Update)]
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
