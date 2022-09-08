using Edge.Bills.Blazor.CustomComponents.Interfaces;
using Edge.Bills.Blazor.CustomComponents.Services;
using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Blazor.Services.Services;

namespace Edge.Bills.Blazor.Configuration
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoadingService, LoadingService>();
            services.AddSingleton<IToastService, ToastService>();
            //services.AddSingleton<IUserService, MemoryUserService>();
            //services.AddScoped<IAuthenticationService, MemoryAuthenticationService>();
            services.AddScoped<IUserService, ApiUserService>();
            services.AddScoped<IBillService, ApiBillService>();
            services.AddScoped<IAuthenticationService, ApiAuthenticationService>();
        }
    }
}
