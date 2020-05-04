using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class Fakulte : IAuditable, ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }


        public int UniversiteId { get; set; }
        public Universite Universite { get; set; }


        public ICollection<Bolum> Bolums{ get; set; }
    }
}
