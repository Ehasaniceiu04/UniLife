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
    public class DersKancaController : ControllerBase
    {
        private readonly IDersKancaManager _dersKancaManager;

        public DersKancaController(IDersKancaManager dersKancaManager)
        {
            _dersKancaManager = dersKancaManager;
        }

        // GET: api/DersKanca
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _dersKancaManager.Get();

        // GET: api/DersKanca/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersKancaManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DersKanca Model is Invalid");

        // POST: api/DersKanca
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] DersKancaDto dersKancaDto)
            => ModelState.IsValid ?
                await _dersKancaManager.Create(dersKancaDto) :
                new ApiResponse(Status400BadRequest, "DersKanca Model is Invalid");

        // Put: api/DersKanca
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] DersKancaDto dersKancaDto)
            => ModelState.IsValid ?
                await _dersKancaManager.Update(dersKancaDto) :
                new ApiResponse(Status400BadRequest, "DersKanca Model is Invalid");

        // DELETE: api/DersKanca/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _dersKancaManager.Delete(id);
    }
}
