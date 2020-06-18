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
    public class KeyValuesController : ControllerBase
    {
        private readonly IKayitNedenManager _kayitNedenManager;
        private readonly ISinavTurManager _sinavTurManager;
        private readonly ISinavTipManager _sinavTipManager;
        //private readonly ISinifManager _sinifManager;

        public KeyValuesController(IKayitNedenManager kayitNedenManager, ISinavTurManager sinavTurManager, ISinavTipManager sinavTipManager)
        {
            _kayitNedenManager = kayitNedenManager;
            _sinavTurManager = sinavTurManager;
            _sinavTipManager = sinavTipManager;
        }

        // GET: api/OgrenimTur
        [HttpGet]
        [AllowAnonymous]
        [Route("GetKayitNeden")]
        public async Task<ApiResponse> GetKayitNeden()
            => await _kayitNedenManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Route("GetKayitNeden")]
        public async Task<ApiResponse> GetKayitNeden(int id)
            => ModelState.IsValid ?
                await _kayitNedenManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [AllowAnonymous]
        [Route("PostKayitNeden")]
        public async Task<ApiResponse> PostKayitNeden([FromBody] KayitNedenDto kayitNedenDto)
            => ModelState.IsValid ?
                await _kayitNedenManager.Create(kayitNedenDto) :
                new ApiResponse(Status400BadRequest, "KayitNeden Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [AllowAnonymous]
        [Route("PutKayitNeden")]
        public async Task<ApiResponse> PutKayitNeden([FromBody] KayitNedenDto kayitNedenDto)
            => ModelState.IsValid ?
                await _kayitNedenManager.Update(kayitNedenDto) :
                new ApiResponse(Status400BadRequest, "KayitNeden Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Permissions.Universite.Delete)]
        [Route("DeleteKayitNeden/{id}")]
        public async Task<ApiResponse> DeleteKayitNeden(int id)
            => await _kayitNedenManager.Delete(id);


        /////SinavTur

        // GET: api/OgrenimTur
        [HttpGet]
        [AllowAnonymous]
        [Route("GetSinavTur")]
        public async Task<ApiResponse> GetSinavTur()
            => await _sinavTurManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Route("GetSinavTur")]
        public async Task<ApiResponse> GetSinavTur(int id)
            => ModelState.IsValid ?
                await _sinavTurManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [AllowAnonymous]
        [Route("PostSinavTur")]
        public async Task<ApiResponse> PostSinavTur([FromBody] SinavTurDto sinavTurDto)
            => ModelState.IsValid ?
                await _sinavTurManager.Create(sinavTurDto) :
                new ApiResponse(Status400BadRequest, "SinavTur Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [AllowAnonymous]
        [Route("PutSinavTur")]
        public async Task<ApiResponse> PutSinavTur([FromBody] SinavTurDto sinavTurDto)
            => ModelState.IsValid ?
                await _sinavTurManager.Update(sinavTurDto) :
                new ApiResponse(Status400BadRequest, "SinavTur Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Permissions.Universite.Delete)]
        [Route("DeleteSinavTur/{id}")]
        public async Task<ApiResponse> DeleteSinavTur(int id)
            => await _sinavTurManager.Delete(id);


        //SinavTip
        //
        // GET: api/OgrenimTur
        [HttpGet]
        [AllowAnonymous]
        [Route("GetSinavTip")]
        public async Task<ApiResponse> GetSinavTip()
            => await _sinavTipManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Route("GetSinavTip")]
        public async Task<ApiResponse> GetSinavTip(int id)
            => ModelState.IsValid ?
                await _sinavTipManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [AllowAnonymous]
        [Route("PostSinavTip")]
        public async Task<ApiResponse> PostSinavTip([FromBody] SinavTipDto sinavTipDto)
            => ModelState.IsValid ?
                await _sinavTipManager.Create(sinavTipDto) :
                new ApiResponse(Status400BadRequest, "SinavTip Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [AllowAnonymous]
        [Route("PutSinavTip")]
        public async Task<ApiResponse> PutSinavTip([FromBody] SinavTipDto sinavTipDto)
            => ModelState.IsValid ?
                await _sinavTipManager.Update(sinavTipDto) :
                new ApiResponse(Status400BadRequest, "SinavTip Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Permissions.Universite.Delete)]
        [Route("DeleteSinavTip/{id}")]
        public async Task<ApiResponse> DeleteSinavTip(int id)
            => await _sinavTipManager.Delete(id);


    }
}
