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
    public class AkademikTakvimController : ControllerBase
    {
        private readonly IAkademikTakvimManager _akademikTakvimManager;

        public AkademikTakvimController(IAkademikTakvimManager akademikTakvimManager)
        {
            _akademikTakvimManager = akademikTakvimManager;
        }

        // GET: api/AkademikTakvim
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _akademikTakvimManager.Get();


        // GET: api/AkademikTakvim/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _akademikTakvimManager.Get(id) :
                new ApiResponse(Status400BadRequest, "AkademikTakvim Model is Invalid");

        // POST: api/AkademikTakvim
        [HttpPost]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Post([FromBody] AkademikTakvimDto akademikTakvimDto)
            => ModelState.IsValid ?
                await _akademikTakvimManager.Create(akademikTakvimDto) :
                new ApiResponse(Status400BadRequest, "AkademikTakvim Model is Invalid");

        // Put: api/AkademikTakvim
        [HttpPut]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Put([FromBody] AkademikTakvimDto akademikTakvimDto)
            => ModelState.IsValid ?
                await _akademikTakvimManager.Update(akademikTakvimDto) :
                new ApiResponse(Status400BadRequest, "AkademikTakvim Model is Invalid");

        // DELETE: api/AkademikTakvim/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Delete(int id)
            => await _akademikTakvimManager.Delete(id);

        [HttpGet]
        [Authorize]
        [Route("GetAkaTakByFakIdDonId/{fakulteID}/{donemId}")]
        public async Task<ApiResponse> GetAkaTakByFakIdDonId(int fakulteId, int donemId)
        => ModelState.IsValid ?
                await _akademikTakvimManager.GetAkaTakByFakIdDonId(fakulteId, donemId) :
                new ApiResponse(Status400BadRequest, "AkademikTakvim Model is Invalid");
        
        [HttpPost]
        [Authorize]
        [Route("PostChangeAllFakultesTakvim")]
        public async Task<ApiResponse> PostChangeAllFakultesTakvim([FromBody] AkademikTakvimDto akademikTakvimDto)
        => ModelState.IsValid ?
                await _akademikTakvimManager.PostChangeAllFakultesTakvim(akademikTakvimDto) :
                new ApiResponse(Status400BadRequest, "AkademikTakvim Model is Invalid");

        
    }
}
