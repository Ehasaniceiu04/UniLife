using Microsoft.AspNetCore.Authorization;

//This is ASP.Net Core Identity
//see https://chrissainty.com/securing-your-blazor-apps-configuring-policy-based-authorization-with-blazor/

namespace UniLife.Shared.AuthorizationDefinitions
{
    public static class Policies
    {
        public const string IsAdmin = "IsAdmin";
        public const string IsOgrenci = "IsOgrenci";
        public const string IsAkademisyen = "IsAkademisyen";
        public const string IsPersonel = "IsPersonel";
        public const string IsUser = "IsUser";
        public const string IsReadOnly = "IsReadOnly";
        public const string IsMyDomain = "IsMyDomain";

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("IsAdministrator")
                .Build();
        }

        public static AuthorizationPolicy IsOgrenciPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("IsOgrenci")
                .Build();
        }

        public static AuthorizationPolicy IsAkademisyenPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("IsAkademisyen")
                .Build();
        }

        public static AuthorizationPolicy IsPersonelPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("IsPersonel")
                .Build();
        }

        public static AuthorizationPolicy IsUserPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("IsUser")
                .Build();
        }

        public static AuthorizationPolicy IsReadOnlyPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("ReadOnly", "true")
                .Build();
        }

        public static AuthorizationPolicy IsMyDomainPolicy()
        {
            return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(new DomainRequirement("blazorboilerplate.com"))
            .Build();                
        }
    }
}
