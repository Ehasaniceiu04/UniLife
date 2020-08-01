using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciDerslerDto
    {
        public int OgrenciId { get; set; }
        public int DersAcilanId { get; set; }
        public int Sube { get; set; }
        public string DersKod { get; set; }
        public string DersAd { get; set; }
        public string SonucDurum { get; set; }
        public double Ort { get; set; }
        public string Not { get; set; }
        public string Durumu { get; set; }
        public string SinavNotlari { get; set; }
        public int Sinif { get; set; }
        public string Donem { get; set; }
        public bool IsZorunlu { get; set; }
        public double Kredi { get; set; }
        public int Akts { get; set; }

    }
}
