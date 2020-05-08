using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Controllers
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
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _harcManager.Get();

        // GET: api/Harc/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _harcManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Harc Model is Invalid");

        // POST: api/Harc
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] HarcDto harcDto)
            => ModelState.IsValid ?
                await _harcManager.Create(harcDto) :
                new ApiResponse(Status400BadRequest, "Harc Model is Invalid");

        // Put: api/Harc
        [HttpPut]
        [AllowAnonymous]
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
