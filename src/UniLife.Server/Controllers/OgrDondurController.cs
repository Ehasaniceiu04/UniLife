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
    public class OgrDondurController : ControllerBase
    {
        private readonly IOgrDondurManager _ogrDondurManager;

        public OgrDondurController(IOgrDondurManager ogrDondurManager)
        {
            _ogrDondurManager = ogrDondurManager;
        }

        // GET: api/OgrDondur
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrDondurManager.Get();

        // GET: api/OgrDondur/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrDondurManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrDondur Model is Invalid");

        // POST: api/OgrDondur
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrDondurDto ogrDondurDto)
            => ModelState.IsValid ?
                await _ogrDondurManager.Create(ogrDondurDto) :
                new ApiResponse(Status400BadRequest, "OgrDondur Model is Invalid");

        // Put: api/OgrDondur
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrDondurDto ogrDondurDto)
            => ModelState.IsValid ?
                await _ogrDondurManager.Update(ogrDondurDto) :
                new ApiResponse(Status400BadRequest, "OgrDondur Model is Invalid");

        // DELETE: api/OgrDondur/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrDondurManager.Delete(id);

        [HttpGet]
        [Route("GetWhere/{ogrId}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> GetWhere(int ogrId)
            => await _ogrDondurManager.GetWhere(x => x.OgrenciId == ogrId);
    }
}
