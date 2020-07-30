using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using System;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsymController : ControllerBase
    {
        private readonly IOsymManager _osymManager;

        public OsymController(IOsymManager osymManager)
        {
            _osymManager = osymManager;
        }

        // GET: api/Osym
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _osymManager.Get();

        // GET: api/Osym/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _osymManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Osym Model is Invalid");

        // GET: api/Osym/5
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("GetByAppId/{appId}")]
        public async Task<ApiResponse> GetByAppId(Guid appId)
            => ModelState.IsValid ?
                await _osymManager.GetByAppId(appId) :
                new ApiResponse(Status400BadRequest, "Osym Model is Invalid");

        // POST: api/Osym
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OsymDto osymDto)
            => ModelState.IsValid ?
                await _osymManager.Create(osymDto) :
                new ApiResponse(Status400BadRequest, "Osym Model is Invalid");

        // Put: api/Osym
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OsymDto osymDto)
            => ModelState.IsValid ?
                await _osymManager.Update(osymDto) :
                new ApiResponse(Status400BadRequest, "Osym Model is Invalid");

        // DELETE: api/Osym/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _osymManager.Delete(id);
    }
}
