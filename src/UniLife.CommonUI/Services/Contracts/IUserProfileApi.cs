using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Account;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Services.Contracts
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
        
        Task<ApiResponseDto> GetAkademisyenState();
        
    }
}
