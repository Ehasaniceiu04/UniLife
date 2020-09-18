using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UniLife.CommonUI.Services.Contracts;
using UniLife.Shared;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Account;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;
//using System.Net.Http;
//using UniLife.CommonUI.Extensions;

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
        public OgrenciDto OgrenciTempState { get; set; }
        public AkademisyenDto AkademisyenState { get; set; }
        public AkademisyenDto AkademisyenTempState { get; set; }
        public PersonelDto PersonelState { get; set; }
        public PersonelDto PersonelTempState { get; set; }

        public Settings AppSettings { get; set; }

        public DonemDto DonemState { get; set; }

        public int DersKayitDonemIdState { get; set; }

        public string UserNavigationLoadRole { get; set; }


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

        public async Task<AkademisyenDto> GetAkademisyenState()
        {
            if (AkademisyenState != null && AkademisyenState.Id != 0)
            {
                return AkademisyenState;
            }

            ApiResponseDto apiResponse = await _userProfileApi.GetAkademisyenState();

            if (apiResponse.StatusCode == Status200OK)
            {
                AkademisyenState = JsonConvert.DeserializeObject<AkademisyenDto>(apiResponse.Result.ToString());
                return AkademisyenState;
            }
            return new AkademisyenDto();
        }

        public async Task<OgrenciDto> GetOgrenciState()
        {
            if (OgrenciState != null && OgrenciState.Id != 0)
            {
                return OgrenciState;
            }

            ApiResponseDto apiResponse = await _userProfileApi.GetOgrenciState();

            if (apiResponse.StatusCode == Status200OK)
            {
                OgrenciState = JsonConvert.DeserializeObject<OgrenciDto>(apiResponse.Result.ToString());
                return OgrenciState;
            }
            return new OgrenciDto();
        }

        public async Task<DonemDto> GetDonemState()
        {
            if (DonemState != null && DonemState.Id != 0)
            {
                return DonemState;
            }

            ApiResponseDto apiResponse = await _userProfileApi.GetDonemState();

            if (apiResponse.StatusCode == Status200OK)
            {
                DonemState = JsonConvert.DeserializeObject<DonemDto>(apiResponse.Result.ToString());
                return DonemState;
            }
            return new DonemDto();
        }


        //public async Task<Settings> GetAppSettings()
        //{
        //    if (AppSettings != null)
        //    {
        //        return AppSettings;
        //    }
        //    HttpClient Http=new HttpClient();
        //    ApiResponseDto apiResponse = await _userProfileApi.GetAkademisyenState();
        //Settings settings = await Http.GetJsonAsyncExtension<Settings>("settings.json");
        //    //        appState.AppSettings = settings;

        //    if (apiResponse.StatusCode == Status200OK)
        //    {
        //        AkademisyenState = JsonConvert.DeserializeObject<AkademisyenDto>(apiResponse.Result.ToString());
        //        return AkademisyenState;
        //    }
        //    return new AkademisyenDto();
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
                //UserProfile.LastPageVisited = uri;
                await UpdateUserProfile();
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
