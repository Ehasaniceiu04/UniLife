using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class Program : IAuditable, ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public int BolumId { get; set; }
        public Bolum Bolum{ get; set; }

        public ICollection<Mufredat> Mufredats{ get; set; }
    }
}
