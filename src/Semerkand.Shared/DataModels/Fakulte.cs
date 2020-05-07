using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class Fakulte : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }


        public int UniversiteId { get; set; }
        
       // [ForeignKey("UniversiteId")]
        public virtual Universite Universite { get; set; }


        public virtual ICollection<Bolum> Bolums{ get; set; }
    }
}
