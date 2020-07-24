using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DerslikRezervController : ControllerBase
    {
        private readonly IDerslikRezervManager _derslikRezervManager;

        public DerslikRezervController(IDerslikRezervManager derslikRezervManager)
        {
            _derslikRezervManager = derslikRezervManager;
        }

        // GET: api/DerslikRezerv
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _derslikRezervManager.Get();

        // GET: api/DerslikRezerv/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _derslikRezervManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DerslikRezerv Model is Invalid");

        // POST: api/DerslikRezerv
        [HttpPost]
        [Authorize(Permissions.DerslikRezerv.Create)]
        public async Task<ApiResponse> Post([FromBody] DerslikRezervDto derslikRezervDto)
            => ModelState.IsValid ?
                await _derslikRezervManager.Create(derslikRezervDto) :
                new ApiResponse(Status400BadRequest, "DerslikRezerv Model is Invalid");

        // Put: api/DerslikRezerv
        [HttpPut]
        [Authorize(Permissions.DerslikRezerv.Update)]
        public async Task<ApiResponse> Put([FromBody] DerslikRezervDto derslikRezervDto)
            => ModelState.IsValid ?
                await _derslikRezervManager.Update(derslikRezervDto) :
                new ApiResponse(Status400BadRequest, "DerslikRezerv Model is Invalid");

        // DELETE: api/DerslikRezerv/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.DerslikRezerv.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _derslikRezervManager.Delete(id);
    }
}
