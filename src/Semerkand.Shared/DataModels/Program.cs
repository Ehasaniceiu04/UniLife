using Semerkand.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Program : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public int BolumId { get; set; }
        public Bolum Bolum { get; set; }

        public ICollection<Mufredat> Mufredats { get; set; }
    }
}
