using Edge.Bills.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Shared
{
    partial class NavMenu
    {
        [Inject] public IAuthenticationService AuthenticationService { get; set; }

        private bool collapseNavMenu = true;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private async Task Logoff()
        {
            await AuthenticationService.Logoff();
        }
    }
}
