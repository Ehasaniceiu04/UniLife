using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Admin;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Semerkand.Server.Managers
{
    public interface IAdminManager
    {
        Task<ApiResponse> GetUsers(int pageSize = 10, int pageNumber = 0);

        ApiResponse GetPermissions();

        Task<ApiResponse> GetRoles(int pageSize = 10,int pageNumber = 0);

        Task<ApiResponse> GetRoleAsync(string roleName);

        Task<ApiResponse> CreateRoleAsync(RoleDto newRole);

        Task<ApiResponse> UpdateRoleAsync([FromBody] RoleDto newRole);

        Task<ApiResponse> DeleteRoleAsync(string name);
        Task<ApiResponse> GetOgrenciUsers(int pageSize, int pageNumber);
        Task<ApiResponse> GetRolesByUserId(Guid userId);
        
    }
}
