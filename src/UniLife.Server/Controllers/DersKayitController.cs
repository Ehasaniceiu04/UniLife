using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Controllers
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
        //[Authorize(Permissions.DersKayit.Read)]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _dersKayitManager.Get();

        // GET: api/DersKayit/5
        [HttpGet("{id}")]
        [Authorize(Permissions.DersKayit.Read)]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _dersKayitManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        //[HttpGet]
        //[Authorize]
        //[Route("GetDersKayitByMufredatId/{mufredatId}")]
        //public async Task<ApiResponse> GetDersKayitByMufredatId(int mufredatId)
        //    => ModelState.IsValid ?
        //        await _dersKayitManager.GetDersKayitByMufredatId(mufredatId) :
        //        new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");


        // POST: api/DersKayit
        [HttpPost]
        [Authorize(Permissions.DersKayit.Create)]
        public async Task<ApiResponse> Post([FromBody] DersKayitDto dersKayitDto)
            => ModelState.IsValid ?
                await _dersKayitManager.Create(dersKayitDto) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        // Put: api/DersKayit
        [HttpPut]
        [Authorize(Permissions.DersKayit.Update)]
        public async Task<ApiResponse> Put([FromBody] DersKayitDto dersKayitDto)
            => ModelState.IsValid ?
                await _dersKayitManager.Update(dersKayitDto) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        // DELETE: api/DersKayit/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.DersKayit.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _dersKayitManager.Delete(id);

        [HttpPost]
        [Route("OgrenciKayitToDerss")]
        [Authorize(Permissions.DersKayit.Create)]
        public async Task<ApiResponse> OgrenciKayitToDerss([FromBody] List<DersKayitDto> ogrenciDersKayitDtos)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.OgrenciKayitToDerss(ogrenciDersKayitDtos);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }

        [HttpDelete]
        [Route("DeleteByOgrId_DersId/{ogrenciId}/{dersId}")]
        [Authorize(Permissions.DersKayit.Delete)]
        public async Task<ApiResponse> DeleteByOgrId_DersId(int ogrenciId,int dersId)
            => await _dersKayitManager.DeleteByOgrId_DersId(ogrenciId, dersId);


        [HttpPut]
        [Authorize(Permissions.DersKayit.Update)]
        [Route("PutUpdateOgrencisDersKayits")]
        public async Task<ApiResponse> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto)
           => ModelState.IsValid ?
               await _dersKayitManager.PutUpdateOgrencisDersKayits(putUpdateOgrencisDersKayitsDto) :
               new ApiResponse(Status400BadRequest, "PutUpdateOgrencisDersKayitsDto Model is Invalid");


        [HttpPut]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Permissions.DersAcilan.Delete)]
        [Route("PutUpdateOgrencisDersKayitsDeleteExSubes")]
        public async Task<ApiResponse> PutUpdateOgrencisDersKayitsDeleteExSubes(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
           => ModelState.IsValid ?
               await _dersKayitManager.PutUpdateOgrencisDersKayitsDeleteExSubes(reqEntityIdWithOtherEntitiesIds) :
               new ApiResponse(Status400BadRequest, "PutUpdateOgrencisDersKayitsDeleteExSubes Model is Invalid");

        [HttpGet]
        [Authorize(Permissions.DersKayit.Read)]
        [Route("GetOgrenciDersKayitsByDers/{dersAcilanId}")]
        public async Task<ApiResponse> GetOgrenciDersKayitsByDers(int dersAcilanId)
            => ModelState.IsValid ?
                await _dersKayitManager.GetOgrenciDersKayitsByDers(dersAcilanId) :
                new ApiResponse(Status400BadRequest, "DersKayit Model is Invalid");

        
        [HttpPost]
        [Route("HedefKaynakOgrAktar")]
        [Authorize(Permissions.DersKayit.Create)]
        [Authorize(Permissions.DersKayit.Delete)]
        public async Task<ApiResponse> HedefKaynakOgrAktar([FromBody] HedefKaynakDto hedefKaynakDto)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.HedefKaynakOgrAktar(hedefKaynakDto);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }

        [HttpPost]
        [Route("HedefKaynakOgrDersKayit")]
        [Authorize(Permissions.DersKayit.Create)]
        [Authorize(Permissions.DersKayit.Delete)]
        public async Task<ApiResponse> HedefKaynakOgrDersKayit([FromBody] HedefKaynakDto hedefKaynakDto)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.HedefKaynakOgrDersKayit(hedefKaynakDto);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }


        [HttpPost]
        [Route("Onayla")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> Onayla([FromBody] List<int> Ids)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.Onayla(Ids);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }

        [HttpPost]
        [Route("OnayKaldir")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> OnayKaldir([FromBody] List<int> Ids)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.OnayKaldir(Ids);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }

        

        [HttpGet]
        [Route("Harflendir/{dersAcilanId}")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> Harflendir( int dersAcilanId)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.Harflendir(dersAcilanId);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }
        [HttpGet]
        [Route("KurulHarflendir/{dersAcilanId}")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> KurulHarflendir(int dersAcilanId)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.KurulHarflendir(dersAcilanId);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }
        

        [HttpGet]
        [Route("ButHarflendir/{dersAcilanId}")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> ButHarflendir(int dersAcilanId)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.ButHarflendir(dersAcilanId);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }

        [HttpGet]
        [Route("GetOgrDersHarfs/{dersAcilanId}")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> GetOgrDersHarfs(int dersAcilanId)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.GetOgrDersHarfs(dersAcilanId);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }

        [HttpGet]
        [Route("GetOgrKurulSonDersHarfs/{dersAcilanId}")]
        [Authorize(Permissions.DersKayit.Update)]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> GetOgrKurulSonDersHarfs(int dersAcilanId)
        {
            if (ModelState.IsValid)
            {
                return await _dersKayitManager.GetOgrKurulSonDersHarfs(dersAcilanId);
            }
            else
            {
                return new ApiResponse(Status400BadRequest, "DersKayitDto Model is Invalid");
            }
        }






    }
}
