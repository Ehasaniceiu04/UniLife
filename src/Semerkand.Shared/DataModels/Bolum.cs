using Semerkand.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Bolum : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(400)]
        public string Isim { get; set; }


        public int FakulteId { get; set; }
        public Fakulte Fakulte { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
