using Edge.Bills.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Shared
{
    partial class MainLayout
    {
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AuthenticationService.RefreshLoggedUser();
            }
        }
    }
}
