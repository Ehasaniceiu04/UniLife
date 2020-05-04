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