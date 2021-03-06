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
    public class DerslikController : ControllerBase
    {
        private readonly IDerslikManager _derslikManager;

        public DerslikController(IDerslikManager derslikManager)
        {
            _derslikManager = derslikManager;
        }

        // GET: api/Derslik
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _derslikManager.Get();

        //[HttpGet]
        //[Authorize]
        //[Route("GetDerslikByBinaId/{binaId}")]
        //public async Task<ApiResponse> GetDerslikByBinaId(int binaId)
        //    => await _derslikManager.GetDerslikByBinaId();

        // GET: api/Derslik/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _derslikManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Derslik Model is Invalid");

        // POST: api/Derslik
        [HttpPost]
        [Authorize(Permissions.Derslik.Create)]
        public async Task<ApiResponse> Post([FromBody] DerslikDto derslikDto)
            => ModelState.IsValid ?
                await _derslikManager.Create(derslikDto) :
                new ApiResponse(Status400BadRequest, "Derslik Model is Invalid");

        // Put: api/Derslik
        [HttpPut]
        [Authorize(Permissions.Derslik.Update)]
        public async Task<ApiResponse> Put([FromBody] DerslikDto derslikDto)
            => ModelState.IsValid ?
                await _derslikManager.Update(derslikDto) :
                new ApiResponse(Status400BadRequest, "Derslik Model is Invalid");

        // DELETE: api/Derslik/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Derslik.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _derslikManager.Delete(id);



        [HttpGet]
        [Route("GetDersliksAndDerslikRezsByMufredatId/{mufredatId}/{ogrenciId}/{isSinav?}")]
        [Authorize(Permissions.Derslik.Read)]
        public async Task<ApiResponse> GetDersliksAndDerslikRezsByMufredatId(int mufredatId,int ogrenciId, bool isSinav)
            => ModelState.IsValid ?
                await _derslikManager.GetDersliksAndDerslikRezsByMufredatId(mufredatId, ogrenciId, isSinav) :
                new ApiResponse(Status400BadRequest, "Derslik Model is Invalid");

        [HttpGet]
        [Route("GetDersliksAndDerslikRezsByAkaId/{akaId}/{isSinav?}")]
        [Authorize(Permissions.Derslik.Read)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> GetDersliksAndDerslikRezsByAkaId(int akaId, bool isSinav)
            => ModelState.IsValid ?
                await _derslikManager.GetDersliksAndDerslikRezsByAkaId(akaId, isSinav) :
                new ApiResponse(Status400BadRequest, "Derslik Model is Invalid");

        


    }
}
