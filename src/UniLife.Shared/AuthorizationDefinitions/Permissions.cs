using System.ComponentModel;

namespace UniLife.Shared.AuthorizationDefinitions
{
    public static class Actions
    {
        public const string Create = nameof(Create);
        public const string Read = nameof(Read);
        public const string Update = nameof(Update);
        public const string Confirm = nameof(Confirm);
        public const string Delete = nameof(Delete);
    }

    public static class Permissions
    {
        public static class Sinav
        {
            [Description("Create a new Sinav")]
            public const string Create = nameof(Sinav) + "." + nameof(Actions.Create);
            [Description("Read Sinavs")]
            public const string Read = nameof(Sinav) + "." + nameof(Actions.Read);
            [Description("Edit existing Sinavs")]
            public const string Update = nameof(Sinav) + "." + nameof(Actions.Update);
            [Description("Delete any Sinav")]
            public const string Delete = nameof(Sinav) + "." + nameof(Actions.Delete);
        }
        public static class YabanciBasvuru
        {
            [Description("Create a new YabanciBasvuru")]
            public const string Create = nameof(YabanciBasvuru) + "." + nameof(Actions.Create);
            [Description("Read YabanciBasvurus")]
            public const string Read = nameof(YabanciBasvuru) + "." + nameof(Actions.Read);
            [Description("Edit existing YabanciBasvurus")]
            public const string Update = nameof(YabanciBasvuru) + "." + nameof(Actions.Update);
            [Description("Delete any YabanciBasvuru")]
            public const string Delete = nameof(YabanciBasvuru) + "." + nameof(Actions.Delete);
        }
        public static class DerslikRezerv
        {
            [Description("Create a new DerslikRezerv")]
            public const string Create = nameof(DerslikRezerv) + "." + nameof(Actions.Create);
            [Description("Read DerslikRezervs")]
            public const string Read = nameof(DerslikRezerv) + "." + nameof(Actions.Read);
            [Description("Edit existing DerslikRezervs")]
            public const string Update = nameof(DerslikRezerv) + "." + nameof(Actions.Update);
            [Description("Delete any DerslikRezerv")]
            public const string Delete = nameof(DerslikRezerv) + "." + nameof(Actions.Delete);
        }
        public static class Derslik
        {
            [Description("Create a new Derslik")]
            public const string Create = nameof(Derslik) + "." + nameof(Actions.Create);
            [Description("Read Dersliks")]
            public const string Read = nameof(Derslik) + "." + nameof(Actions.Read);
            [Description("Edit existing Dersliks")]
            public const string Update = nameof(Derslik) + "." + nameof(Actions.Update);
            [Description("Delete any Derslik")]
            public const string Delete = nameof(Derslik) + "." + nameof(Actions.Delete);
        }
        public static class SinavKayit
        {
            [Description("Create a new SinavKayit")]
            public const string Create = nameof(SinavKayit) + "." + nameof(Actions.Create);
            [Description("Read SinavKayits")]
            public const string Read = nameof(SinavKayit) + "." + nameof(Actions.Read);
            [Description("Edit existing SinavKayits")]
            public const string Update = nameof(SinavKayit) + "." + nameof(Actions.Update);
            [Description("Delete any SinavKayit")]
            public const string Delete = nameof(SinavKayit) + "." + nameof(Actions.Delete);
        }
        public static class Personel
        {
            [Description("Create a new Personel")]
            public const string Create = nameof(Personel) + "." + nameof(Actions.Create);
            [Description("Read Personels")]
            public const string Read = nameof(Personel) + "." + nameof(Actions.Read);
            [Description("Edit existing Personels")]
            public const string Update = nameof(Personel) + "." + nameof(Actions.Update);
            [Description("Delete any Personel")]
            public const string Delete = nameof(Personel) + "." + nameof(Actions.Delete);
        }
        public static class Akademisyen
        {
            [Description("Create a new Akademisyen")]
            public const string Create = nameof(Akademisyen) + "." + nameof(Actions.Create);
            [Description("Read Akademisyens")]
            public const string Read = nameof(Akademisyen) + "." + nameof(Actions.Read);
            [Description("Edit existing Akademisyens")]
            public const string Update = nameof(Akademisyen) + "." + nameof(Actions.Update);
            [Description("Delete any Akademisyen")]
            public const string Delete = nameof(Akademisyen) + "." + nameof(Actions.Delete);
        }
        public static class DersKayit
        {
            [Description("Create a new DersKayit")]
            public const string Create = nameof(DersKayit) + "." + nameof(Actions.Create);
            [Description("Read DersKayits")]
            public const string Read = nameof(DersKayit) + "." + nameof(Actions.Read);
            [Description("Edit existing DersKayits")]
            public const string Update = nameof(DersKayit) + "." + nameof(Actions.Update);

