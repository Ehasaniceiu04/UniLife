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
    public class AskerlikController : ControllerBase
    {
        private readonly IAskerlikManager _askerlikManager;

        public AskerlikController(IAskerlikManager askerlikManager)
        {
            _askerlikManager = askerlikManager;
        }

        // GET: api/Askerlik
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _askerlikManager.Get();

        // GET: api/Askerlik/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _askerlikManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Askerlik Model is Invalid");

        // POST: api/Askerlik
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] AskerlikDto askerlikDto)
            => ModelState.IsValid ?
                await _askerlikManager.Create(askerlikDto) :
                new ApiResponse(Status400BadRequest, "Askerlik Model is Invalid");

        // Put: api/Askerlik
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] AskerlikDto askerlikDto)
            => ModelState.IsValid ?
                await _askerlikManager.Update(askerlikDto) :
                new ApiResponse(Status400BadRequest, "Askerlik Model is Invalid");

        // DELETE: api/Askerlik/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _askerlikManager.Delete(id);
    }
}
