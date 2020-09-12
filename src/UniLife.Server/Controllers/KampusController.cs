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
    public class KampusController : ControllerBase
    {
        private readonly IKampusManager _kampusManager;

        public KampusController(IKampusManager kampusManager)
        {
            _kampusManager = kampusManager;
        }

        // GET: api/Kampus
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _kampusManager.Get();

        // GET: api/Kampus/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _kampusManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Kampus Model is Invalid");

        // POST: api/Kampus
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] KampusDto kampusDto)
            => ModelState.IsValid ?
                await _kampusManager.Create(kampusDto) :
                new ApiResponse(Status400BadRequest, "Kampus Model is Invalid");

        // Put: api/Kampus
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] KampusDto kampusDto)
            => ModelState.IsValid ?
                await _kampusManager.Update(kampusDto) :
                new ApiResponse(Status400BadRequest, "Kampus Model is Invalid");

        // DELETE: api/Kampus/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _kampusManager.Delete(id);
    }
}
