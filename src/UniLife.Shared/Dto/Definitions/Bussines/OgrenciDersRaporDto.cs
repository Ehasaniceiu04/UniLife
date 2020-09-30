using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciDersRaporDto
    {
        [Key]
        public int Id { get; set; }
        public long OgrNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public double TopKredi { get; set; }
        public double TopAkts { get; set; }
        public int ProgramId { get; set; }
        public int BolumId { get; set; }
        public int FakulteId { get; set; }
        public int? Sinif { get; set; }
    }
}
