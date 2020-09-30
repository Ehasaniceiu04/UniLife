using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersKancaDto : EntityDto<int>
    {
        public int AktifMufredatDersId { get; set; }
        public int PasifMufredatDersId { get; set; }
        public string PasifMufredatDersKod { get; set; }
        public string PasifMufredatDersAd { get; set; }
        public double PasifMufredatKredi { get; set; }
        public int PasifMufredatAkts { get; set; }

        public string AktifMufredatDersKod { get; set; }
        public string AktifMufredatDersAd { get; set; }
        public double AktifMufredatKredi { get; set; }
        public int AktifMufredatAkts { get; set; }
    }
}
