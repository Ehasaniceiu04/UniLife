using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DersDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }

        public int MufredatID { get; set; }
        public MufredatDto Mufredat { get; set; }
    }
}
