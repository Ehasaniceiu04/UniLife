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
    public class YabanciBasvuruController : ControllerBase
    {
        private readonly IYabanciBasvuruManager _yabanciBasvuruManager;

        public YabanciBasvuruController(IYabanciBasvuruManager yabanciBasvuruManager)
        {
            _yabanciBasvuruManager = yabanciBasvuruManager;
        }

        // GET: api/YabanciBasvuru
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _yabanciBasvuruManager.Get();

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("GetYabanciBasvuruByBinaId/{binaId}")]
        //public async Task<ApiResponse> GetYabanciBasvuruByBinaId(int binaId)
        //    => await _yabanciBasvuruManager.GetYabanciBasvuruByBinaId();

        // GET: api/YabanciBasvuru/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _yabanciBasvuruManager.Get(id) :
                new ApiResponse(Status400BadRequest, "YabanciBasvuru Model is Invalid");

        // POST: api/YabanciBasvuru
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] YabanciBasvuruDto yabanciBasvuruDto)
            => ModelState.IsValid ?
                await _yabanciBasvuruManager.Create(yabanciBasvuruDto) :
                new ApiResponse(Status400BadRequest, "YabanciBasvuru Model is Invalid");

        // Put: api/YabanciBasvuru
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] YabanciBasvuruDto yabanciBasvuruDto)
            => ModelState.IsValid ?
                await _yabanciBasvuruManager.Update(yabanciBasvuruDto) :
                new ApiResponse(Status400BadRequest, "YabanciBasvuru Model is Invalid");

        // DELETE: api/YabanciBasvuru/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.YabanciBasvuru.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _yabanciBasvuruManager.Delete(id);
    }
}
