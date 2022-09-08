using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Shared.ViewModels.Authorization;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Pages.Authentication
{
    partial class LoginPage
    {
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        private LoginViewModel _loginModel = new LoginViewModel
        {
            Email = "master@master.com",
            Password = "123456"
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (await IsAuthenticated)
                {
                    NavigationManager.NavigateTo("/home");
                }
            }
        }

        private async Task Logar()
        {
            LoadingService.Show();            
            if (await AuthenticationService.Login(_loginModel))
            {
                NavigationManager.NavigateTo("/home");
            }
            else
            {
                ToastService.AddError("Dados inválidos!");
            }
            LoadingService.Hide();
        }
    }
}
