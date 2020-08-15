using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class CezaTipDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
        public string YoksisTip { get; set; }
        public bool SistemeGiremez { get; set; }
        public bool DersKayitIzin { get; set; }
    }
}
