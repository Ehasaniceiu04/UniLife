using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversiteController : ControllerBase
    {
        private readonly IUniversiteManager _universiteManager;

        public UniversiteController(IUniversiteManager universiteManager)
        {
            _universiteManager = universiteManager;
        }

        // GET: api/Universite
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _universiteManager.Get();

        // GET: api/Universite/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _universiteManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Universite Model is Invalid");

        // POST: api/Universite
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] UniversiteDto universiteDto)
            => ModelState.IsValid ?
                await _universiteManager.Create(universiteDto) :
                new ApiResponse(Status400BadRequest, "Universite Model is Invalid");

        // Put: api/Universite
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] UniversiteDto universiteDto)
            => ModelState.IsValid ?
                await _universiteManager.Update(universiteDto) :
                new ApiResponse(Status400BadRequest, "Universite Model is Invalid");

        // DELETE: api/Universite/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Universite.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _universiteManager.Delete(id);
    }
}
