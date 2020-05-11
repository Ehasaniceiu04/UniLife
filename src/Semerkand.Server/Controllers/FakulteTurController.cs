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
    public class FakulteTurController : ControllerBase
    {
        private readonly IFakulteTurManager _fakulteTurManager;

        public FakulteTurController(IFakulteTurManager fakulteTurManager)
        {
            _fakulteTurManager = fakulteTurManager;
        }

        // GET: api/FakulteTur
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _fakulteTurManager.Get();


        // GET: api/FakulteTur/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _fakulteTurManager.Get(id) :
                new ApiResponse(Status400BadRequest, "FakulteTur Model is Invalid");

        // POST: api/FakulteTur
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] FakulteTurDto fakulteTurDto)
            => ModelState.IsValid ?
                await _fakulteTurManager.Create(fakulteTurDto) :
                new ApiResponse(Status400BadRequest, "FakulteTur Model is Invalid");

        // Put: api/FakulteTur
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] FakulteTurDto fakulteTurDto)
            => ModelState.IsValid ?
                await _fakulteTurManager.Update(fakulteTurDto) :
                new ApiResponse(Status400BadRequest, "FakulteTur Model is Invalid");

        // DELETE: api/FakulteTur/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Universite.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _fakulteTurManager.Delete(id);
    }
}
