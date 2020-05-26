using System.Threading.Tasks;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Account;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.CommonUI.Services.Contracts
{
    /// <summary>
    /// Access to User Profile information
    /// </summary>
    public interface IUserProfileApi
    {
        Task<ApiResponseDto> Upsert(UserProfileDto userProfile);
        //Task<ApiResponseDto> UpsertOgrenci(OgrenciDto ogrenciDto);
        Task<ApiResponseDto> Get();
        //Task<ApiResponseDto> GetOgrenci();
    }
}
