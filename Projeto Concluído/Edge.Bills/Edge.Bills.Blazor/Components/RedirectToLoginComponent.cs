using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace Edge.Bills.Blazor.Components
{
    public class RedirectToLoginComponent : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> _authenticationStateTask { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await _authenticationStateTask;
            if (authenticationState?.User?.Identity is null || !authenticationState.User.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/login");
            }
        }
    }
}
