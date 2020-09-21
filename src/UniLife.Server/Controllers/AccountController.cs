using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Extensions;
using Microsoft.Extensions.Logging;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        public IConfiguration Configuration { get; }

        private readonly ApiResponse _invalidUserModel;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountManager accountManager, IConfiguration configuration, ILogger<AccountController> logger)
        {
            Configuration = configuration;
            _accountManager = accountManager;
            _logger = logger;
            _invalidUserModel = new ApiResponse(Status400BadRequest, "User Model is Invalid"); // Could we inject this? As some form of 'Errors which has constant values'?
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType((int)Status204NoContent)]
        [ProducesResponseType((int)Status401Unauthorized)]
        public async Task<ApiResponse> Login(LoginDto parameters)
            => ModelState.IsValid ? await _accountManager.Login(parameters) : _invalidUserModel;

        // POST: api/Account/Register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ApiResponse> Register(RegisterDto parameters)
            => ModelState.IsValid ? await _accountManager.Register(parameters) : _invalidUserModel;

        // POST: api/Account/ConfirmEmail
        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<ApiResponse> ConfirmEmail(ConfirmEmailDto parameters)
            => ModelState.IsValid ? await _accountManager.ConfirmEmail(parameters) : _invalidUserModel;

        // POST: api/Account/ForgotPassword
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ApiResponse> ForgotPassword(ForgotPasswordDto parameters)
            => ModelState.IsValid ? await _accountManager.ForgotPassword(parameters) : _invalidUserModel;

        // PUT: api/Account/ResetPassword
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ApiResponse> ResetPassword(ResetPasswordDto parameters)
        {
            if (ModelState.IsValid)
            {
                string kullaniciId = User.GetSubjectId();
                if (User.IsInRole("Administrator"))
                {
                    return await _accountManager.ResetPassword(parameters);
                }
                else if(kullaniciId == parameters.UserId)
                {
                    _logger.LogError($"{kullaniciId} Kullanıcısı, {parameters.UserId} id li kullanıcının şifresini değiştirdi. Şifre!!!.");
                    return await _accountManager.ResetPassword(parameters);
                }
                else
                {
                    _logger.LogError($"{kullaniciId} Kullanıcısı, {parameters.UserId} id li kullanıcının şifresini değiştirmey çalıştı. Dikkat!!!.");
                    return new ApiResponse(Status401Unauthorized, "Başka kullanıcıların şifresini değiştirme yetkiniz yok. Bu işlem adınıza loglanmıştır!");
                }
                
            }
            else
            {
                return _invalidUserModel;
            }
        }

        // POST: api/Account/Logout
        [HttpPost("Logout")]
        [Authorize]
        public async Task<ApiResponse> Logout()
            => await _accountManager.Logout();

        [HttpGet("UserInfo")]
        [ProducesResponseType((int)Status200OK)]
        [ProducesResponseType((int)Status401Unauthorized)]
        public async Task<ApiResponse> UserInfo()
        =>  await _accountManager.UserInfo(User);

        // DELETE: api/Account/5
        [HttpPost("UpdateUser")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> UpdateUser(UserInfoDto userInfo)
        => ModelState.IsValid ? await _accountManager.UpdateUser(userInfo) : _invalidUserModel;

        [HttpPost("UpdateOgrenciUser")]
        [Authorize(Roles = "Administrator,Personel,Ogrenci")]
        public async Task<ApiResponse> UpdateOgrenciUser(OgrenciDto ogrenciDto)
        => ModelState.IsValid ? await _accountManager.UpdateOgrenciUser(ogrenciDto) : _invalidUserModel;

        [HttpPost("UpdateAkademisyenUser")]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public async Task<ApiResponse> UpdateAkademisyenUser(AkademisyenDto akademisyenDto)
        => ModelState.IsValid ? await _accountManager.UpdateAkademisyenUser(akademisyenDto) : _invalidUserModel;

        [HttpPost("UpdatePersonelUser")]
        [Authorize(Roles = "Administrator,Personel")]
        public async Task<ApiResponse> UpdatePersonelUser(PersonelDto personelDto)
        => ModelState.IsValid ? await _accountManager.UpdatePersonelUser(personelDto) : _invalidUserModel;



        ///----------Admin User Management Interface Methods
        // POST: api/Account/Create
        [HttpPost("Create")]
        [Authorize(Permissions.User.Create)]
        public async Task<ApiResponse> Create(RegisterDto parameters)
        =>  ModelState.IsValid ? await _accountManager.Create(parameters) : _invalidUserModel;

        ///----------Admin OgrenciUser Management Interface Methods
        // POST: api/Account/Create
        [HttpPost("CreateOgrenci")]
        [Authorize(Permissions.User.Create)]
        public async Task<ApiResponse> CreateOgrenci(OgrenciDto ogrenciDto)
        => ModelState.IsValid ? await _accountManager.CreateOgrenci(ogrenciDto, Configuration["UniLife:OgrNoDesen"]) : _invalidUserModel;

        ///----------Admin AkademisyenUser Management Interface Methods
        // POST: api/Account/Create
        [HttpPost("CreateAkademisyen")]
        [Authorize(Permissions.User.Create)]
        public async Task<ApiResponse> CreateAkademisyen(AkademisyenDto akademisyenDto)
        => ModelState.IsValid ? await _accountManager.CreateAkademisyen(akademisyenDto) : _invalidUserModel;

        ///----------Admin PersonelUser Management Interface Methods
        // POST: api/Account/Create
        [HttpPost("CreatePersonel")]
        [Authorize(Permissions.User.Create)]
        public async Task<ApiResponse> CreatePersonel(PersonelDto personelDto)
        => ModelState.IsValid ? await _accountManager.CreatePersonel(personelDto) : _invalidUserModel;

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.User.Delete)]
        public async Task<ApiResponse> Delete(string id)
        => await _accountManager.Delete(id);

        [HttpGet("GetUser")]
        public ApiResponse GetUser()
        => _accountManager.GetUser(User);

        [HttpGet("ListRoles")]
        [Authorize(Permissions.Role.Read)]
        public async Task<ApiResponse> ListRoles()
        =>  await _accountManager.ListRoles();

        [HttpPut]
        [Authorize(Permissions.User.Update)]
        // PUT: api/Account/5
        public async Task<ApiResponse> Update([FromBody] UserInfoDto userInfo)
        =>  ModelState.IsValid ? await _accountManager.Update(userInfo) : _invalidUserModel;

        [HttpPut]
        [Authorize(Permissions.User.Update)]
        [Route("UpdateRoleFromUser")]
        // PUT: api/Account/5
        public async Task<ApiResponse> UpdateRoleFromUser([FromBody] OgrenciDto ogrenciDto)
        => ModelState.IsValid ? await _accountManager.UpdateRoleFromOgrenciUser(ogrenciDto) : _invalidUserModel;

        [HttpPut]
        [Authorize(Permissions.User.Update)]
        [Route("UpdateRoleFromAkademisyenUser")]
        // PUT: api/Account/5
        public async Task<ApiResponse> UpdateRoleFromAkademisyenUser([FromBody] AkademisyenDto akademisyenDto)
        => ModelState.IsValid ? await _accountManager.UpdateRoleFromAkademisyenUser(akademisyenDto) : _invalidUserModel;

        [HttpPut]
        [Authorize(Permissions.User.Update)]
        [Route("UpdateRoleFromPersonelUser")]
        // PUT: api/Account/5
        public async Task<ApiResponse> UpdateRoleFromPersonelUser([FromBody] PersonelDto personelDto)
        => ModelState.IsValid ? await _accountManager.UpdateRoleFromPersonelUser(personelDto) : _invalidUserModel;


        [HttpPost("AdminUserPasswordReset/{id}")]
        [Authorize(Permissions.User.Update)]
        [ProducesResponseType((int)Status204NoContent)]
        public async Task<ApiResponse> AdminResetUserPasswordAsync(Guid id, [FromBody] string newPassword)
        => ModelState.IsValid
                ? await _accountManager.AdminResetUserPasswordAsync(id, newPassword, User)
                : _invalidUserModel;
    }
}
