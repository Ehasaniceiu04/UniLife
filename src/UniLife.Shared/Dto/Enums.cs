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
    public enum MezunOnayDurum
    {
        Bekleniyor = 0,
        Aktif = 1,
        DanismanOnayinda = 2,
        DanışmanOnayladı = 3,
        MezunEdildi = 4,
        DanismanRet = 5
    }
    public enum DersNedenEnum
    {
        Dönemsel = 1,
        Uzaktan_Eğitim = 2,
        Tez = 3,
        Staj = 4
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
        But = 3,
        Mazeret = 4,
    }
    public enum SinavKatilimEnum
    {
        Katıldı = 1,
        Katılmadı = 2,
        Devamsız = 3
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
    public enum OgrenimTurEnum
    {
        Doktora = 1,
        Lisans = 2,
        Ön_Lisans = 3,
        Tezsiz_Yüksek_Lisans = 4,
        Yüksek_Lisans = 5
    }
    public enum DonemTipEnum
    {
        Güz_Dönemi = 1,
        Bahar_Dönemi = 2,
        Yaz_Dönemi = 3
    }



    public enum UserRoles
    {
        Administrator,
        Personel,
        Akademisyen,
        Ogrenci,
        Anonim
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
