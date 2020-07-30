using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OsymDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }
        public string OsymTip { get; set; }
        public string Yil { get; set; }
        public string Puan { get; set; }
        public string PuanTuru { get; set; }
        public int TercihSirasi { get; set; }
        public string BolumKodu { get; set; }
        public string PuanYuzde { get; set; } //yerleştirmede kullanılan puan yüzdesi
        public int BasariSira { get; set; }
        public double BasariPuan { get; set; }
        public bool OkulBirinci { get; set; }
    }
}
