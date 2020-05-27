﻿using Semerkand.Server.Managers;
using Semerkand.Shared.Dto.Admin;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Semerkand.Server.Controllers
{
    /// <summary>
    /// This controller is the entry API for the platform administration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet("Users")]
        [Authorize]
        public async Task<ApiResponse> GetUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
            => await _adminManager.GetUsers(pageSize, pageNumber);
        
        [HttpGet("GetOgrenciUsers")]
        [Authorize]
        public async Task<ApiResponse> GetOgrenciUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
            => await _adminManager.GetOgrenciUsers(pageSize, pageNumber);

        [HttpGet("Permissions")]
        [Authorize]
        public ApiResponse GetPermissions()
            => _adminManager.GetPermissions();


        [HttpGet("Roles")]
        [Authorize(Permissions.Role.Read)]
        public async Task<ApiResponse> GetRoles([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
            => await _adminManager.GetRoles(pageSize, pageNumber);

        [HttpGet("Role/{roleName}")]
        [Authorize]
        public async Task<ApiResponse> GetRoleAsync(string roleName)
            => await _adminManager.GetRoleAsync(roleName);

        [HttpPost("Role")]
        [Authorize(Permissions.Role.Create)]
        public async Task<ApiResponse> CreateRoleAsync([FromBody] RoleDto newRole)
            => await _adminManager.CreateRoleAsync(newRole);

        [HttpPut("Role")]
        [Authorize(Permissions.Role.Update)]
        public async Task<ApiResponse> UpdateRoleAsync([FromBody] RoleDto newRole)
            => await _adminManager.UpdateRoleAsync(newRole);

        // DELETE: api/Admin/Role/5
        [HttpDelete("Role/{name}")]
        [Authorize(Permissions.Role.Delete)]
        public async Task<ApiResponse> DeleteRoleAsync(string name)
            => await _adminManager.DeleteRoleAsync(name);

        //Benim show buradan aşşa

        [HttpGet]
        [Route("GetRolesByUserId/{userId:guid}")]
        [Authorize(Permissions.Role.Read)]
        public async Task<ApiResponse> GetRolesByUserId(Guid userId)
            => await _adminManager.GetRolesByUserId(userId);
        
    }
}
