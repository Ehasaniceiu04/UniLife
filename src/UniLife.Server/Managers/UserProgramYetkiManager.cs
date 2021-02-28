using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Definitions.Bussines;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class UserProgramYetkiManager : BaseManager<UserProgramYetki, UserProgramYetkiDto>, IUserProgramYetkiManager
    {
        private readonly IUserProgramYetkiStore _userProgramYetkiStore;

        public UserProgramYetkiManager(IUserProgramYetkiStore userProgramYetkiStore) : base(userProgramYetkiStore)
        {
            _userProgramYetkiStore = userProgramYetkiStore;
        }

        public async Task<ApiResponse> GetUPYByUserId(Guid userId)
        {
            return new ApiResponse(Status200OK, "Retrieved UserProgramYetkis", await _userProgramYetkiStore.GetUPYByUserId(userId));
        }

        public async Task<ApiResponse> UpdateUserProgramYetkis(ProgramYetkiListUserIdDto programYetkiListUserIdDto)
        {
            await _userProgramYetkiStore.UpdateUserProgramYetkis(programYetkiListUserIdDto);
            return new ApiResponse(Status200OK, "Program yetkileri getirildi.");
        }
        
    }
}
