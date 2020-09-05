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
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciController : ControllerBase
    {
        private readonly IOgrenciManager _ogrenciManager;
        private readonly ILogger<OgrenciController> _logger;
        //private readonly IApplicationDbContext _applicationDbContext;

        public OgrenciController(IOgrenciManager ogrenciManager, ILogger<OgrenciController> logger)//, IApplicationDbContext applicationDbContext
        {
            _ogrenciManager = ogrenciManager;
            _logger = logger;
            //_applicationDbContext = applicationDbContext;
        }

        // GET: api/Ogrenci
        [HttpGet]
        [Authorize(Roles ="Administrator,Personel")]
        public async Task<ApiResponse> Get()
        {
            return await _ogrenciManager.Get();
        }
            //=> await _ogrenciManager.Get();

        // GET: api/Ogrenci/5
        [HttpGet("{id}")]
        [Authorize(Permissions.Ogrenci.Read)]
        public async Task<ApiResponse> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var ogrenci = await _ogrenciManager.Get(id);
                if (User.IsInRole("Ogrenci"))
                {
                    return await CheckIsOgrenciHimSelf(id, ogrenci);
                }

                return ogrenci;
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "Öğrenci bilgilerinde eksik var.");
            }
        }

        private async Task<ApiResponse> CheckIsOgrenciHimSelf(int id, ApiResponse ogrenci)
        {
            if (User.GetSubjectId() == (ogrenci.Result as OgrenciDto).ApplicationUserId.ToString())
            {
                return ogrenci;
            }
            else
            {
                _logger.LogError($"{User.GetSubjectId()} öğrencisi, {id} id li öğrenci bilgilerine ulaşmaya çalıştı.");
                return new ApiResponse(Status401Unauthorized, "Başka öğrencilerin bilgilerini görme yetkiniz yok. Bu işlem adınıza loglanmıştır!");
            }
        }


        // GET: api/Ogrenci/GetOgrenciWithRelations/5
        [HttpGet]
        [Route("GetOgrenciWithRelations/{id}")]
        [Authorize(Permissions.Ogrenci.Read)]
        public async Task<ApiResponse> GetOgrenciWithRelations(int id)
        {
            if (ModelState.IsValid)
            {
                var ogrenci = await _ogrenciManager.GetOgrenciWithRelations(id);
                if (User.IsInRole("Ogrenci"))
                {
                    return await CheckIsOgrenciHimSelf(id, ogrenci);
                }

                return ogrenci;
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "Öğrenci bilgilerinde eksik var.");
            }
        }

        //[HttpGet]
        //[Route("GetOgrenciQuery")]
        //[AllowAnonymous]
        //public async Task<ApiResponse> GetOgrenciQuery([FromQuery]OgrenciDto Ogrenci)
        //{
        //    return await _ogrenciManager.GetOgrenciQuery(Ogrenci);
        //}


        // GET: api/Ogrenci/GetOgrenciListBySinavId/5
        [HttpGet]
        [Route("GetOgrenciListBySinavId/{sinavId}")]
        [Authorize(Permissions.Ogrenci.Read)]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> GetOgrenciListBySinavId(int sinavId)
            => ModelState.IsValid ?
                await _ogrenciManager.GetOgrenciListBySinavId(sinavId) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");


        [HttpGet]
        [Route("GetOgrenciListByDersAcId/{dersAcId}")]
        [Authorize(Permissions.Ogrenci.Read)]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> GetOgrenciListByDersAcId(int dersAcId)
            => ModelState.IsValid ?
                await _ogrenciManager.GetOgrenciListByDersAcId(dersAcId) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        


        // POST: api/Ogrenci
        [HttpPost]
        [Authorize(Permissions.Ogrenci.Create)]
        public async Task<ApiResponse> Post([FromBody] OgrenciDto ogrenciDto)
            => ModelState.IsValid ?
                await _ogrenciManager.Create(ogrenciDto) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        [HttpPost]
        [Route("SetDanismanToOgrencis")]
        [Authorize(Permissions.Ogrenci.Update)]
        public async Task<ApiResponse> SetDanismanToOgrencis([FromBody] ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds)
            => ModelState.IsValid ?
                await _ogrenciManager.SetDanismanToOgrencis(ReqEntityIdWithOtherEntitiesIds) :
                new ApiResponse(Status400BadRequest, "SetDanismanToOgrencis is Invalid");

        [HttpPost]
        [Route("SetMufredatToOgrencis")]
        [Authorize(Permissions.Ogrenci.Update)]
        public async Task<ApiResponse> SetMufredatToOgrencis([FromBody] ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds)
            => ModelState.IsValid ?
                await _ogrenciManager.SetMufredatToOgrencis(ReqEntityIdWithOtherEntitiesIds) :
                new ApiResponse(Status400BadRequest, "SetMufredatToOgrencis is Invalid");

        [HttpPost]
        [Route("SetOgrDurumToOgrencis")]
        [Authorize(Permissions.Ogrenci.Update)]
        public async Task<ApiResponse> SetOgrDurumToOgrencis([FromBody] ReqEntityIdWithOtherEntitiesIds ReqEntityIdWithOtherEntitiesIds)
            => ModelState.IsValid ?
                await _ogrenciManager.SetOgrDurumToOgrencis(ReqEntityIdWithOtherEntitiesIds) :
                new ApiResponse(Status400BadRequest, "SetOgrDurumToOgrencis is Invalid");



        [HttpPost]
        [Route("OgrencisSinifAtlat")]
        [Authorize(Permissions.Ogrenci.Update)]
        public async Task<ApiResponse> OgrencisSinifAtlat([FromBody] ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
            => ModelState.IsValid ?
                await _ogrenciManager.OgrencisSinifAtlat(reqEntityIdWithOtherEntitiesIds) :
                new ApiResponse(Status400BadRequest, "OgrencisSinifAtlat is Invalid");


        


        // Put: api/Ogrenci
        [HttpPut]
        [Authorize(Permissions.Ogrenci.Update)]
        public async Task<ApiResponse> Put([FromBody] OgrenciDto ogrenciDto)
            => ModelState.IsValid ?
                await _ogrenciManager.Update(ogrenciDto) :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");

        // DELETE: api/Ogrenci/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Ogrenci.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _ogrenciManager.Delete(id);


        [HttpGet]
        [Route("SinifAtlaTemizle")]
        [Authorize(Permissions.Ogrenci.Update)]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> SinifAtlaTemizle()
            => ModelState.IsValid ?
                await _ogrenciManager.SinifAtlaTemizle() :
                new ApiResponse(Status400BadRequest, "Ogrenci Model is Invalid");
        
    }
}
