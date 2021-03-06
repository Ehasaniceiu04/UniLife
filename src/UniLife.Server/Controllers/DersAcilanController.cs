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
        [Authorize(Permissions.DersAcilan.Read)]
        public async Task<ApiResponse> Get()
            => await _dersAcilanManager.Get();

        // GET: api/DersAcilan/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersAcilanManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [Obsolete("öğrencinin kayıtlı olduğu dersleri sınıf componentinden çekersen kullanabilirsin. Biz parenttan yolluyoruz.")]
        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetKayitliDerssByOgrenciId/{ogrenciId}/{donemId}")]
        public async Task<ApiResponse> GetKayitliDerssByOgrenciId(int ogrenciId,int donemId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetKayitliDerssByOgrenciId(ogrenciId, donemId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetKayitliDerssByOgrenciIdDonemId/{ogrenciId}/{donemId}")]
        public async Task<ApiResponse> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetKayitliDerssByOgrenciIdDonemId(ogrenciId, donemId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetDersAcilanSubelerByDersKod/{dersKod}/{donemId}/{programId}")]
        public async Task<ApiResponse> GetDersAcilanSubelerByDersKod(string dersKod,int donemId,int programId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetDersAcilanSubelerByDersKod(dersKod, donemId, programId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetDersAcilanSpecByDersAcId/{dersAcilanId}")]
        public async Task<ApiResponse> GetDersAcilanSpecByDersAcId(int dersAcilanId)
            => ModelState.IsValid ?
                await _dersAcilanManager.GetDersAcilanSpecByDersAcId(dersAcilanId) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");
        

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("ByZorunlu/{isZorunlu}")]
        public async Task<ApiResponse> ByZorunlu(bool isZorunlu,int sinif)
            => ModelState.IsValid ?
                await _dersAcilanManager.ByZorunlu(isZorunlu) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        
        [HttpPost]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("PostDersAcilansByFilters")]
        public async Task<ApiResponse> PostDersAcilansByFilters([FromBody] SinavDersAcDto sinavDersAcDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.PostDersAcilansByFilters(sinavDersAcDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpPost]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("DersAcilansByLongFilters")]
        public async Task<ApiResponse> DersAcilansByLongFilters([FromBody] ReqDersAcilansByLongFilters reqDersAcilansByLongFilters)
            => ModelState.IsValid ?
                await _dersAcilanManager.DersAcilansByLongFilters(reqDersAcilansByLongFilters) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


        [HttpPost]
        [Authorize(Permissions.DersAcilan.Create)]
        [Route("PostCreateNewSubesAndUpdateOgrenciSubes")]
        public async Task<ApiResponse> PostCreateNewSubesAndUpdateOgrenciSubes([FromBody] SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.PostCreateNewSubesAndUpdateOgrenciSubes(subeDersAcilanOgrenciCreateDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        



        // POST: api/DersAcilan
        [HttpPost]
        [Authorize(Permissions.DersAcilan.Create)]
        public async Task<ApiResponse> Post([FromBody] DersAcilanDto dersAcilanDto)
            => ModelState.IsValid ?
                await _dersAcilanManager.Create(dersAcilanDto) :
                new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        // Put: api/DersAcilan
        [HttpPut]
        [Authorize(Permissions.DersAcilan.Update)]
        public async Task<ApiResponse> Put([FromBody] DersAcilanDto dersAcilanDto)
        {
            if (ModelState.IsValid)
            {
                return await _dersAcilanManager.Update(dersAcilanDto);
            }
            else
                return new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");
        }

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Update)]
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
        [Route("CreateDersAcilanByDers")]
        [Authorize(Permissions.DersAcilan.Create)]
        public async Task<ApiResponse> CreateDersAcilanByDers([FromBody] DersAcDto dersAcDto)
            => ModelState.IsValid ?
                    await _dersAcilanManager.CreateDersAcilanByDers(dersAcDto) :
                    new ApiResponse(Status400BadRequest, "DersAcDto Model is Invalid");


        
        [HttpPost]
        [Route("GetAcilanDersByFilterDto")]
        [Authorize(Permissions.DersAcilan.Read)]
        public async Task<ApiResponse> GetAcilanDersByFilterDto([FromBody] DersAcilanFilterDto dersAcilanFilterDto)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetAcilanDersByFilterDto(dersAcilanFilterDto) :
                    new ApiResponse(Status400BadRequest, "DersAcilanFilterDto Model is Invalid");

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetAcilanDerssByOgrenciId/{ogrenciId}/{pageSinif}/{pageDonemId}")]
        public async Task<ApiResponse> GetAcilanDerssByOgrenciId(int ogrenciId, int pageSinif, int pageDonemId)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetAcilanDerssByOgrenciId(ogrenciId, pageSinif, pageDonemId) :
                    new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetMufredatDersByOgrenciId/{ogrenciId}")]
        public async Task<ApiResponse> GetMufredatDersByOgrenciId(int ogrenciId)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetMufredatDersByOgrenciId(ogrenciId) :
                    new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");


        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Route("GetDonemDersByOgrenciId/{ogrenciId}")]
        public async Task<ApiResponse> GetDonemDersByOgrenciId(int ogrenciId)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetDonemDersByOgrenciId(ogrenciId) :
                    new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");
        

        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        [Authorize(Roles = "Administrator,Personel")]
        [Route("GetDersAcilansByMufredat/{mufredatId}/{donemId}/{sinif?}")]
        public async Task<ApiResponse> GetDersAcilansByMufredat(int mufredatId,int donemId,int? sinif)
            => ModelState.IsValid ?
                    await _dersAcilanManager.GetDersAcilansByMufredat(mufredatId, donemId, sinif) :
                    new ApiResponse(Status400BadRequest, "DersAcilan Model is Invalid");

        

    }
}
