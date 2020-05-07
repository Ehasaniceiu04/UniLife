using Semerkand.Shared.DataInterfaces;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Ders : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public int MufredatID { get; set; }
        public virtual Mufredat Mufredat { get; set; }
    }
}
