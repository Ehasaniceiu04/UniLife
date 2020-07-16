using System;
using System.Threading.Tasks;

using static Microsoft.AspNetCore.Http.StatusCodes;

using UniLife.CommonUI.Services.Contracts;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Account;
using Newtonsoft.Json;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using Syncfusion.Blazor.CircularGauge;
using UniLife.Shared;

namespace UniLife.CommonUI
{
    public class AppState
    {
        public event Action OnChange;
        private readonly IUserProfileApi _userProfileApi;

        public UserProfileDto UserProfile { get; set; }
        //public OgrenciDto OgrenciUserProfile { get; set; }
        public MufredatStateDto MufredatState { get; set; }

        public OgrenciDto OgrenciState { get; set; }
        public AkademisyenDto AkademisyenState { get; set; }
        public PersonelDto PersonelState { get; set; }

        public Settings AppSettings { get; set; }

        public DonemDto DonemState { get; set; }

        public int DersKayitDonemIdState { get; set; }

        public AppState(IUserProfileApi userProfileApi)
        {
            _userProfileApi = userProfileApi;
        }

        public bool IsNavOpen
        {
            get
            {
                if (UserProfile == null)
                {
                    return true;
                }
                return UserProfile.IsNavOpen;
            }
            set
            {
                UserProfile.IsNavOpen = value;
            }
        }

        //public bool IsNavOpenOgrenci
        //{
        //    get
        //    {
        //        if (OgrenciUserProfile == null)
        //        {
        //            return true;
        //        }
        //        return OgrenciUserProfile.IsNavOpen;
        //    }
        //    set
        //    {
        //        OgrenciUserProfile.IsNavOpen = value;
        //    }
        //}

        public bool IsNavMinified { get; set; }

        public async Task UpdateUserProfile()
        {
            await _userProfileApi.Upsert(UserProfile);
        }
        //public async Task UpdateOgrenciUserProfile()
        //{
        //    await _userProfileApi.UpsertOgrenci(OgrenciUserProfile);
        //}

        public async Task<UserProfileDto> GetUserProfile()
        {
            if (UserProfile != null && UserProfile.UserId != Guid.Empty)
            {
                return UserProfile;
            }

            ApiResponseDto apiResponse = await _userProfileApi.Get();

            if (apiResponse.StatusCode == Status200OK)
            {
                return JsonConvert.DeserializeObject<UserProfileDto>(apiResponse.Result.ToString());
            }
            return new UserProfileDto();
        }

        //public async Task<OgrenciDto> GetOgrenciUserProfile()
        //{
        //    if (OgrenciUserProfile != null && OgrenciUserProfile.ApplicationUserId != Guid.Empty)
        //    {
        //        return OgrenciUserProfile;
        //    }

        //    ApiResponseDto apiResponse = await _userProfileApi.GetOgrenci();

        //    if (apiResponse.StatusCode == Status200OK)
        //    {
        //        return JsonConvert.DeserializeObject<OgrenciDto>(apiResponse.Result.ToString());
        //    }
        //    return new OgrenciDto();
        //}

        public async Task UpdateUserProfileCount(int count)
        {
            UserProfile.Count = count;
            await UpdateUserProfile();
            NotifyStateChanged();
        }
        //public async Task UpdateOgrenciUserProfileCount(int count)
        //{
        //    OgrenciUserProfile.Count = count;
        //    await UpdateOgrenciUserProfile();
        //    NotifyStateChanged();
        //}

        public async Task<int> GetUserProfileCount()
        {
            if (UserProfile == null)
            {
                UserProfile = await GetUserProfile();
                return UserProfile.Count;
            }

            return UserProfile.Count;
        }
        //public async Task<int> GetOgrenciUserProfileCount()
        //{
        //    if (OgrenciUserProfile == null)
        //    {
        //        OgrenciUserProfile = await GetOgrenciUserProfile();
        //        return OgrenciUserProfile.Count;
        //    }

        //    return OgrenciUserProfile.Count;
        //}

        public async Task SaveLastVisitedUri(string uri)
        {
            if (UserProfile == null)
            {
                UserProfile = await GetUserProfile();
            }
            else
            {
                UserProfile.LastPageVisited = uri;
                await UpdateUserProfile();
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
