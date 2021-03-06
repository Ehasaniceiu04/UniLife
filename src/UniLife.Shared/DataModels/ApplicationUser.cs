using UniLife.Server.Models;
using UniLife.Shared.DataInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class ApplicationUser : IdentityUser<Guid>//, ITenant
    {
        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [MaxLength(64)]
        public string FullName { get; set; }

        [MaxLength(11)]
        public string TCKN { get; set; }

        public int UserType { get; set; } // 1:ogrenci 2:Akademisyen 3:İdari personel

        public ICollection<ApiLogItem> ApiLogItems { get; set; }

        public UserProfile Profile { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual ICollection<Ogrenci> Ogrencis { get; set; }


        ////---zero to one ilişki---// ApplicationUsera bağlı birtane öğrenci olabilir, olmayabilirde. dikkat fluentide var.
        //public int? OgrenciId { get; set; }
        //public virtual Ogrenci Ogrenci { get; set; }
        ////---zero to one ilişki---//

        ////---Zero to many ilişki---//ApplicationUser a bağlı birden fazla öğrenci olabilir, hiç olmayabilirde/ bunu yapınca öğrenciye bir tane nullable id atıyor.
        //public virtual ICollection<Ogrenci> Ogrencis { get; set; }
        ////---Zero to many ilişki---//
    }
}
