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
    public class SinavKriterController : ControllerBase
    {
        private readonly ISinavKriterManager _sinavKriterManager;

        public SinavKriterController(ISinavKriterManager sinavKriterManager)
        {
            _sinavKriterManager = sinavKriterManager;
        }

        // GET: api/SinavKriter
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _sinavKriterManager.Get();

        // GET: api/SinavKriter/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _sinavKriterManager.Get(id) :
                new ApiResponse(Status400BadRequest, "SinavKriter Model is Invalid");

        // POST: api/SinavKriter
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] SinavKriterDto sinavKriterDto)
            => ModelState.IsValid ?
                await _sinavKriterManager.Create(sinavKriterDto) :
                new ApiResponse(Status400BadRequest, "SinavKriter Model is Invalid");

        // Put: api/SinavKriter
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] SinavKriterDto sinavKriterDto)
            => ModelState.IsValid ?
                await _sinavKriterManager.Update(sinavKriterDto) :
                new ApiResponse(Status400BadRequest, "SinavKriter Model is Invalid");

        // DELETE: api/SinavKriter/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _sinavKriterManager.Delete(id);
    }
}
