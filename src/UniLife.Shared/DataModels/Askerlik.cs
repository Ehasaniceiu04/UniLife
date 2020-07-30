using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class Askerlik : Entity<int>, IAuditable, ISoftDelete
    {
        
        public Guid ApplicationUserId{ get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int Durum { get; set; }
        public DateTime? Tecil { get; set; }
        public DateTime? Terhis { get; set; }
        public DateTime? Alinis { get; set; }
        public DateTime? Islem { get; set; }

    }
}
