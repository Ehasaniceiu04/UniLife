
using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using System;
using UniLife.Shared.Dto.Definitions.Bussines;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgramYetkiController : ControllerBase
    {
        private readonly IUserProgramYetkiManager _userProgramYetkiManager;

        public UserProgramYetkiController(IUserProgramYetkiManager userProgramYetkiManager)
        {
            _userProgramYetkiManager = userProgramYetkiManager;
        }

        // GET: api/UserProgramYetki
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ApiResponse> Get()
            => await _userProgramYetkiManager.Get();

        // GET: api/UserProgramYetki/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _userProgramYetkiManager.Get(id) :
                new ApiResponse(Status400BadRequest, "UserProgramYetki Model is Invalid");

        // POST: api/UserProgramYetki
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ApiResponse> Post([FromBody] UserProgramYetkiDto userProgramYetkiDto)
            => ModelState.IsValid ?
                await _userProgramYetkiManager.Create(userProgramYetkiDto) :
                new ApiResponse(Status400BadRequest, "UserProgramYetki Model is Invalid");

        // Put: api/UserProgramYetki
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<ApiResponse> Put([FromBody] UserProgramYetkiDto userProgramYetkiDto)
            => ModelState.IsValid ?
                await _userProgramYetkiManager.Update(userProgramYetkiDto) :
                new ApiResponse(Status400BadRequest, "UserProgramYetki Model is Invalid");

        // DELETE: api/UserProgramYetki/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ApiResponse> Delete(int id)
            => await _userProgramYetkiManager.Delete(id);


        [HttpGet]
        [Authorize]
        [Route("GetUPYByUserId/{userId}")]
        public async Task<ApiResponse> GetUPYByUserId(Guid userId)
        => ModelState.IsValid ?
                await _userProgramYetkiManager.GetUPYByUserId(userId) :
                new ApiResponse(Status400BadRequest, "Model is Invalid");

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("UpdateUserProgramYetkis")]
        public async Task<ApiResponse> UpdateUserProgramYetkis([FromBody] ProgramYetkiListUserIdDto programYetkiListUserIdDto)
           => ModelState.IsValid ?
               await _userProgramYetkiManager.UpdateUserProgramYetkis(programYetkiListUserIdDto) :
               new ApiResponse(Status400BadRequest, "UpdateUserProgramYetkis Model is Invalid");
        
    }
}
