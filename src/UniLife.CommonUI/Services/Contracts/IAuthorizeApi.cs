using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Account;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task<ApiResponseDto> Login(LoginDto loginParameters);
        Task<ApiResponseDto> Create(RegisterDto registerParameters);
        Task<ApiResponseDto> Create(OgrenciDto ogrenciDto);
        Task<ApiResponseDto> Create(AkademisyenDto akademisyenDto);
        Task<ApiResponseDto> Create(PersonelDto personelDto);
        Task<ApiResponseDto> Register(RegisterDto registerParameters);
        Task<ApiResponseDto> ForgotPassword(ForgotPasswordDto forgotPasswordParameters);
        Task<ApiResponseDto> ResetPassword(ResetPasswordDto resetPasswordParameters);
        Task<ApiResponseDto> Logout();
        Task<ApiResponseDto> ConfirmEmail(ConfirmEmailDto confirmEmailParameters);
        Task<UserInfoDto> GetUserInfo();
        Task<ApiResponseDto> UpdateUser(UserInfoDto userInfo);
        Task<ApiResponseDto> UpdateOgrenciUser(OgrenciDto ogrenciDto);
        Task<ApiResponseDto> UpdateAkademisyenUser(AkademisyenDto akademisyenDto);
        Task<ApiResponseDto> UpdatePersonelUser(PersonelDto personelDto);

        Task<UserInfoDto> GetUser();
    }
}
