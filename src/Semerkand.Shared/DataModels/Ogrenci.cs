using Semerkand.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Ogrenci : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }


        public int UniversiteId { get; set; }

        // [ForeignKey("UniversiteId")]
        public virtual Universite Universite { get; set; }


        public virtual ICollection<Bolum> Bolums { get; set; }
    }
}
