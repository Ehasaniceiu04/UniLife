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
        private readonly IBinaManager _binaManager;
        //private readonly ISinifManager _sinifManager;

        public KeyValuesController(IKayitNedenManager kayitNedenManager, ISinavTurManager sinavTurManager, ISinavTipManager sinavTipManager, IBinaManager binaManager)
        {
            _kayitNedenManager = kayitNedenManager;
            _sinavTurManager = sinavTurManager;
            _sinavTipManager = sinavTipManager;
            _binaManager = binaManager;
        }

        // GET: api/OgrenimTur
        [HttpGet]
        [Authorize]
        [Route("GetKayitNeden")]
        public async Task<ApiResponse> GetKayitNeden()
            => await _kayitNedenManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [Authorize]
        [Route("GetKayitNeden")]
        public async Task<ApiResponse> GetKayitNeden(int id)
            => ModelState.IsValid ?
                await _kayitNedenManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostKayitNeden")]
        public async Task<ApiResponse> PostKayitNeden([FromBody] KayitNedenDto kayitNedenDto)
            => ModelState.IsValid ?
                await _kayitNedenManager.Create(kayitNedenDto) :
                new ApiResponse(Status400BadRequest, "KayitNeden Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PutKayitNeden")]
        public async Task<ApiResponse> PutKayitNeden([FromBody] KayitNedenDto kayitNedenDto)
            => ModelState.IsValid ?
                await _kayitNedenManager.Update(kayitNedenDto) :
                new ApiResponse(Status400BadRequest, "KayitNeden Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("DeleteKayitNeden/{id}")]
        public async Task<ApiResponse> DeleteKayitNeden(int id)
            => await _kayitNedenManager.Delete(id);


        /////SinavTur

        // GET: api/OgrenimTur
        [HttpGet]
        [Authorize]
        [Route("GetSinavTur")]
        public async Task<ApiResponse> GetSinavTur()
            => await _sinavTurManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [Authorize]
        [Route("GetSinavTur")]
        public async Task<ApiResponse> GetSinavTur(int id)
            => ModelState.IsValid ?
                await _sinavTurManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSinavTur")]
        public async Task<ApiResponse> PostSinavTur([FromBody] SinavTurDto sinavTurDto)
            => ModelState.IsValid ?
                await _sinavTurManager.Create(sinavTurDto) :
                new ApiResponse(Status400BadRequest, "SinavTur Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PutSinavTur")]
        public async Task<ApiResponse> PutSinavTur([FromBody] SinavTurDto sinavTurDto)
            => ModelState.IsValid ?
                await _sinavTurManager.Update(sinavTurDto) :
                new ApiResponse(Status400BadRequest, "SinavTur Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("DeleteSinavTur/{id}")]
        public async Task<ApiResponse> DeleteSinavTur(int id)
            => await _sinavTurManager.Delete(id);


        //SinavTip
        //
        // GET: api/OgrenimTur
        [HttpGet]
        [Authorize]
        [Route("GetSinavTip")]
        public async Task<ApiResponse> GetSinavTip()
            => await _sinavTipManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [Authorize]
        [Route("GetSinavTip")]
        public async Task<ApiResponse> GetSinavTip(int id)
            => ModelState.IsValid ?
                await _sinavTipManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostSinavTip")]
        public async Task<ApiResponse> PostSinavTip([FromBody] SinavTipDto sinavTipDto)
            => ModelState.IsValid ?
                await _sinavTipManager.Create(sinavTipDto) :
                new ApiResponse(Status400BadRequest, "SinavTip Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PutSinavTip")]
        public async Task<ApiResponse> PutSinavTip([FromBody] SinavTipDto sinavTipDto)
            => ModelState.IsValid ?
                await _sinavTipManager.Update(sinavTipDto) :
                new ApiResponse(Status400BadRequest, "SinavTip Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("DeleteSinavTip/{id}")]
        public async Task<ApiResponse> DeleteSinavTip(int id)
            => await _sinavTipManager.Delete(id);



        //Bina

        [HttpGet]
        [Authorize]
        [Route("GetBina")]
        public async Task<ApiResponse> GetBina()
            => await _binaManager.Get();

        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [Authorize]
        [Route("GetBina")]
        public async Task<ApiResponse> GetBina(int id)
            => ModelState.IsValid ?
                await _binaManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PostBina")]
        public async Task<ApiResponse> PostBina([FromBody] BinaDto binaDto)
            => ModelState.IsValid ?
                await _binaManager.Create(binaDto) :
                new ApiResponse(Status400BadRequest, "Bina Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("PutBina")]
        public async Task<ApiResponse> PutBina([FromBody] BinaDto binaDto)
            => ModelState.IsValid ?
                await _binaManager.Update(binaDto) :
                new ApiResponse(Status400BadRequest, "Bina Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("DeleteBina/{id}")]
        public async Task<ApiResponse> DeleteBina(int id)
            => await _binaManager.Delete(id);

    }
}
