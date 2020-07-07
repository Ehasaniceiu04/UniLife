using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Builder;
using UniLife.Storage;
using System.Collections.Generic;
using UniLife.Shared.DataModels;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciController : ControllerBase
    {
        private readonly IOgrenciManager _ogrenciManager;
        //private readonly IApplicationDbContext _applicationDbContext;

        public OgrenciController(IOgrenciManager ogrenciManager, IApplicationDbContext applicationDbContext)
        {
            _ogrenciManager = ogrenciManager;
            //_applicationDbContext = applicationDbContext;
        }

        // GET: api/Ogrenci
        [HttpGet]
        //[AllowAnonymous]
        //[Authorize(Permissions.Ogrenci.Read)]
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

        [HttpGet]
        [Route("GetOgrenciQuery")]
        [AllowAnonymous]
        public async Task<ApiResponse> GetOgrenciQuery([FromQuery]OgrenciDto Ogrenci)
        {
            return await _ogrenciManager.GetOgrenciQuery(Ogrenci);
        }


        // GET: api/Ogrenci/GetOgrenciListBySinavId/5
        [HttpGet]
        [Route("GetOgrenciListBySinavId/{sinavId}")]
        [AllowAnonymous]
        public async Task<ApiResponse> GetOgrenciListBySinavId(int sinavId)
            => ModelState.IsValid ?
                await _ogrenciManager.GetOgrenciListBySinavId(sinavId) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");


        [HttpGet]
        [Route("GetOgrenciListByDersAcId/{dersAcId}")]
        [AllowAnonymous]
        public async Task<ApiResponse> GetOgrenciListByDersAcId(int dersAcId)
            => ModelState.IsValid ?
                await _ogrenciManager.GetOgrenciListByDersAcId(dersAcId) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        


        // POST: api/Ogrenci
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] OgrenciDto ogrenciDto)
            => ModelState.IsValid ?
                await _ogrenciManager.Create(ogrenciDto) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        [HttpPost]
        [AllowAnonymous]
        [Route("SetDanismanToOgrencis")]
        public async Task<ApiResponse> SetDanismanToOgrencis([FromBody] ReqSetEntityIdToOtherEntities reqSetEntityIdToOtherEntities)
            => ModelState.IsValid ?
                await _ogrenciManager.SetDanismanToOgrencis(reqSetEntityIdToOtherEntities) :
                new ApiResponse(Status400BadRequest, "SetDanismanToOgrencis is Invalid");

        

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
