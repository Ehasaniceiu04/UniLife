using System.ComponentModel;

namespace Semerkand.Shared.AuthorizationDefinitions
{
    public static class Actions
    {
        public const string Create = nameof(Create);
        public const string Read = nameof(Read);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
    }

    public static class Permissions
    {
        public static class Ogretmen
        {
            [Description("Create a new Ogretmen")]
            public const string Create = nameof(Ogretmen) + "." + nameof(Actions.Create);
            [Description("Read Ogretmens")]
            public const string Read = nameof(Ogretmen) + "." + nameof(Actions.Read);
            [Description("Edit existing Ogretmens")]
            public const string Update = nameof(Ogretmen) + "." + nameof(Actions.Update);
            [Description("Delete any Ogretmen")]
            public const string Delete = nameof(Ogretmen) + "." + nameof(Actions.Delete);
        }
        public static class DersKayit
        {
            [Description("Create a new DersKayit")]
            public const string Create = nameof(DersKayit) + "." + nameof(Actions.Create);
            [Description("Read DersKayits")]
            public const string Read = nameof(DersKayit) + "." + nameof(Actions.Read);
            [Description("Edit existing DersKayits")]
            public const string Update = nameof(DersKayit) + "." + nameof(Actions.Update);
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
        public static class WeatherForecasts
        {
            [Description("Read Weather Forecasts")]
            public const string Read = nameof(WeatherForecasts) + "." + nameof(Actions.Read);
        }
    }
}