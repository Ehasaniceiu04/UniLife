using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class Nufus : Entity<int>, IAuditable, ISoftDelete
    {
        
        public Guid ApplicationUserId{ get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Adres { get; set; }
        public int IlId{ get; set; }

        public virtual Il Il { get; set; }

        public string Ilce { get; set; }
        public string Telefon { get; set; }
        public string Banka { get; set; }
        public string Sube { get; set; }
        public string SubeKod { get; set; }
        public string HesapNo { get; set; }
        public string Iban { get; set; }
    }
}
