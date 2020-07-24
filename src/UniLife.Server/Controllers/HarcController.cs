using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarcController : ControllerBase
    {
        private readonly IHarcManager _harcManager;

        public HarcController(IHarcManager harcManager)
        {
            _harcManager = harcManager;
        }

        // GET: api/Harc
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _harcManager.Get();

        // GET: api/Harc/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _harcManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Harc Model is Invalid");

        // POST: api/Harc
        [HttpPost]
        [Authorize(Permissions.Harc.Create)]
        public async Task<ApiResponse> Post([FromBody] HarcDto harcDto)
            => ModelState.IsValid ?
                await _harcManager.Create(harcDto) :
                new ApiResponse(Status400BadRequest, "Harc Model is Invalid");

        // Put: api/Harc
        [HttpPut]
        [Authorize(Permissions.Harc.Update)]
        public async Task<ApiResponse> Put([FromBody] HarcDto harcDto)
            => ModelState.IsValid ?
                await _harcManager.Update(harcDto) :
                new ApiResponse(Status400BadRequest, "Harc Model is Invalid");

        // DELETE: api/Harc/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Harc.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _harcManager.Delete(id);
    }
}
