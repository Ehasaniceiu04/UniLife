using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Account;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface IAccountManager
    {
        Task<ApiResponse> Login(LoginDto parameters);

        Task<ApiResponse> Register(RegisterDto parameters);

        Task<ApiResponse> ConfirmEmail(ConfirmEmailDto parameters);

        Task<ApiResponse> ForgotPassword(ForgotPasswordDto parameters);

        Task<ApiResponse> ResetPassword(ResetPasswordDto parameters);

        Task<ApiResponse> Logout();

        Task<ApiResponse> UserInfo(ClaimsPrincipal userClaimsPrincipal);

        Task<ApiResponse> UpdateUser(UserInfoDto userInfo);


        Task<ApiResponse> UpdateOgrenciUser(OgrenciDto ogrenciDto);
        Task<ApiResponse> UpdateAkademisyenUser(AkademisyenDto AkademisyenDto);
        Task<ApiResponse> UpdatePersonelUser(PersonelDto personelDto);

        // Admin policies. 

        Task<ApiResponse> Create(RegisterDto parameters);
        Task<ApiResponse> CreateOgrenci(OgrenciDto ogrenciDto);
        Task<ApiResponse> CreateAkademisyen(AkademisyenDto akademisyenDto);
        Task<ApiResponse> CreatePersonel(PersonelDto personelDto);

        Task<ApiResponse> Delete(string id);

        ApiResponse GetUser(ClaimsPrincipal userClaimsPrincipal);

        Task<ApiResponse> ListRoles();

        Task<ApiResponse> Update(UserInfoDto userInfo);

        Task<ApiResponse> UpdateRoleFromOgrenciUser(OgrenciDto ogrenciDto);
        Task<ApiResponse> UpdateRoleFromAkademisyenUser(AkademisyenDto akademisyenDto);
        Task<ApiResponse> UpdateRoleFromPersonelUser(PersonelDto personelDto);

        Task<ApiResponse> AdminResetUserPasswordAsync(Guid id, string newPassword, ClaimsPrincipal userClaimsPrincipal);
        
        Task<ApplicationUser> RegisterNewUserAsync(string userName, string email, string password, bool requireConfirmEmail);
        
    }
}
