using Blazored.LocalStorage;
using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Blazor.Services.Providers;
using Edge.Bills.Shared.ViewModels.Authorization;
using Edge.Bills.Shared.ViewModels.Request;

namespace Edge.Bills.Blazor.Services.Services
{
    public class ApiAuthenticationService : BaseApiService, IAuthenticationService
    {
        public const string SESSION_USER_KEY = "SESSION_USER_KEY";
        private readonly ILocalStorageService _localStorageSevice;
        private readonly AppAuthenticationStateProvider _appAuthenticationStateProvider;

        public ApiAuthenticationService(HttpClient httpClient, ILocalStorageService localStorageSevice, AppAuthenticationStateProvider appAuthenticationStateProvider) : base ("authentication", httpClient)
        {
            _localStorageSevice = localStorageSevice;
            _appAuthenticationStateProvider = appAuthenticationStateProvider;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            var result = await Post<LoginViewModel, RequestResult<AuthenticatedUserModel>>(ServiceUrl(), model);
            if (result != null && result.Success)
            {
                var authenticatedUserModel = result.Data as AuthenticatedUserModel;
                await _localStorageSevice.SetItemAsync(SESSION_USER_KEY, authenticatedUserModel);
                _appAuthenticationStateProvider.MarkUserAuthenticated(authenticatedUserModel);
                return true;
            }
            return false;
        }

        public async Task Logoff()
        {
            await _localStorageSevice.RemoveItemAsync(SESSION_USER_KEY);
            _appAuthenticationStateProvider.MarkUserLoggedOut();
        }

        public async Task RefreshLoggedUser()
        {
            var savedUser = await _localStorageSevice.GetItemAsync<AuthenticatedUserModel?>(SESSION_USER_KEY);
            if (savedUser != null)
            {
                _appAuthenticationStateProvider.MarkUserAuthenticated(savedUser);
            }
        }
    }
}