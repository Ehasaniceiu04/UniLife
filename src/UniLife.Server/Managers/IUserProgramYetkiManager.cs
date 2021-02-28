using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Definitions.Bussines;

namespace UniLife.Server.Managers
{
    public interface IUserProgramYetkiManager : IBaseManager<UserProgramYetki, UserProgramYetkiDto>
    {
        Task<ApiResponse> GetUPYByUserId(Guid userId);
        Task<ApiResponse> UpdateUserProgramYetkis(ProgramYetkiListUserIdDto programYetkiListUserIdDto);
    }
}
