using Edge.Bills.Data.Context;
using Edge.Bills.Data.Repositories;
using Edge.Bills.Domain.DomainServices;
using Edge.Bills.Domain.Interfaces;
using Edge.Bills.Shared.Helpers;
using Edge.Bills.Shared.ViewModels.Authorization;
using Edge.Bills.Shared.ViewModels.Bill;
using Edge.Bills.Shared.ViewModels.Request;
using Edge.Bills.Shared.ViewModels.User;
using Edge.Bills.WebApi.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edge.Bills.WebApi.Configuration
{
    public static class BillsModuleConfiguration
    {
        public static WebApplicationBuilder? ConfigBillsModule(this WebApplicationBuilder? builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddEndpointsApiExplorer();
            builder.ConfigSwagger();
            builder.Services.AddDbContext<BillsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBillRepository, BillRepository>();
            builder.Services.AddScoped<IProfileDomainService, ProfileDomainService>();
            builder.Services.AddScoped<IGenerateTokenService, GenerateTokenService>();
            builder.Services.AddScoped<IAuthorizationDomainService, AuthorizationDomainService>();

            return builder;
        }

        public static WebApplication? MapBillRoutes(this WebApplication? app)
        {
            // User
            app?.MapGet("/users", async (IUserRepository userRepository) =>
            {
                var result = await userRepository.GetList();
                return CreateResult(true, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.VIEW_USERS);
            app?.MapGet("/users/{id:Guid}", async (
                Guid id,
                IUserRepository userRepository) =>
            {
                var result = await userRepository.GetById(id);
                return CreateResult(true, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.VIEW_USERS);
            app?.MapPost("/users", async (
                [FromBody] UserFullViewModel model,
                IUserRepository userRepository) =>
            {
                var result = await userRepository.Add(model);
                return CreateResult(result > 0, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.MANAGE_USER);
            app?.MapPut("/users", async (
                [FromBody] UserFullViewModel model,
                IUserRepository userRepository) =>
            {
                var result = await userRepository.Update(model);
                return CreateResult(result > 0, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.MANAGE_USER);
            app?.MapDelete("/users/{id:Guid}", async (
                Guid id,
                IUserRepository userRepository) =>
            {
                var result = await userRepository.Delete(id);
                return CreateResult(result > 0, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.MANAGE_USER);
            // Bills
            app?.MapGet("/bills", async (IBillRepository billRepository) =>
            {
                var result = await billRepository.GetList();
                return CreateResult(true, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.VIEW_BILLS);
            app?.MapGet("/bills/{id:long}", async (
                long id,
                IBillRepository billRepository) =>
            {
                var result = await billRepository.GetById(id);
                return CreateResult(true, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.VIEW_BILLS);
            app?.MapGet("/bills/user/{id:Guid}", async (
                Guid id,
                IBillRepository billRepository) =>
            {
                var result = await billRepository.GetByUserId(id);
                return CreateResult(true, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.VIEW_BILLS);
            app?.MapPost("/bills", async (
                [FromBody] BillFullViewModel model,
                IBillRepository billRepository) =>
            {
                var result = await billRepository.Add(model);
                return CreateResult(result > 0, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.MANAGE_BILL);
            app?.MapPut("/bills", async (
                [FromBody] BillFullViewModel model,
                IBillRepository billRepository) =>
            {
                var result = await billRepository.Update(model);
                return CreateResult(result > 0, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.MANAGE_BILL);
            app?.MapDelete("/bills/{id:long}", async (
                long id,
                IBillRepository billRepository) =>
            {
                var result = await billRepository.Delete(id);
                return CreateResult(result > 0, result);
            }).RequireAuthorization(BillsPoliciesHelper.POLICIES.MANAGE_BILL);
            // Authentication
            app?.MapPost("/authentication", async (
                [FromBody] LoginViewModel model,
                IAuthorizationDomainService service) =>
            {
                var result = await service.Authenticate(model);
                return CreateResult(result != null, result);
            }).AllowAnonymous();
            return app;
        }

        private static IResult CreateResult<T>(bool success, T? data)
        {
            var result = new RequestResult<T>
            {
                Success = success,
                Data = data
            };
            return success ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
