using Edge.Bills.Blazor.Services.Providers;
using Edge.Bills.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Edge.Bills.Blazor.Configuration
{
    public static class AuthorizationConfig
    {
        public static void ConfigureAutentication(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddBillsPolicies();
            });
            services.AddScoped<AppAuthenticationStateProvider>();
            services.AddScoped((Func<IServiceProvider, AuthenticationStateProvider>)(provider => provider.GetRequiredService<AppAuthenticationStateProvider>()));
        }

        private static AuthorizationOptions AddBillsPolicies(this AuthorizationOptions options)
        {
            var policies = BillsPoliciesHelper.GetPolicies();
            foreach (var policy in policies)
            {
                options.AddPolicy(policy.Item1, x => x.RequireRole(policy.Item2));
            }
            return options;
        }
    }
}
