namespace Edge.Bills.WebApi.Configuration
{
    public static class CorsConfig
    {
        public static WebApplicationBuilder ConfigCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });
            return builder;
        }

        public static WebApplication ConfigUseCors(this WebApplication app)
        {
            app.UseCors();
            return app;
        }
    }
}
