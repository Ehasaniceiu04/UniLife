using Semerkand.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Universite : IAuditable, ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public ICollection<Fakulte> Fakultes { get; set; }
    }
}
