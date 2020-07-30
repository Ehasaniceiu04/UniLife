using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class IlDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
    }
}
