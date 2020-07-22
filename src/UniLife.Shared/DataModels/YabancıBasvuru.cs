using System;
using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class YabanciBasvuru : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string BabaAd { get; set; }
        public string AnaAd { get; set; }
        public string DogumYer { get; set; }
        public DateTime DogumTarih{ get; set; }
        public string Email { get; set; }
        public string Cinsiyet { get; set; }
        public string PassportNo { get; set; }
        public string YasaUlke { get; set; }
        public string IletisimBilgisi { get; set; }
        public string Adres { get; set; }
        public string LiseMezuniyetAlan { get; set; }
        public string LiseDurum { get; set; }
        public decimal LiseMezunNot { get; set; }
        public string Tercih1 { get; set; }
        public string Tercih2 { get; set; }
        public string Tercih3 { get; set; }
        public string Tercih4 { get; set; }
        public string Tercih5 { get; set; }
        public bool Accept { get; set; }
    }
}
