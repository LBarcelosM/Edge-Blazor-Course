using Edge.Bills.Shared.Helpers;
using Edge.Bills.WebApi.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Edge.Bills.WebApi.Configuration
{
    public static class SecurityConfig
    {
        public static WebApplicationBuilder ConfigSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecuritySettings.SecretBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            ConfigBillsPolicies(builder);
            return builder;
        }

        public static WebApplicationBuilder ConfigBillsPolicies(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                var policies = BillsPoliciesHelper.GetPolicies();
                foreach (var policy in policies)
                {
                    options.AddPolicy(policy.Item1, x => x.RequireRole(policy.Item2));
                }
            });
            return builder;
        }

        public static WebApplication ConfigUseSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
