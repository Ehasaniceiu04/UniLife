using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;

namespace UniLife.Shared.Dto.Definitions.Bussines
{
    public class ProgramYetkiListUserIdDto
    {
        public Guid UserId { get; set; }

        public List<UserProgramYetkiDto> UserProgramYetkiList { get; set; }
    }
}
