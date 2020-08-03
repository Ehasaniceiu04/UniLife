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
    public class OgrStajController : ControllerBase
    {
        private readonly IOgrStajManager _ogrStajManager;

        public OgrStajController(IOgrStajManager ogrStajManager)
        {
            _ogrStajManager = ogrStajManager;
        }

        // GET: api/OgrStaj
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrStajManager.Get();

        // GET: api/OgrStaj/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrStajManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrStaj Model is Invalid");

        // POST: api/OgrStaj
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrStajDto ogrStajDto)
            => ModelState.IsValid ?
                await _ogrStajManager.Create(ogrStajDto) :
                new ApiResponse(Status400BadRequest, "OgrStaj Model is Invalid");

        // Put: api/OgrStaj
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrStajDto ogrStajDto)
            => ModelState.IsValid ?
                await _ogrStajManager.Update(ogrStajDto) :
                new ApiResponse(Status400BadRequest, "OgrStaj Model is Invalid");

        // DELETE: api/OgrStaj/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrStajManager.Delete(id);
    }
}
