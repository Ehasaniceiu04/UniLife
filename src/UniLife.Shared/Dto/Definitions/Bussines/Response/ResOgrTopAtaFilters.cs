using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class ResOgrTopAtaFilters
    {
        [Key]
        public int OgrenciId { get; set; }
        public string OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string AdSoyad
        {
            get { return Ad.ToString() + " " + Soyad.ToString(); }
        }
        public int Sinif { get; set; }
        public DateTime KayitTarih { get; set; }
        public string Danisman { get; set; }
        public string DanismanIki { get; set; }
        public string Program { get; set; }
    }
}
