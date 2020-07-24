using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinavController : ControllerBase
    {
        private readonly ISinavManager _SinavManager;

        public SinavController(ISinavManager SinavManager)
        {
            _SinavManager = SinavManager;
        }

        // GET: api/Sinav
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> Get()
            => await _SinavManager.Get();


        // GET: api/Sinav/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _SinavManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Sinav Model is Invalid");

        [HttpGet]
        [Route("GetSinavListByAcilanDersId/{dersId}")]
        [Authorize(Permissions.Sinav.Read)]
        public async Task<ApiResponse> GetSinavListByAcilanDersId(int dersId)
            => ModelState.IsValid ?
                await _SinavManager.GetSinavListByAcilanDersId(dersId) :
                new ApiResponse(Status400BadRequest, "Sinav Model is Invalid");


        // POST: api/Sinav
        [HttpPost]
        [Authorize(Permissions.Sinav.Create)]
        public async Task<ApiResponse> Post([FromBody] SinavDto SinavDto)
            => ModelState.IsValid ?
                await _SinavManager.Create(SinavDto) :
                new ApiResponse(Status400BadRequest, "Sinav Model is Invalid");

        // POST: api/Sinav/PostBulkCreate
        [HttpPost]
        [Route("PostBulkCreate")]
        [Authorize(Permissions.Sinav.Create)]
        public async Task<ApiResponse> PostBulkCreate([FromBody] SinavDto SinavDto)
            => ModelState.IsValid ?
                await _SinavManager.PostBulkCreate(SinavDto) :
                new ApiResponse(Status400BadRequest, "Sinav Model is Invalid");

        //// POST: api/Sinav/PostSingleCreate
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("PostSingleCreate")]
        //public async Task<ApiResponse> PostSingleCreate([FromBody] SinavDto SinavDto)
        //    => ModelState.IsValid ?
        //        await _SinavManager.PostBulkCreate(SinavDto) :
        //        new ApiResponse(Status400BadRequest, "Sinav Model is Invalid");
        


        // Put: api/Sinav
        [HttpPut]
        [Authorize(Permissions.Sinav.Update)]
        public async Task<ApiResponse> Put([FromBody] SinavDto SinavDto)
            => ModelState.IsValid ?
                await _SinavManager.Update(SinavDto) :
                new ApiResponse(Status400BadRequest, "Sinav Model is Invalid");

        // DELETE: api/Sinav/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Sinav.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _SinavManager.Delete(id);


        
    }
}
