using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class AskerlikDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }
        public int? Durum { get; set; }
        public DateTime? Tecil { get; set; }
        public DateTime? Terhis { get; set; }
        public DateTime? Alinis { get; set; }
        public DateTime? Islem { get; set; }
    }
}
