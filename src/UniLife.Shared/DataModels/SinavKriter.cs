using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class SinavKriter : Entity<int>, IAuditable, ISoftDelete
    {
        public int? ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public int SinavDegerlendirmeTipi { get; set; } //ENUM
        public int EnAzYYSEtkiOranYuzde { get; set; }
        public int BglDegKatmaLimiti { get; set; }
        public int YariYilSonSinavLimit { get; set; }
        public int BglDegDisSinOrtTavan { get; set; }
        public int BglDegEnAzOgrSayisi { get; set; }
        public int IkiAralikEnAzOgrSayisi { get; set; }
        public int VarSayilanBagHarfTablosu { get; set; } //ENUM
        public int MutlakDegerHarfTablo { get; set; } //ENUM
        public string BasariDegTabanGeçHarf { get; set; } // A B... E
        public bool ButGirmeyenSifir { get; set; }
        public int AraSinavSayi { get; set; }
        public int EnAzAraSnvEtkiOrnYuzde { get; set; }
        public int EnCokYYSEtkiOranYuzde { get; set; }
        public int HamBasariNotAltLimit { get; set; }
        public int EnFazlaHamBasariNotuOrt { get; set; }
        public int VarSayilanBagilHesapTipi { get; set; } //ENUM
        public int VarsayilanMutlakHesapTipi { get; set; } // ENUM
        public int YYSSinavSayisi { get; set; }
        public bool FinaleGirmeyenKalir { get; set; }
        public bool ButuFinalleAyniDegerlendir { get; set; }
    }
}
