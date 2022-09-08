using Edge.Bills.Blazor.CustomComponents.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Edge.Bills.Blazor.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IToastService ToastService { get; set; }
        [Inject] public ILoadingService LoadingService { get; set; }
        [CascadingParameter] public Task<AuthenticationState> _authenticationStateTask { get; set; }
        protected Task<bool> IsAuthenticated => VerifyIsAuthenticated();
        private async Task<bool> VerifyIsAuthenticated()
        {
            var authenticationState = await _authenticationStateTask;
            return authenticationState?.User?.Identity != null &&
                authenticationState.User.Identity.IsAuthenticated;
        }
    }
}
