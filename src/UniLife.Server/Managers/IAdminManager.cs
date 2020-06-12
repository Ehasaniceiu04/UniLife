using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Admin;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UniLife.Server.Managers
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