            [Description("Ders kayit Onay")]
            public const string Confirm = nameof(DersKayit) + "." + nameof(Actions.Confirm);

            [Description("Delete any DersKayit")]
            public const string Delete = nameof(DersKayit) + "." + nameof(Actions.Delete);
        }
        public static class Ogrenci
        {
            [Description("Create a new Ogrenci")]
            public const string Create = nameof(Ogrenci) + "." + nameof(Actions.Create);
            [Description("Read Ogrencis")]
            public const string Read = nameof(Ogrenci) + "." + nameof(Actions.Read);
            [Description("Edit existing Ogrencis")]
            public const string Update = nameof(Ogrenci) + "." + nameof(Actions.Update);
            [Description("Delete any Ogrenci")]
            public const string Delete = nameof(Ogrenci) + "." + nameof(Actions.Delete);
        }
        public static class DersAcilan
        {
            [Description("Create a new DersAcilan")]
            public const string Create = nameof(DersAcilan) + "." + nameof(Actions.Create);
            [Description("Read DersAcilans")]
            public const string Read = nameof(DersAcilan) + "." + nameof(Actions.Read);
            [Description("Edit existing DersAcilans")]
            public const string Update = nameof(DersAcilan) + "." + nameof(Actions.Update);
            [Description("Delete any DersAcilan")]
            public const string Delete = nameof(DersAcilan) + "." + nameof(Actions.Delete);
        }
        public static class Donem
        {
            [Description("Create a new Donem")]
            public const string Create = nameof(Donem) + "." + nameof(Actions.Create);
            [Description("Read Donems")]
            public const string Read = nameof(Donem) + "." + nameof(Actions.Read);
            [Description("Edit existing Donems")]
            public const string Update = nameof(Donem) + "." + nameof(Actions.Update);
            [Description("Delete any Donem")]
            public const string Delete = nameof(Donem) + "." + nameof(Actions.Delete);
        }
        public static class Ders
        {
            [Description("Create a new Ders")]
            public const string Create = nameof(Ders) + "." + nameof(Actions.Create);
            [Description("Read Derss")]
            public const string Read = nameof(Ders) + "." + nameof(Actions.Read);
            [Description("Edit existing Derss")]
            public const string Update = nameof(Ders) + "." + nameof(Actions.Update);
            [Description("Delete any Ders")]
            public const string Delete = nameof(Ders) + "." + nameof(Actions.Delete);
        }
        public static class Mufredat
        {
            [Description("Create a new Mufredat")]
            public const string Create = nameof(Mufredat) + "." + nameof(Actions.Create);
            [Description("Read Mufredats")]
            public const string Read = nameof(Mufredat) + "." + nameof(Actions.Read);
            [Description("Edit existing Mufredats")]
            public const string Update = nameof(Mufredat) + "." + nameof(Actions.Update);
            [Description("Delete any Mufredat")]
            public const string Delete = nameof(Mufredat) + "." + nameof(Actions.Delete);
        }
        public static class Harc
        {
            [Description("Create a new Harc")]
            public const string Create = nameof(Harc) + "." + nameof(Actions.Create);
            [Description("Read Harcs")]
            public const string Read = nameof(Harc) + "." + nameof(Actions.Read);
            [Description("Edit existing Harcs")]
            public const string Update = nameof(Harc) + "." + nameof(Actions.Update);
            [Description("Delete any Harc")]
            public const string Delete = nameof(Harc) + "." + nameof(Actions.Delete);
        }
        public static class Program
        {
            [Description("Create a new Program")]
            public const string Create = nameof(Program) + "." + nameof(Actions.Create);
            [Description("Read Programs")]
            public const string Read = nameof(Program) + "." + nameof(Actions.Read);
            [Description("Edit existing Programs")]
            public const string Update = nameof(Program) + "." + nameof(Actions.Update);
            [Description("Delete any Program")]
            public const string Delete = nameof(Program) + "." + nameof(Actions.Delete);
        }
        public static class Bolum
        {
            [Description("Create a new Bolum")]
            public const string Create = nameof(Bolum) + "." + nameof(Actions.Create);
            [Description("Read Bolums")]
            public const string Read = nameof(Bolum) + "." + nameof(Actions.Read);
            [Description("Edit existing Bolums")]
            public const string Update = nameof(Bolum) + "." + nameof(Actions.Update);
            [Description("Delete any Bolum")]
            public const string Delete = nameof(Bolum) + "." + nameof(Actions.Delete);
        }
        public static class Fakulte
        {
            [Description("Create a new Fakulte")]
            public const string Create = nameof(Fakulte) + "." + nameof(Actions.Create);
            [Description("Read Fakultes")]
            public const string Read = nameof(Fakulte) + "." + nameof(Actions.Read);
            [Description("Edit existing Fakultes")]
            public const string Update = nameof(Fakulte) + "." + nameof(Actions.Update);
            [Description("Delete any Fakulte")]
            public const string Delete = nameof(Fakulte) + "." + nameof(Actions.Delete);
        }
        public static class Universite
        {
            [Description("Create a new Universite")]
            public const string Create = nameof(Universite) + "." + nameof(Actions.Create);
            [Description("Read Universites")]
            public const string Read = nameof(Universite) + "." + nameof(Actions.Read);
            [Description("Edit existing Universites")]
            public const string Update = nameof(Universite) + "." + nameof(Actions.Update);
            [Description("Delete any Universite")]
            public const string Delete = nameof(Universite) + "." + nameof(Actions.Delete);
        }

