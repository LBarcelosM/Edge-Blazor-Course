using Blazored.LocalStorage;
using Edge.Bills.Blazor.Services.Services;
using Edge.Bills.Shared.ViewModels.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Edge.Bills.Blazor.Services.Providers
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public AppAuthenticationStateProvider(
            HttpClient httpClient,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sessionUser = await _localStorageService.GetItemAsync<AuthenticatedUserModel?>(ApiAuthenticationService.SESSION_USER_KEY);
            var identity = sessionUser == null
            ? new ClaimsIdentity()
                : new ClaimsIdentity(GetRoleClaimList(sessionUser.Roles), "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void MarkUserAuthenticated(AuthenticatedUserModel model)
        {
            var identity = new ClaimsIdentity(GetRoleClaimList(model.Roles), "jwt");
            var authState = new AuthenticationState(new ClaimsPrincipal(identity));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", model.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public void MarkUserLoggedOut()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        public static Claim[] GetRoleClaimList(IEnumerable<string> roles)
        {
            var claimList = new List<Claim>();
            if ((roles?.Any()).GetValueOrDefault())
            {
                claimList = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            }
            return claimList.ToArray();
        }
    }
}
