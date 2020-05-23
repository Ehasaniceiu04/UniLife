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
    public class OgrenciController : ControllerBase
    {
        private readonly IOgrenciManager _ogrenciManager;

        public OgrenciController(IOgrenciManager ogrenciManager)
        {
            _ogrenciManager = ogrenciManager;
        }

        // GET: api/Ogrenci
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _ogrenciManager.Get();

        // GET: api/Ogrenci/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _ogrenciManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        // GET: api/Ogrenci/5
        [HttpGet]
        [Route("GetOgrenciWithRelations/{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> GetOgrenciWithRelations(int id)
            => ModelState.IsValid ?
                await _ogrenciManager.GetOgrenciWithRelations(id) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        // POST: api/Ogrenci
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] OgrenciDto ogrenciDto)
            => ModelState.IsValid ?
                await _ogrenciManager.Create(ogrenciDto) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        // Put: api/Ogrenci
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] OgrenciDto ogrenciDto)
            => ModelState.IsValid ?
                await _ogrenciManager.Update(ogrenciDto) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        // DELETE: api/Ogrenci/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Ogrenci.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrenciManager.Delete(id);
    }
}
