using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DersKayitController : ControllerBase
    {
        private readonly IDersKayitManager _dersKayitManager;

        public DersKayitController(IDersKayitManager dersKayitManager)
        {
            _dersKayitManager = dersKayitManager;
        }

        // GET: api/DersKayit
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _dersKayitManager.Get();

        // GET: api/DersKayit/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersKayitManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("GetDersKayitByMufredatId/{mufredatId}")]
        //public async Task<ApiResponse> GetDersKayitByMufredatId(int mufredatId)
        //    => ModelState.IsValid ?
        //        await _dersKayitManager.GetDersKayitByMufredatId(mufredatId) :
        //        new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");


        // POST: api/DersKayit
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] DersKayitDto dersKayitDto)
            => ModelState.IsValid ?
                await _dersKayitManager.Create(dersKayitDto) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        // Put: api/DersKayit
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] DersKayitDto dersKayitDto)
            => ModelState.IsValid ?
                await _dersKayitManager.Update(dersKayitDto) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        // DELETE: api/DersKayit/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.DersKayit.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _dersKayitManager.Delete(id);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("CreateDersKayitByDers")]
        //public async Task<ApiResponse> CreateDersKayitByDers([FromBody] DersAcDto dersAcDto)
        //    => ModelState.IsValid ?
        //            await _dersKayitManager.CreateDersKayitByDers(dersAcDto) :
        //            new ApiResponse(Status400BadRequest, "DersAcDto Model is Invalid");


        
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("GetAcilanDersByFilterDto")]
        //public async Task<ApiResponse> GetAcilanDersByFilterDto([FromBody] DersKayitFilterDto dersKayitFilterDto)
        //    => ModelState.IsValid ?
        //            await _dersKayitManager.GetAcilanDersByFilterDto(dersKayitFilterDto) :
        //            new ApiResponse(Status400BadRequest, "DersKayitFilterDto Model is Invalid");

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("GetAcilanDersByMufredatId/{mufredatId}")]
        //public async Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId)
        //    => ModelState.IsValid ?
        //            await _dersKayitManager.GetAcilanDersByMufredatId(mufredatId) :
        //            new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");


    }
}
