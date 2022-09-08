using Edge.Bills.Shared.ViewModels.Authorization;

namespace Edge.Bills.Domain.Interfaces
{
    public interface IGenerateTokenService
    {
        public string GenerateToken(AuthenticatedUserModel authenticatedUserModel);
    }
}
