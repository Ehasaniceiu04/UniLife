using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class FakulteTurDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Tur { get; set; }
        public string TurEn { get; set; }
        public string YokasId { get; set; }
    }
}
