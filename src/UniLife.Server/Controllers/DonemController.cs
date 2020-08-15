using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.DataModels;
using UniLife.CommonUI.Pages.Definitions.Tabs;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonemController : ControllerBase
    {
        private readonly IDonemManager _donemManager;

        public DonemController(IDonemManager donemManager)
        {
            _donemManager = donemManager;
        }

        // GET: api/Donem
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _donemManager.Get();

        // GET: api/Donem
        [HttpGet]
        [Route("Current")]
        [Authorize]
        public async Task<ApiResponse> Current()
            => await _donemManager.Current();


        // GET: api/Donem/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _donemManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");

        // POST: api/Donem
        [HttpPost]
        [Authorize(Permissions.Donem.Create)]
        public async Task<ApiResponse> Post([FromBody] DonemDto donemDto)
            => ModelState.IsValid ?
                await _donemManager.Create(donemDto) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");

        // Put: api/Donem
        [HttpPut]
        [Authorize(Permissions.Donem.Update)]
        public async Task<ApiResponse> Put([FromBody] DonemDto donemDto)
            => ModelState.IsValid ?
                await _donemManager.Update(donemDto) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");

        // DELETE: api/Donem/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Donem.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _donemManager.Delete(id);

        // POST: api/Donem
        [HttpPost]
        [Route("CreateNewDonemWithTakvim")]
        [Authorize(Permissions.Donem.Create)]
        public async Task<ApiResponse> CreateNewDonemWithTakvim([FromBody] DonemDto donemDto)
            => ModelState.IsValid ?
                await _donemManager.CreateNewDonemWithTakvim(donemDto) :
                new ApiResponse(Status400BadRequest, "Donem Model is Invalid");
    }
}
