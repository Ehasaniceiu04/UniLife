﻿using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.DataModels;
using Semerkand.CommonUI.Pages.Definitions.Tabs;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonemTipController : ControllerBase
    {
        private readonly IDonemTipManager _donemTipManager;

        public DonemTipController(IDonemTipManager donemTipManager)
        {
            _donemTipManager = donemTipManager;
        }

        // GET: api/DonemTip
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
            => await _donemTipManager.Get();

        // GET: api/DonemTip/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _donemTipManager.Get(id) :
                new ApiResponse(Status400BadRequest, "DonemTip Model is Invalid");

        // POST: api/DonemTip
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] DonemTipDto donemTipDto)
            => ModelState.IsValid ?
                await _donemTipManager.Create(donemTipDto) :
                new ApiResponse(Status400BadRequest, "DonemTip Model is Invalid");

        // Put: api/DonemTip
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] DonemTipDto donemTipDto)
            => ModelState.IsValid ?
                await _donemTipManager.Update(donemTipDto) :
                new ApiResponse(Status400BadRequest, "DonemTip Model is Invalid");

        // DELETE: api/DonemTip/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Universite.Delete)]
        public async Task<ApiResponse> Delete(int id)
            => await _donemTipManager.Delete(id);
    }
}
