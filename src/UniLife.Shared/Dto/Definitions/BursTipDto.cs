using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class BursTipDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
        public double TutarOran { get; set; }
        public int Tutar1Oran2 { get; set; }
        public int YoksisKod { get; set; }
        public bool Engelli { get; set; }
        public bool Sehit { get; set; }
    }
}
