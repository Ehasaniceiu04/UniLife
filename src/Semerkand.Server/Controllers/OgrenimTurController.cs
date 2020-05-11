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
    public class OgrenimTurController : ControllerBase
    {
        private readonly IOgrenimTurManager _ogrenimTurManager;

        public OgrenimTurController(IOgrenimTurManager ogrenimTurManager)
        {
            _ogrenimTurManager = ogrenimTurManager;
        }

        // GET: api/OgrenimTur
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _ogrenimTurManager.Get();


        // GET: api/OgrenimTur/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrenimTurManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // POST: api/OgrenimTur
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] OgrenimTurDto ogrenimTurDto)
            => ModelState.IsValid ?
                await _ogrenimTurManager.Create(ogrenimTurDto) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // Put: api/OgrenimTur
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] OgrenimTurDto ogrenimTurDto)
            => ModelState.IsValid ?
                await _ogrenimTurManager.Update(ogrenimTurDto) :
                new ApiResponse(Status400BadRequest, "OgrenimTur Model is Invalid");

        // DELETE: api/OgrenimTur/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Universite.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrenimTurManager.Delete(id);
    }
}
