using UniLife.Shared.DataInterfaces;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class Harc : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }

        public int DonemId { get; set; }
        public virtual Donem Donem { get; set; }

        public int NormalSure { get; set; }
        public int IlkUzatma { get; set; }
        public int TakipYillar { get; set; }


        //public virtual ICollection<Ders> Derss { get; set; }
    }
}
