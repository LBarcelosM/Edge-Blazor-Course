using Blazored.LocalStorage;

namespace Edge.Bills.Blazor.Configuration
{
    public static class GeneralConfig
    {
        private static string BASE_URL = "https://localhost:7123/";
        public static void ConfigureGeneral(this IServiceCollection services)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(BASE_URL) });
            services.AddBlazoredLocalStorage();
        }
    }
}
