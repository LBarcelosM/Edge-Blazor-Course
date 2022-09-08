using Blazored.LocalStorage;
using Edge.Bills.Blazor.Services.Interfaces;
using Edge.Bills.Blazor.Services.Providers;
using Edge.Bills.Security.Enums;
using Edge.Bills.Shared.Enums;
using Edge.Bills.Shared.ViewModels.Authorization;

namespace Edge.Bills.Blazor.Services.Services
{
    public class MemoryAuthenticationService : IAuthenticationService
    {
        public const string SESSION_USER_KEY = "SESSION_USER_KEY";
        private readonly ILocalStorageService _localStorageSevice;
        private readonly AppAuthenticationStateProvider _appAuthenticationStateProvider;
        private IUserService _userService;

        public MemoryAuthenticationService(
            ILocalStorageService localStorageSevice,
            AppAuthenticationStateProvider appAuthenticationStateProvider,
            IUserService userService)
        {
            _localStorageSevice = localStorageSevice;
            _appAuthenticationStateProvider = appAuthenticationStateProvider;
            _userService = userService;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            var user = await _userService.GetByEmail(model.Email);
            if(user != null && user.Password == model.Password)
            {
                var authenticatedUserModel = new AuthenticatedUserModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Roles = GetPermissions(user.UserType).Select(x => x.ToString()),
                    Token = Guid.NewGuid().ToString()
                };
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

        private IEnumerable<EPermissionType> GetPermissions(EUserType userType)
        {
            switch (userType)
            {
                case EUserType.Administrator:
                    {
                        return new List<EPermissionType> {
                            EPermissionType.MANAGE_USER,
                            EPermissionType.MANAGE_BILL,
                            EPermissionType.VIEW_USERS,
                            EPermissionType.VIEW_BILLS
                        };
                    }
                case EUserType.Visualizer:
                    {
                        return new List<EPermissionType> {
                            EPermissionType.VIEW_USERS,
                            EPermissionType.VIEW_BILLS
                        };
                    }
                default:
                    {
                        return new List<EPermissionType>();
                    }
            }
        }
    }
}
