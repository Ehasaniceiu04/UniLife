using System.Threading.Tasks;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Account;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.CommonUI.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task<ApiResponseDto> Login(LoginDto loginParameters);
        Task<ApiResponseDto> Create(RegisterDto registerParameters);
        Task<ApiResponseDto> Create(OgrenciDto ogrenciDto);
        Task<ApiResponseDto> Register(RegisterDto registerParameters);
        Task<ApiResponseDto> ForgotPassword(ForgotPasswordDto forgotPasswordParameters);
        Task<ApiResponseDto> ResetPassword(ResetPasswordDto resetPasswordParameters);
        Task<ApiResponseDto> Logout();
        Task<ApiResponseDto> ConfirmEmail(ConfirmEmailDto confirmEmailParameters);
        Task<UserInfoDto> GetUserInfo();
        Task<ApiResponseDto> UpdateUser(UserInfoDto userInfo);
        Task<ApiResponseDto> UpdateOgrenciUser(OgrenciDto ogrenciDto);
        
        Task<UserInfoDto> GetUser();
    }
}
