using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class Bolum : IAuditable, ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string Isim { get; set; }


        public int FakulteId { get; set; }
        public Fakulte Fakulte{ get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
