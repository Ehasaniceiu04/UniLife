using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinavKayitController : ControllerBase
    {
        private readonly ISinavKayitManager _sinavKayitManager;

        public SinavKayitController(ISinavKayitManager sinavKayitManager)
        {
            _sinavKayitManager = sinavKayitManager;
        }

        // GET: api/SinavKayit
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _sinavKayitManager.Get();


        // GET: api/SinavKayit/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _sinavKayitManager.Get(id) :
                new ApiResponse(Status400BadRequest, "SinavKayit Model is Invalid");

        // POST: api/SinavKayit
        [HttpPost]
        [Authorize(Permissions.SinavKayit.Create)]
        public async Task<ApiResponse> Post([FromBody] SinavKayitDto sinavKayitDto)
            => ModelState.IsValid ?
                await _sinavKayitManager.Create(sinavKayitDto) :
                new ApiResponse(Status400BadRequest, "SinavKayit Model is Invalid");

        // Put: api/SinavKayit
        [HttpPut]
        [Authorize(Permissions.SinavKayit.Update)]
        public async Task<ApiResponse> Put([FromBody] SinavKayitDto sinavKayitDto)
            => ModelState.IsValid ?
                await _sinavKayitManager.Update(sinavKayitDto) :
                new ApiResponse(Status400BadRequest, "SinavKayit Model is Invalid");

        // DELETE: api/SinavKayit/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.SinavKayit.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _sinavKayitManager.Delete(id);



        [HttpGet]
        [Authorize(Permissions.SinavKayit.Read)]
        [Route("GetOgrenciNotlar/{ogrenciId}")]
        public async Task<ApiResponse> GetOgrenciNotlar(int ogrenciId)
        => ModelState.IsValid ?
            await _sinavKayitManager.GetOgrenciNotlar(ogrenciId) :
            new ApiResponse(Status400BadRequest, "SinavKayit Model is Invalid");


        [HttpGet]
        [Authorize(Permissions.SinavKayit.Read)]
        [Route("GetSinavKayitOgrenciNotlar/{sinavId}")]
        public async Task<ApiResponse> GetSinavKayitOgrenciNotlar(int sinavId)
        => ModelState.IsValid ?
            await _sinavKayitManager.GetSinavKayitOgrenciNotlar(sinavId) :
            new ApiResponse(Status400BadRequest, "SinavKayit Model is Invalid");


        
    }
}
