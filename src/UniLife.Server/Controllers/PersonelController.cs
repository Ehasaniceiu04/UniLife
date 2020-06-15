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
    public class PersonelController : ControllerBase
    {
        private readonly IPersonelManager _personelManager;

        public PersonelController(IPersonelManager personelManager)
        {
            _personelManager = personelManager;
        }

        // GET: api/Personel
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _personelManager.Get();


        // GET: api/Personel/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _personelManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Personel Model is Invalid");

        // POST: api/Personel
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] PersonelDto personelDto)
            => ModelState.IsValid ?
                await _personelManager.Create(personelDto) :
                new ApiResponse(Status400BadRequest, "Personel Model is Invalid");

        // Put: api/Personel
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] PersonelDto personelDto)
            => ModelState.IsValid ?
                await _personelManager.Update(personelDto) :
                new ApiResponse(Status400BadRequest, "Personel Model is Invalid");

        // DELETE: api/Personel/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Personel.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _personelManager.Delete(id);
    }
}
