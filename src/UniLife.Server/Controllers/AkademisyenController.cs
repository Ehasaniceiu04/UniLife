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
    public class AkademisyenController : ControllerBase
    {
        private readonly IAkademisyenManager _akademisyenManager;

        public AkademisyenController(IAkademisyenManager akademisyenManager)
        {
            _akademisyenManager = akademisyenManager;
        }

        // GET: api/Akademisyen
        [HttpGet]
        [Authorize(Permissions.Akademisyen.Read)]
        public async Task<ApiResponse> Get()
            => await _akademisyenManager.Get();


        // GET: api/Akademisyen/5
        [HttpGet("{id}")]
        [Authorize(Permissions.Akademisyen.Read)]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _akademisyenManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Akademisyen Model is Invalid");

        // POST: api/Akademisyen
        [HttpPost]
        [Authorize(Permissions.Akademisyen.Create)]
        public async Task<ApiResponse> Post([FromBody] AkademisyenDto akademisyenDto)
            => ModelState.IsValid ?
                await _akademisyenManager.Create(akademisyenDto) :
                new ApiResponse(Status400BadRequest, "Akademisyen Model is Invalid");

        // Put: api/Akademisyen
        [HttpPut]
        [Authorize(Permissions.Akademisyen.Update)]
        public async Task<ApiResponse> Put([FromBody] AkademisyenDto akademisyenDto)
            => ModelState.IsValid ?
                await _akademisyenManager.Update(akademisyenDto) :
                new ApiResponse(Status400BadRequest, "Akademisyen Model is Invalid");

        // DELETE: api/Akademisyen/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Akademisyen.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _akademisyenManager.Delete(id);
    }
}
