using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DersController : ControllerBase
    {
        private readonly IDersManager _dersManager;

        public DersController(IDersManager dersManager)
        {
            _dersManager = dersManager;
        }

        // GET: api/Ders
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _dersManager.Get();

        // GET: api/Ders/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");

        [HttpGet]
        [AllowAnonymous]
        [Route("GetDersByMufredatId/{mufredatId}")]
        public async Task<ApiResponse> GetDersByMufredatId(int mufredatId)
            => ModelState.IsValid ?
                await _dersManager.GetDersByMufredatId(mufredatId) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");


        // POST: api/Ders
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] DersDto dersDto)
            => ModelState.IsValid ?
                await _dersManager.Create(dersDto) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");

        // Put: api/Ders
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] DersDto dersDto)
            => ModelState.IsValid ?
                await _dersManager.Update(dersDto) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");

        // DELETE: api/Ders/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Ders.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _dersManager.Delete(id);


        [HttpPost]
        [AllowAnonymous]
        [Route("GetAcilacakDers")]
        public async Task<ApiResponse> GetAcilacakDers([FromBody] DersAcDto dersAcDto)
            => ModelState.IsValid ?
                    await _dersManager.GetAcilacakDers(dersAcDto) :
                    new ApiResponse(Status400BadRequest, "DersAcDto Model is Invalid");
    }
}
