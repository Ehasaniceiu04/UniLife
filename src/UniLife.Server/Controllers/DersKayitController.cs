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

        [HttpPost]
        [AllowAnonymous]
        [Route("OgrenciKayitToDerss")]
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
        [AllowAnonymous]
        [Route("PutUpdateOgrencisDersKayits")]
        public async Task<ApiResponse> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto)
           => ModelState.IsValid ?
               await _dersKayitManager.PutUpdateOgrencisDersKayits(putUpdateOgrencisDersKayitsDto) :
               new ApiResponse(Status400BadRequest, "PutUpdateOgrencisDersKayitsDto Model is Invalid");


    }
}
