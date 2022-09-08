using Edge.Bills.Shared.ViewModels.Authorization;

namespace Edge.Bills.Domain.Interfaces
{
    public interface IAuthorizationDomainService
    {
        Task<AuthenticatedUserModel?> Authenticate(LoginViewModel viewModel);
    }
}