        public static class Menu
        {
            [Description("Üniversite Tanımlamaları")]
            public const string UnivTanimlar = nameof(Menu) + ".ÜniversiteTanimlamaları";
            [Description("Akademik Tanimlamalar")]
            public const string AkadeTanim = nameof(Menu) + ".AkademikTanimlamalar";
            [Description("Yöksis Tanimlamalar")]
            public const string YoksisTanim = nameof(Menu) + ".YöksisTanimlamalar";
            [Description("Kampüs ve Birim Tanimlamalar")]
            public const string KampusBirimTanim = nameof(Menu) + ".KampüsveBirimTanimlamalar";
            [Description("Diğer Tanimlamalar")]
            public const string DigerTanim = nameof(Menu) + ".DiğerTanimlamalar";

            [Description("Ders ve Müfredat")]
            public const string DersVeMufredat = nameof(Menu) + ".DersVeMufredat";
            [Description("Müfredat")]
            public const string Mufredat = nameof(Menu) + ".Müfredat";
            [Description("DönemDersleri")]
            public const string DonemDers = nameof(Menu) + ".DönemDersleri";
            [Description("Şubelendirme")]
            public const string Sube = nameof(Menu) + ".Şubelendirme";
            [Description("Ders Programı Planlama")]
            public const string DersProgram = nameof(Menu) + ".DersProgramıPlanlama";

            [Description("Sinav ve Not")]
            public const string SinavVeNot = nameof(Menu) + ".SinavVeNot";
            [Description("Sinav İşlemleri")]
            public const string SinavIslemleri = nameof(Menu) + ".Sinavİşlemleri";
            [Description("Sinav Listesi")]
            public const string SinavListesi = nameof(Menu) + ".SinavListesi";

            [Description("Öğrenci İşlemleri")]
            public const string OgrenciIslem = nameof(Menu) + ".Öğrenciİşlemleri";
            [Description("Öğrenciler")]
            public const string Ogrenciler = nameof(Menu) + ".Öğrenciler";
            [Description("Toplu Atama")]
            public const string TopluAtama = nameof(Menu) + ".TopluAtama";
            [Description("Sınıf Atlatma")]
            public const string SinifAtlatma = nameof(Menu) + ".SınıfAtlatma";
            [Description("Ders Aktarım")]
            public const string DersAktarim = nameof(Menu) + ".DersAktarım";
            [Description("Toplu Ders Kayıt")]
            public const string TopDersKayit = nameof(Menu) + ".TopluDersKayıt";

