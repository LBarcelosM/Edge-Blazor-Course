using Edge.Bills.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigCors();
builder.ConfigBillsModule();
builder.ConfigSecurity();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigUseSwagger();
    app.ConfigUseSecurity();
}

app.UseHttpsRedirection();

app.MapBillRoutes();

app.ConfigUseCors();

app.Run();