using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class Mufredat : IAuditable, ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public int ProgramId { get; set; }
        public Program Program{ get; set; }

        public ICollection<Ders> Derss{ get; set; }
    }
}
