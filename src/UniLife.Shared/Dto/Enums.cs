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
        Yeni_Sinav = 0,
        Mazeret_Sinav = 1
    }
    public enum SinavTipEnum
    {
        Ara_Sinav = 0,
        Final = 1
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
