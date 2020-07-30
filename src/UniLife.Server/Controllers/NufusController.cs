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
    public class NufusController : ControllerBase
    {
        private readonly INufusManager _nufusManager;

        public NufusController(INufusManager nufusManager)
        {
            _nufusManager = nufusManager;
        }

        // GET: api/Nufus
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _nufusManager.Get();

        // GET: api/Nufus/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _nufusManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Nufus Model is Invalid");

        // POST: api/Nufus
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] NufusDto nufusDto)
            => ModelState.IsValid ?
                await _nufusManager.Create(nufusDto) :
                new ApiResponse(Status400BadRequest, "Nufus Model is Invalid");

        // Put: api/Nufus
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] NufusDto nufusDto)
            => ModelState.IsValid ?
                await _nufusManager.Update(nufusDto) :
                new ApiResponse(Status400BadRequest, "Nufus Model is Invalid");

        // DELETE: api/Nufus/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _nufusManager.Delete(id);
    }
}
