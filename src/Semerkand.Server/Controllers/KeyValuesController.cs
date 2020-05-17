using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValuesController : ControllerBase
    {
        private readonly IKayitNedenManager _kayitNedenManager;
        //private readonly ISinifManager _sinifManager;

        public KeyValuesController(IKayitNedenManager kayitNedenManager)
        {
            _kayitNedenManager = kayitNedenManager;
        }

        // GET: api/OgrenimTur
        [HttpGet]
        [AllowAnonymous]
        [Route("GetKayitNeden")]
        public async Task<ApiResponse> GetKayitNeden()
            => await _kayitNedenManager.Get();

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("GetOgrenimDurum")]
        //public async Task<ApiResponse> GetOgrenimDurum()
        //    => await _ogrenimdurumManager.Get();


        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Route("GetKayitNeden")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _kayitNedenManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [AllowAnonymous]
        [Route("PostKayitNeden")]
        public async Task<ApiResponse> Post([FromBody] KayitNedenDto kayitNedenDto)
            => ModelState.IsValid ?
                await _kayitNedenManager.Create(kayitNedenDto) :
                new ApiResponse(Status400BadRequest, "KayitNeden Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [AllowAnonymous]
        [Route("PutKayitNeden")]
        public async Task<ApiResponse> Put([FromBody] KayitNedenDto kayitNedenDto)
            => ModelState.IsValid ?
                await _kayitNedenManager.Update(kayitNedenDto) :
                new ApiResponse(Status400BadRequest, "KayitNeden Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Universite.Delete)]
        [Route("DeleteKayitNeden")]
        public async Task<ApiResponse> Delete(int id)
            => await _kayitNedenManager.Delete(id);
    }
}
