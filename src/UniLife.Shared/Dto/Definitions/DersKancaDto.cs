using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersKancaDto : EntityDto<int>
    {
        public int AktifMufredatDersId { get; set; }
        public int PasifMufredatDersId { get; set; }
    }
}
