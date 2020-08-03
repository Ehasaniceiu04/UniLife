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
    public class OgrTezController : ControllerBase
    {
        private readonly IOgrTezManager _ogrTezManager;

        public OgrTezController(IOgrTezManager ogrTezManager)
        {
            _ogrTezManager = ogrTezManager;
        }

        // GET: api/OgrTez
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrTezManager.Get();

        // GET: api/OgrTez/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrTezManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrTez Model is Invalid");

        // POST: api/OgrTez
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrTezDto ogrTezDto)
            => ModelState.IsValid ?
                await _ogrTezManager.Create(ogrTezDto) :
                new ApiResponse(Status400BadRequest, "OgrTez Model is Invalid");

        // Put: api/OgrTez
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrTezDto ogrTezDto)
            => ModelState.IsValid ?
                await _ogrTezManager.Update(ogrTezDto) :
                new ApiResponse(Status400BadRequest, "OgrTez Model is Invalid");

        // DELETE: api/OgrTez/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrTezManager.Delete(id);
    }
}
