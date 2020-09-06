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
    public class OgrCezaController : ControllerBase
    {
        private readonly IOgrCezaManager _ogrCezaManager;

        public OgrCezaController(IOgrCezaManager ogrCezaManager)
        {
            _ogrCezaManager = ogrCezaManager;
        }

        // GET: api/OgrCeza
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrCezaManager.Get();

        // GET: api/OgrCeza/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrCezaManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrCeza Model is Invalid");

        // POST: api/OgrCeza
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrCezaDto ogrCezaDto)
            => ModelState.IsValid ?
                await _ogrCezaManager.Create(ogrCezaDto) :
                new ApiResponse(Status400BadRequest, "OgrCeza Model is Invalid");

        // Put: api/OgrCeza
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrCezaDto ogrCezaDto)
            => ModelState.IsValid ?
                await _ogrCezaManager.Update(ogrCezaDto) :
                new ApiResponse(Status400BadRequest, "OgrCeza Model is Invalid");

        // DELETE: api/OgrCeza/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrCezaManager.Delete(id);

        [HttpGet]
        [Route("GetWhere/{ogrId}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> GetWhere(int ogrId)
            => await _ogrCezaManager.GetWhere(x => x.OgrenciId == ogrId);
    }
}
