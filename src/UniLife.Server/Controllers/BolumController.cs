using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.DataModels;
using UniLife.CommonUI.Pages.Definitions.Tabs;
using System.Linq;
using System;
using System.Collections.Generic;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BolumController : ControllerBase
    {
        private readonly IBolumManager _bolumManager;

        public BolumController(IBolumManager bolumManager)
        {
            _bolumManager = bolumManager;
        }

        // GET: api/Bolum
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _bolumManager.Get();

        // GET: api/Bolum/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _bolumManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        // POST: api/Bolum
        [HttpPost]
        [Authorize(Permissions.Bolum.Create)]
        public async Task<ApiResponse> Post([FromBody] BolumDto bolumDto)
            => ModelState.IsValid ?
                await _bolumManager.Create(bolumDto) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        // Put: api/Bolum
        [HttpPut]
        [Authorize(Permissions.Bolum.Update)]
        public async Task<ApiResponse> Put([FromBody] BolumDto bolumDto)
            => ModelState.IsValid ?
                await _bolumManager.Update(bolumDto) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        // DELETE: api/Bolum/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Bolum.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _bolumManager.Delete(id);




        [HttpGet]
        [Authorize(Permissions.Bolum.Read)]
        [Route("GetBolumByFakulteIds/{fakulteIds}")]
        public async Task<ApiResponse> GetBolumByFakulteIds(string fakulteIds)
        => ModelState.IsValid ?
                await _bolumManager.GetBolumByFakulteId(fakulteIds.Replace(" ", "").Split(',')) :
                new ApiResponse(Status400BadRequest, "Bolum Model is Invalid");

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllQueryble")]
        public object GetAllQueryble()
        {
            IQueryable<Bolum> data = _bolumManager.GetAllQueryable();
            var count = data.Count();
            var queryString = Request.Query;
            if (queryString.Keys.Contains("$inlinecount"))
            {
                Microsoft.Extensions.Primitives.StringValues Take;
                Microsoft.Extensions.Primitives.StringValues Skip;
                Microsoft.Extensions.Primitives.StringValues Fiter;
                string filter = (queryString.TryGetValue("$filter", out Fiter)) ? Fiter[0].ToString() : "";
                int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;
                int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : data.Count();
                var asd = new { Items = data.Skip(skip).Take(top), Count = count };
                return asd;
            }
            else
            {
                return data;
            }
        }
    }
}