            [Description("E-Kayıt")]
            public const string EKayit = nameof(Menu) + ".E-Kayıt";
            [Description("Akademisyen İşlemleri")]
            public const string AkadeIslem = nameof(Menu) + ".Akademisyenİşlemleri";
            [Description("İdari Personel İşlem")]
            public const string PersonelIslem = nameof(Menu) + ".İdariPersonelİşlem";

            [Description("Rapor Ve İstatistik")]
            public const string RaporVeIstatistik = nameof(Menu) + ".RaporVeİstatistik";
            [Description("Rapor Öğrenci")]
            public const string RaporOgrenci = nameof(Menu) + ".RaporÖğrenci";
            [Description("Rapor ÖğrenciDers")]
            public const string RaporOgrDers = nameof(Menu) + ".RaporÖğrenciDers";

            [Description("Diploma Ve Mezuniyet")]
            public const string DiplomaVeMezuniyet = nameof(Menu) + ".DiplomaVeMezuniyet";
            [Description("Diploma")]
            public const string Diploma = nameof(Menu) + ".Diploma";
            [Description("Mezuniyet")]
            public const string Mezuniyet = nameof(Menu) + ".Mezuniyet";

            [Description("Paydaş Kurum İşlem")]
            public const string PaydasKurum = nameof(Menu) + ".PaydaşKurumİşlem";
            [Description("Paydaş İşlem")]
            public const string Paydas = nameof(Menu) + ".Paydaşİşlem";
            [Description("Kurum İşlem")]
            public const string Kurum = nameof(Menu) + ".Kurumİşlem";

            [Description("Akts Tyyç Katoloğu")]
            public const string AktsTyyc = nameof(Menu) + ".AktsTyyçKatoloğu";
            [Description("Akts")]
            public const string Akts = nameof(Menu) + ".Akts";
            [Description("Tyyç")]
            public const string Tyyc = nameof(Menu) + ".Tyyç";

            [Description("Uzaktan Eğitim")]
            public const string UzakEgitim = nameof(Menu) + ".UzaktanEğitim";
            [Description("Bağlantılar")]
            public const string Baglantilar = nameof(Menu) + ".Bağlantılar";
            [Description("Ayarlar")]
            public const string Ayarlar = nameof(Menu) + ".Ayarlar";

            [Description("Değişim Programı")]
            public const string DegProgram = nameof(Menu) + ".Değişim Programı";
            [Description("Başvurular")]
            public const string Basvuru = nameof(Menu) + ".Başvurular";
            [Description("Programlar")]
            public const string Programlar = nameof(Menu) + ".Programlar";


        }



        public static class Todo
        {
            [Description("Create a new ToDo")]
            public const string Create = nameof(Todo) + "." + nameof(Actions.Create);
            [Description("Read ToDos")]
            public const string Read = nameof(Todo) + "." + nameof(Actions.Read);
            [Description("Edit existing ToDos")]
            public const string Update = nameof(Todo) + "." + nameof(Actions.Update);
            [Description("Delete any ToDo")]
            public const string Delete = nameof(Todo) + "." + nameof(Actions.Delete);
        }
        public static class Role
        {
            [Description("Create a new Role")]
            public const string Create = nameof(Role) + "." + nameof(Actions.Create);
            [Description("Read roles data (permissions, etc.")]
            public const string Read = nameof(Role) + "." + nameof(Actions.Read);
            [Description("Edit existing Roles")]
            public const string Update = nameof(Role) + "." + nameof(Actions.Update);
            [Description("Delete any Role")]
            public const string Delete = nameof(Role) + "." + nameof(Actions.Delete);
        }
        public static class User
        {
            [Description("Create a new User")]
            public const string Create = nameof(User) + "." + nameof(Actions.Create);
            [Description("Read Users data (Names, Emails, Phone Numbers, etc.)")]
            public const string Read = nameof(User) + "." + nameof(Actions.Read);
            [Description("Edit existing users")]
            public const string Update = nameof(User) + "." + nameof(Actions.Update);
            [Description("Delete any user")]
            public const string Delete = nameof(User) + "." + nameof(Actions.Delete);
        }
    }
}