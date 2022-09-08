using Microsoft.OpenApi.Models;

namespace Edge.Bills.WebApi.Configuration
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder ConfigSwagger(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token based security",
                };

                var securityReq = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                };

                var contact = new OpenApiContact()
                {
                    Name = "Leonardo",
                    Email = "leonardo.barcelos@edge.ufal.br",
                    Url = new Uri("http://www.lbarcelosm.com.br")
                };

                var license = new OpenApiLicense()
                {
                    Name = "Free License",
                    Url = new Uri("http://www.lbarcelosm.com.br")
                };

                var info = new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Minimal API - JWT Authentication with Swagger demo",
                    Description = "Implementing JWT Authentication in Minimal API",
                    TermsOfService = new Uri("http://www.example.com"),
                    Contact = contact,
                    License = license
                };

                builder.Services.AddSwaggerGen(o =>
                {
                    o.SwaggerDoc("v1", info);
                    o.AddSecurityDefinition("Bearer", securityScheme);
                    o.AddSecurityRequirement(securityReq);
                });
            }
            return builder;
        }

        public static WebApplication ConfigUseSwagger(this WebApplication application)
        {
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            return application;
        }
    }
}
