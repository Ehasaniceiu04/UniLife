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
    [Authorize(Roles = "Administrator,Personel")]
    public class PersonelTaskController : ControllerBase
    {
        private readonly IPersonelTaskManager _personelTaskManager;

        public PersonelTaskController(IPersonelTaskManager personelTaskManager)
        {
            _personelTaskManager = personelTaskManager;
        }

        // GET: api/PersonelTask
        [HttpGet]
        public async Task<ApiResponse> Get()
            => await _personelTaskManager.Get();

        // GET: api/PersonelTask/5
        [HttpGet("{id}")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _personelTaskManager.Get(id) :
                new ApiResponse(Status400BadRequest, "PersonelTask Model is Invalid");

        // POST: api/PersonelTask
        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] PersonelTaskDto personelTaskDto)
            => ModelState.IsValid ?
                await _personelTaskManager.Create(personelTaskDto) :
                new ApiResponse(Status400BadRequest, "PersonelTask Model is Invalid");

        // Put: api/PersonelTask
        [HttpPut]
        public async Task<ApiResponse> Put([FromBody] PersonelTaskDto personelTaskDto)
            => ModelState.IsValid ?
                await _personelTaskManager.Update(personelTaskDto) :
                new ApiResponse(Status400BadRequest, "PersonelTask Model is Invalid");

        // DELETE: api/PersonelTask/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
            => await _personelTaskManager.Delete(id);
    }
}
