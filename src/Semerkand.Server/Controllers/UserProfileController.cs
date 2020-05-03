using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileManager _userProfileManager;

        public UserProfileController(IUserProfileManager userProfileManager)
        {
            _userProfileManager = userProfileManager;
        }

        // GET: api/UserProfile
        [HttpGet("Get")]
        public async Task<ApiResponse> Get()
            => await _userProfileManager.Get();

        // POST: api/UserProfile
        [HttpPost("Upsert")]
        public async Task<ApiResponse> Upsert(UserProfileDto userProfile)
            => ModelState.IsValid ?
                await _userProfileManager.Upsert(userProfile) :
                new ApiResponse(Status400BadRequest, "User Model is Invalid");
    }
}
