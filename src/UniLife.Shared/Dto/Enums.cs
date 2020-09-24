using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto
{
    public enum UserType
    {
        Ogrenci = 1,
        Akademisyen = 2,
        IdariPersonel = 3,
    }
    public enum DersSonucDurum
    {
        Bekleniyor = 0,
        Sonuçlandırıldı = 1
    }
    public enum DersSonuc
    {
        Kaldı = 0,
        Geçti = 1
    }
    public enum SinavTurEnum
    {
        Yeni_Sinav = 1,
        Mazeret_Sinav = 2
    }
    public enum SinavTipEnum
    {
        Ara_Sinav = 1,
        Final = 2,
        But=3
    }
    public enum SinavKatilimEnum
    {
        Katıldı=1,
        Katılmadı=2,
        Devamsız=3
    }
    public enum SinavDegerlendirmeTip
    {
        Bagil_Değerler = 1,
        Temp = 2
    }
    public enum VarSayilanBagHarfTablo
    {
        Tablo_1 = 1,
        Tablo_2 = 2
    }
    public enum MutlakDegerHarfTablo
    {
        Mutlak = 1,
        Bağıl = 2
    }
    public enum VarSayilanBagilHesapTip
    {
        Mutlak = 1,
        Bağıl = 2
    }
    public enum VarsayilanMutlakHesapTip
    {
        Mutlak = 1,
        Bağıl = 2
    }



    public enum UserRoles
    {
        Administrator,
        Personel,
        Akademisyen,
        Ogrenci
    }



    //public enum DersSonucDurum
    //{
    //    [EnumMember(Value = "none")]
    //    None,
    //    [EnumMember(Value = "new")]
    //    New,
    //    [EnumMember(Value = "updated")]
    //    Updated,
    //    [EnumMember(Value = "preview")]
    //    Preview
    //}
}
