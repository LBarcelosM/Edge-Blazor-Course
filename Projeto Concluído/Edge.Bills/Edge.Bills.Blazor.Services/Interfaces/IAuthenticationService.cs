using Edge.Bills.Shared.ViewModels.Authorization;

namespace Edge.Bills.Blazor.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Login(LoginViewModel model);
        Task Logoff();
        Task RefreshLoggedUser();
    }
}
