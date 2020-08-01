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
    public class OgrenciDigerController : ControllerBase
    {
        private readonly IOgrenciDigerManager _ogrenciDigerManager;

        public OgrenciDigerController(IOgrenciDigerManager ogrenciDigerManager)
        {
            _ogrenciDigerManager = ogrenciDigerManager;
        }

        // GET: api/OgrenciDiger
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _ogrenciDigerManager.Get();

        // GET: api/OgrenciDiger/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");

        // POST: api/OgrenciDiger
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.Create(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");

        // Put: api/OgrenciDiger
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.Update(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");

        // DELETE: api/OgrenciDiger/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrenciDigerManager.Delete(id);

        // POST: api/OgrenciDiger
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSaveGecis")]
        public async Task<ApiResponse> PostSaveGecis([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.PostSaveGecis(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");

        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSaveKayitDond")]
        public async Task<ApiResponse> PostSaveKayitDond([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.PostSaveKayitDond(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSaveKayitCeza")]
        public async Task<ApiResponse> PostSaveKayitCeza([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.PostSaveKayitCeza(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSaveKayitStaj")]
        public async Task<ApiResponse> PostSaveKayitStaj([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.PostSaveKayitStaj(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSaveKayitTez")]
        public async Task<ApiResponse> PostSaveKayitTez([FromBody] OgrenciDigerDto ogrenciDigerDto)
            => ModelState.IsValid ?
                await _ogrenciDigerManager.PostSaveKayitTez(ogrenciDigerDto) :
                new ApiResponse(Status400BadRequest, "OgrenciDiger Model is Invalid");

        // GET: api/OgrenciDiger
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("GetOgrDigerByOgrId/{ogrId}")]
        public async Task<ApiResponse> GetOgrDigerByOgrId(int ogrId)
        {
            return await _ogrenciDigerManager.First(x => x.OgrenciId == ogrId);
        }
    }
}
