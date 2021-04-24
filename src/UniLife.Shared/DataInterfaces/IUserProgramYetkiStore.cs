using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Definitions.Bussines;

namespace UniLife.Shared.DataInterfaces
{
    public interface IUserProgramYetkiStore : IBaseStore<UserProgramYetki, UserProgramYetkiDto>
    {
        Task<List<UserProgramYetkiDto>> GetUPYByUserId(Guid userId);
        Task UpdateUserProgramYetkis(ProgramYetkiListUserIdDto programYetkiListUserIdDto);
        Task<List<UserProgramYetkiDto>> GetUserProgramYetkiListState(Guid userId);
    }
}
