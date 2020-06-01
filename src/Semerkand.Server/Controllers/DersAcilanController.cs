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
    public class DersAcilanController : ControllerBase
    {
        private readonly IDersAcilanManager _dersAcilanManager;

        public DersAcilanController(IDersAcilanManager dersAcilanManager)
        {
            _dersAcilanManager = dersAcilanManager;
        }

        // GET: api/DersAcilan
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _dersAcilanManager.Get();

        // GET: api/DersAcilan/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersAcilanManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("GetDersAcilanByMufredatId/{mufredatId}")]
        //public async Task<ApiResponse> GetDersAcilanByMufredatId(int mufredatId)
        //    => ModelState.IsValid ?
        //        await _dersAcilanManager.GetDersAcilanByMufredatId(mufredatId) :
        //        new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


        // POST: api/DersAcilan
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] DersAcilanDto dersAcilanDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.Create(dersAcilanDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        // Put: api/DersAcilan
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] DersAcilanDto dersAcilanDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.Update(dersAcilanDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        // DELETE: api/DersAcilan/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.DersAcilan.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _dersAcilanManager.Delete(id);

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateDersAcilanByDers")]
        public async Task<ApiResponse> CreateDersAcilanByDers([FromBody] DersAcDto dersAcDto)
            => ModelState.IsValid ?
                    await _dersAcilanManager.CreateDersAcilanByDers(dersAcDto) :
                    new ApiResponse(Status400BadRequest, "DersAcDto Model is Invalid");


        
        [HttpPost]
        [AllowAnonymous]
        [Route("GetAcilanDersByFilterDto")]
        public async Task<ApiResponse> GetAcilanDersByFilterDto([FromBody] DersAcilanFilterDto dersAcilanFilterDto)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetAcilanDersByFilterDto(dersAcilanFilterDto) :
                    new ApiResponse(Status400BadRequest, "DersAcilanFilterDto Model is Invalid");

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAcilanDersByMufredatId/{mufredatId}")]
        public async Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetAcilanDersByMufredatId(mufredatId) :
                    new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


    }
}
