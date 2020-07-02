using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Controllers
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

        [Obsolete("öğrencinin kayıtlı olduğu dersleri sınıf componentinden çekersen kullanabilirsin. Biz parenttan yolluyoruz.")]
        [HttpGet]
        [AllowAnonymous]
        [Route("GetKayitliDerssByOgrenciId/{ogrenciId}/{sinif}/{donemId}")]
        public async Task<ApiResponse> GetKayitliDerssByOgrenciId(int ogrenciId,int sinif,int donemId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetKayitliDerssByOgrenciId(ogrenciId, sinif, donemId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpGet]
        [AllowAnonymous]
        [Route("GetKayitliDerssByOgrenciIdDonemId/{ogrenciId}/{donemId}")]
        public async Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetKayitliDerssByOgrenciIdDonemId(ogrenciId, donemId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpGet]
        [AllowAnonymous]
        [Route("GetDersAcilanSubelerByDersKod/{dersKod}")]
        public async Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(string dersKod)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetKayitliDerssByOgrenciIdDonemId(dersKod) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


        [HttpGet]
        [AllowAnonymous]
        [Route("GetDersAcilanSpecByDersAcId/{dersAcilanId}")]
        public async Task<ApiResponse> GetDersAcilanSpecByDersAcId(int dersAcilanId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetDersAcilanSpecByDersAcId(dersAcilanId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");
        

        [HttpGet]
        [AllowAnonymous]
        [Route("ByZorunlu/{isZorunlu}")]
        public async Task<ApiResponse> ByZorunlu(bool isZorunlu)
            => ModelState.IsValid ?
                await _dersAcilanManager.ByZorunlu(isZorunlu) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        
        [HttpPost]
        [AllowAnonymous]
        [Route("PostDersAcilansByFilters")]
        public async Task<ApiResponse> PostDersAcilansByFilters([FromBody] SinavDersAcDto sinavDersAcDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.PostDersAcilansByFilters(sinavDersAcDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpPost]
        [AllowAnonymous]
        [Route("DersAcilansByLongFilters")]
        public async Task<ApiResponse> DersAcilansByLongFilters([FromBody] ReqDersAcilansByLongFilters reqDersAcilansByLongFilters)
            => ModelState.IsValid ?
                await _dersAcilanManager.DersAcilansByLongFilters(reqDersAcilansByLongFilters) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


        [HttpPost]
        [AllowAnonymous]
        [Route("PostCreateNewSubesAndUpdateOgrenciSubes")]
        public async Task<ApiResponse> PostCreateNewSubesAndUpdateOgrenciSubes([FromBody] SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.PostCreateNewSubesAndUpdateOgrenciSubes(subeDersAcilanOgrenciCreateDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        



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

        [HttpGet]
        [AllowAnonymous]
        [Route("UpdateDersAcilanAkademsiyen/{dersAcilanId}/{akademisyenId}")]
        public async Task<ApiResponse> UpdateDersAcilanAkademsiyen(int dersAcilanId, int akademisyenId)
           => ModelState.IsValid ?
               await _dersAcilanManager.UpdateDersAcilanAkademsiyen(dersAcilanId, akademisyenId) :
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
        [Route("GetAcilanDersByMufredatId/{mufredatId}/{sinif}/{donemId}")]
        public async Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId,int sinif,int donemId)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetAcilanDersByMufredatId(mufredatId, sinif, donemId) :
                    new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");





    }
}
