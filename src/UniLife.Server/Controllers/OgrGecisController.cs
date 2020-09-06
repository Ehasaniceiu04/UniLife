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
    public class OgrGecisController : ControllerBase
    {
        private readonly IOgrGecisManager _ogrGecisManager;

        public OgrGecisController(IOgrGecisManager ogrGecisManager)
        {
            _ogrGecisManager = ogrGecisManager;
        }

        // GET: api/OgrGecis
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrGecisManager.Get();

        // GET: api/OgrGecis/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrGecisManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrGecis Model is Invalid");

        // POST: api/OgrGecis
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrGecisDto ogrGecisDto)
            => ModelState.IsValid ?
                await _ogrGecisManager.Create(ogrGecisDto) :
                new ApiResponse(Status400BadRequest, "OgrGecis Model is Invalid");

        // Put: api/OgrGecis
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrGecisDto ogrGecisDto)
            => ModelState.IsValid ?
                await _ogrGecisManager.Update(ogrGecisDto) :
                new ApiResponse(Status400BadRequest, "OgrGecis Model is Invalid");

        // DELETE: api/OgrGecis/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrGecisManager.Delete(id);

        [HttpGet]
        [Route("GetWhere/{ogrId}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> GetWhere(int ogrId)
            => await _ogrGecisManager.GetWhere(x => x.OgrenciId == ogrId);
    }
}
