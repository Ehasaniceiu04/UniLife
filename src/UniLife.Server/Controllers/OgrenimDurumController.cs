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
    public class OgrenimDurumController : ControllerBase
    {
        private readonly IOgrenimDurumManager _ogrenimDurumManager;

        public OgrenimDurumController(IOgrenimDurumManager ogrenimDurumManager)
        {
            _ogrenimDurumManager = ogrenimDurumManager;
        }

        // GET: api/OgrenimDurum
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _ogrenimDurumManager.Get();


        // GET: api/OgrenimDurum/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrenimDurumManager.Get(id) :
                new ApiResponse(Status400BadRequest, "OgrenimDurum Model is Invalid");

        // POST: api/OgrenimDurum
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] OgrenimDurumDto ogrenimDurumDto)
            => ModelState.IsValid ?
                await _ogrenimDurumManager.Create(ogrenimDurumDto) :
                new ApiResponse(Status400BadRequest, "OgrenimDurum Model is Invalid");

        // Put: api/OgrenimDurum
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] OgrenimDurumDto ogrenimDurumDto)
            => ModelState.IsValid ?
                await _ogrenimDurumManager.Update(ogrenimDurumDto) :
                new ApiResponse(Status400BadRequest, "OgrenimDurum Model is Invalid");

        // DELETE: api/OgrenimDurum/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Universite.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrenimDurumManager.Delete(id);
    }
}
