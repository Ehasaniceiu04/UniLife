﻿using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;

namespace Semerkand.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        private readonly ApiResponse _invalidUserModel;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
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
        =>  ModelState.IsValid ? await _accountManager.ResetPassword(parameters) : _invalidUserModel;

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
        [Authorize]
        public async Task<ApiResponse> UpdateUser(UserInfoDto userInfo)
        => ModelState.IsValid ? await _accountManager.UpdateUser(userInfo) : _invalidUserModel;

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
        => ModelState.IsValid ? await _accountManager.CreateOgrenci(ogrenciDto) : _invalidUserModel;

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
        => ModelState.IsValid ? await _accountManager.UpdateRoleFromUser(ogrenciDto) : _invalidUserModel;


        [HttpPost("AdminUserPasswordReset/{id}")]
        [Authorize(Permissions.User.Update)]
        [ProducesResponseType((int)Status204NoContent)]
        public async Task<ApiResponse> AdminResetUserPasswordAsync(Guid id, [FromBody] string newPassword)
        => ModelState.IsValid
                ? await _accountManager.AdminResetUserPasswordAsync(id, newPassword, User)
                : _invalidUserModel;
    }
}
