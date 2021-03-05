using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class Ogrenci : Entity<int>, IAuditable, ISoftDelete
    {
        
        public Guid ApplicationUserId{ get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        [MaxLength(300)]
        public string Soyad { get; set; }

        public long OgrNo { get; set; }

        [MaxLength(11)]
        public string TCKN { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }

        public int FakulteId { get; set; }
        public virtual Fakulte Fakulte{ get; set; }
        public int BolumId { get; set; }
        public virtual Bolum Bolum{ get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program{ get; set; }
        public int MufredatId { get; set; }
        public virtual Mufredat Mufredat { get; set; }
        [MaxLength(500)]
        public string Eimg { get; set; }

        public int KayitNedenId { get; set; }
        public virtual KayitNeden KayitNeden { get; set; }

        public int OgrenimDurumId { get; set; }
        public virtual OgrenimDurum OgrenimDurum{ get; set; }
        //public int OgrenimTurId { get; set; }
        //public virtual OgrenimTur OgrenimTur { get; set; }
        public bool Durum { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Danisman")]
        public int? DanismanId { get; set; }
        public virtual Akademisyen Danisman { get; set; }

        public DateTime? MezuniyetTarih { get; set; }
        [MaxLength(11)]
        public string DiplomaNo { get; set; }
        public bool? DanismanOnay { get; set; }

        [MaxLength(500)]
        public string DnmSnfGecBilgi { get; set; }
        [MaxLength(500)]
        public string AskerDurum { get; set; }
        public DateTime KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }
        public int? Sinif { get; set; }
        public decimal GerekenTopUcret { get; set; }
        public decimal OdenenTopUcret { get; set; }
        public decimal GenelBakiye { get; set; }
        public bool IsMale { get; set; }
        [MaxLength(500)]
        public string Adres { get; set; }
        [MaxLength(500)]
        public string BilgNotu { get; set; }
        public bool MultiUni { get; set; }
        [MaxLength(200)]
        public string IlaveDonem { get; set; }
        public int CapYan { get; set; }=1;

        public decimal AgNo1 { get; set; }
        public decimal AgNo2 { get; set; }
        public int TopKredi1 { get; set; }
        public int TopKredi2 { get; set; }
        public int TopAkts1 { get; set; }
        public int TopAkts2 { get; set; }
        public int HasStaj { get; set; }
        public int HasHazirlik { get; set; }
        public int SDersler1 { get; set; }
        public int SDersler2 { get; set; }
        public int ZDersler1 { get; set; }
        public int ZDersler2 { get; set; }
        public int BasarisizDersler { get; set; }
        [MaxLength(11)]
        public string SonDonem { get; set; }

        public virtual ICollection<DersKayit> DersKayits{ get; set; }

        public virtual ICollection<SinavKayit> SinavKayits{ get; set; }

    }
}
