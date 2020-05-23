using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Account;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.Server.Managers
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
        
        // Admin policies. 

        Task<ApiResponse> Create(RegisterDto parameters);
        Task<ApiResponse> CreateOgrenci(OgrenciDto ogrenciDto);

        Task<ApiResponse> Delete(string id);

        ApiResponse GetUser(ClaimsPrincipal userClaimsPrincipal);

        Task<ApiResponse> ListRoles();

        Task<ApiResponse> Update(UserInfoDto userInfo);

        Task<ApiResponse> AdminResetUserPasswordAsync(Guid id, string newPassword, ClaimsPrincipal userClaimsPrincipal);
        
        Task<ApplicationUser> RegisterNewUserAsync(string userName, string email, string password, bool requireConfirmEmail);
    }
}
