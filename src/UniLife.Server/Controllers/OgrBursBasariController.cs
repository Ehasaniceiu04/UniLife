using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrBursBasariController : ControllerBase
    {
        private readonly IOgrBursBasariManager _ogrBursBasariManager;

        public OgrBursBasariController(IOgrBursBasariManager ogrBursBasariManager)
        {
            _ogrBursBasariManager = ogrBursBasariManager;
        }

        // GET: api/OgrBursBasari
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrBursBasariManager.Get();

        // GET: api/OgrBursBasari/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrBursBasariManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrBursBasari Model is Invalid");

        // POST: api/OgrBursBasari
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrBursBasariDto ogrBursBasariDto)
            => ModelState.IsValid ?
                await _ogrBursBasariManager.Create(ogrBursBasariDto) :
                new ApiResponse(Status400BadRequest, "OgrBursBasari Model is Invalid");

        // Put: api/OgrBursBasari
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrBursBasariDto ogrBursBasariDto)
            => ModelState.IsValid ?
                await _ogrBursBasariManager.Update(ogrBursBasariDto) :
                new ApiResponse(Status400BadRequest, "OgrBursBasari Model is Invalid");

        // DELETE: api/OgrBursBasari/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrBursBasariManager.Delete(id);

        [HttpGet]
        [Route("GetWhere/{ogrId}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> GetWhere(int ogrId)
            => await _ogrBursBasariManager.GetWhere(x => x.OgrenciId == ogrId);
    }
}
