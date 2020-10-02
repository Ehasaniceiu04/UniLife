using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class Bina : Entity<int>,IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public int? KampusId { get; set; }
        public virtual Kampus Kampus{ get; set; }

        public virtual ICollection<Derslik> Dersliks{ get; set; }
    }
}
