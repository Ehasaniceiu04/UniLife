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
    public class OgretmenController : ControllerBase
    {
        private readonly IOgretmenManager _ogretmenManager;

        public OgretmenController(IOgretmenManager ogretmenManager)
        {
            _ogretmenManager = ogretmenManager;
        }

        // GET: api/Ogretmen
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _ogretmenManager.Get();


        // GET: api/Ogretmen/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogretmenManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Ogretmen Model is Invalid");

        // POST: api/Ogretmen
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] OgretmenDto ogretmenDto)
            => ModelState.IsValid ?
                await _ogretmenManager.Create(ogretmenDto) :
                new ApiResponse(Status400BadRequest, "Ogretmen Model is Invalid");

        // Put: api/Ogretmen
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] OgretmenDto ogretmenDto)
            => ModelState.IsValid ?
                await _ogretmenManager.Update(ogretmenDto) :
                new ApiResponse(Status400BadRequest, "Ogretmen Model is Invalid");

        // DELETE: api/Ogretmen/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Ogretmen.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _ogretmenManager.Delete(id);
    }
}
