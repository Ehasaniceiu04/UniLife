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
    public class FakulteTurController : ControllerBase
    {
        private readonly IFakulteTurManager _fakulteTurManager;

        public FakulteTurController(IFakulteTurManager fakulteTurManager)
        {
            _fakulteTurManager = fakulteTurManager;
        }

        // GET: api/FakulteTur
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _fakulteTurManager.Get();


        // GET: api/FakulteTur/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _fakulteTurManager.Get(id) :
                new ApiResponse(Status400BadRequest, "FakulteTur Model is Invalid");

        // POST: api/FakulteTur
        [HttpPost]
        [Authorize(Permissions.Fakulte.Create)]
        public async Task<ApiResponse> Post([FromBody] FakulteTurDto fakulteTurDto)
            => ModelState.IsValid ?
                await _fakulteTurManager.Create(fakulteTurDto) :
                new ApiResponse(Status400BadRequest, "FakulteTur Model is Invalid");

        // Put: api/FakulteTur
        [HttpPut]
        [Authorize(Permissions.Fakulte.Update)]
        public async Task<ApiResponse> Put([FromBody] FakulteTurDto fakulteTurDto)
            => ModelState.IsValid ?
                await _fakulteTurManager.Update(fakulteTurDto) :
                new ApiResponse(Status400BadRequest, "FakulteTur Model is Invalid");

        // DELETE: api/FakulteTur/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Fakulte.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _fakulteTurManager.Delete(id);
    }
}
