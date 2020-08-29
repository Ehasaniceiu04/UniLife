using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Controllers
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
        [Authorize(Permissions.Ders.Read)]
        public async Task<ApiResponse> Get()
            => await _dersManager.Get();

        // GET: api/Ders/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");

        [HttpGet]
        [Route("GetDersByMufredatId/{mufredatId}")]
        public async Task<ApiResponse> GetDersByMufredatId(int mufredatId)
            => ModelState.IsValid ?
                await _dersManager.GetDersByMufredatId(mufredatId) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");


        // POST: api/Ders
        [HttpPost]
        [Authorize(Permissions.Ders.Create)]
        public async Task<ApiResponse> Post([FromBody] DersDto dersDto)
            => ModelState.IsValid ?
                await _dersManager.Create(dersDto) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");

        // Put: api/Ders
        [HttpPut]
        [Authorize(Permissions.Ders.Update)]
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
        //[Authorize(Permissions.Ders.Read)]
        [Route("GetAcilacakDersByFilterDto")]
        public async Task<ApiResponse> GetAcilacakDersByFilterDto([FromBody] DersFilterDto dersFilterDto)
            => ModelState.IsValid ?
                    await _dersManager.GetAcilacakDersByFilterDto(dersFilterDto) :
                    new ApiResponse(Status400BadRequest, "DersFilterDto Model is Invalid");

        [HttpPost]
        [Authorize(Permissions.Ders.Create)]
        [Route("CreateDersAcilansByDersId")]
        public async Task<ApiResponse> CreateDersAcilansByDersId([FromBody] int dersId)
            => ModelState.IsValid ?
                    await _dersManager.CreateDersAcilansByDersId(dersId) :
                    new ApiResponse(Status400BadRequest, "DersFilterDto Model is Invalid");


        [HttpGet]
        [Authorize(Permissions.Ders.Create)]
        [Route("AddYerineDers/{dersId}/{yerineDersId}")]
        public async Task<ApiResponse> AddYerineDers(int dersId, int yerineDersId)
        => ModelState.IsValid ?
                await _dersManager.AddYerineDers(dersId, yerineDersId) :
                new ApiResponse(Status400BadRequest, "Ders Model is Invalid");


        [HttpGet]
        [Authorize(Permissions.Ders.Update)]
        [Route("DeleteExistKancas/{dersId}")]
        public async Task<ApiResponse> DeleteExistKancas(int dersId)
            => ModelState.IsValid ?
                    await _dersManager.DeleteExistKancas(dersId) :
                    new ApiResponse(Status400BadRequest, "Ders Model is Invalid");
        

    }
}
