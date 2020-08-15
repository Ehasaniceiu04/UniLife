using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class CezaTip : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public string Kod { get; set; }
        public string YoksisTip { get; set; }
        public bool SistemeGiremez { get; set; }
        public bool DersKayitIzin { get; set; }
    }
}
