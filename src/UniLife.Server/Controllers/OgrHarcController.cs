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
    public class OgrHarcController : ControllerBase
    {
        private readonly IOgrHarcManager _ogrHarcManager;

        public OgrHarcController(IOgrHarcManager ogrHarcManager)
        {
            _ogrHarcManager = ogrHarcManager;
        }

        // GET: api/OgrHarc
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrHarcManager.Get();

        // GET: api/OgrHarc/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrHarcManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrHarc Model is Invalid");

        // POST: api/OgrHarc
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrHarcDto ogrHarcDto)
            => ModelState.IsValid ?
                await _ogrHarcManager.Create(ogrHarcDto) :
                new ApiResponse(Status400BadRequest, "OgrHarc Model is Invalid");

        // Put: api/OgrHarc
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrHarcDto ogrHarcDto)
            => ModelState.IsValid ?
                await _ogrHarcManager.Update(ogrHarcDto) :
                new ApiResponse(Status400BadRequest, "OgrHarc Model is Invalid");

        // DELETE: api/OgrHarc/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrHarcManager.Delete(id);

        [HttpGet]
        [Route("GetWhere/{ogrId}")]
        [Authorize]
        public async Task<ApiResponse> GetWhere(int ogrId)
            => await _ogrHarcManager.GetWhere(x => x.OgrenciId == ogrId);
    }
}
