using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class UniversiteDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[MaxLength(300)]
        public string Isim { get; set; }
    }
}