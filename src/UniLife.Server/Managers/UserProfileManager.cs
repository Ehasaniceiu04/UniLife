using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Account;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.DataInterfaces;
using System.Linq;

namespace UniLife.Server.Managers
{
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IUserProfileStore _userProfileStore;
        private readonly IAkademisyenStore _akademisyenStore;
        private readonly IOgrenciStore _ogrenciStore;
        private readonly IDonemStore _donemStore;
        private readonly IUserProgramYetkiStore _userProgramYetkiStore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileManager(IUserProfileStore userProfileStore, IHttpContextAccessor httpContextAccessor,
            IAkademisyenStore akademisyenStore,
            IOgrenciStore ogrenciStore,
            IDonemStore donemStore,
            IUserProgramYetkiStore userProgramYetkiStore)
        {
            _userProfileStore = userProfileStore;
            _httpContextAccessor = httpContextAccessor;
            _akademisyenStore = akademisyenStore;
            _ogrenciStore = ogrenciStore;
            _donemStore = donemStore;
            _userProgramYetkiStore = userProgramYetkiStore;
        }

        public async Task<string> GetLastPageVisited(string userName)
        => await _userProfileStore.GetLastPageVisited(userName);

        public async Task<ApiResponse> Get()
        {
            var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);

            var userProfile = await _userProfileStore.Get(userId);

            return new ApiResponse(Status200OK, "Retrieved User Profile", userProfile);
        }

        public async Task<ApiResponse> Upsert(UserProfileDto userProfileDto)
        {
            try
            {
                await _userProfileStore.Upsert(userProfileDto);

                return new ApiResponse(Status200OK, "Updated User Profile");
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve User Profile");
            }
        }

        public async Task<ApiResponse> GetUserProgramYetkiListState()
        {
            var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);

            var userYetkis = await _userProgramYetkiStore.GetUserProgramYetkiListState(userId);

            return new ApiResponse(Status200OK, "Retrieved Akademisyen state", userYetkis);
        }

        public async Task<ApiResponse> GetAkademisyenState()
        {
            var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);

            var akademisyen = await _akademisyenStore.GetAkademisyenState(userId);

            return new ApiResponse(Status200OK, "Retrieved Akademisyen state", akademisyen);
        }

        public async Task<ApiResponse> GetOgrenciState()
        {
            var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);

            var ogrenci = await _ogrenciStore.GetOgrenciState(userId);

            return new ApiResponse(Status200OK, "Retrieved Ogrenci state", ogrenci);
        }

        public async Task<ApiResponse> GetDonemState()
        {
            var donem = await _donemStore.GetWhere(x=>x.Durum == true);

            return new ApiResponse(Status200OK, "Retrieved Ogrenci state", donem.FirstOrDefault());
        }

        
    }
}
