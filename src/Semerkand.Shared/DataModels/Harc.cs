using Semerkand.Shared.DataInterfaces;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Harc : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }

        //public virtual ICollection<Ders> Derss { get; set; }
    }
}
