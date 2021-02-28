using System;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class UserProgramYetkiDto : EntityDto<int>
    {
        public Guid UserId { get; set; }
        public int ProgramId { get; set; }
        public int BolumId { get; set; }
        public int FakulteId { get; set; }

        public string ProgramAd { get; set; }
    }
}
