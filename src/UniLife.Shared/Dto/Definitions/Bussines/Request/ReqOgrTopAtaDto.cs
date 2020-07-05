using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class ReqOgrTopAtaDto
    {
        public int? FakulteId { get; set; }
        public int? BolumId { get; set; }
        public int? ProgramId { get; set; }
        public int? KayitNedenId { get; set; }
        public int? OgrenimDurumId { get; set; }
        public int? Sinif { get; set; }
        public int? Sube { get; set; }
        public int? Cinsiyet { get; set; }
        public DateTime? KayitAraBas { get; set; }
        public DateTime? KayitAraSon { get; set; }


    }
}
