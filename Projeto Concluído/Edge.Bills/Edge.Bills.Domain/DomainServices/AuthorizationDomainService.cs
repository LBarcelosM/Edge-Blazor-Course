using Edge.Bills.Domain.Interfaces;
using Edge.Bills.Shared.ViewModels.Authorization;

namespace Edge.Bills.Domain.DomainServices
{
    public class AuthorizationDomainService : IAuthorizationDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly IGenerateTokenService _generateTokenService;

        public AuthorizationDomainService(IUserRepository userRepository, IProfileDomainService profileDomainService, IGenerateTokenService generateTokenService)
        {
            _userRepository = userRepository;
            _profileDomainService = profileDomainService;
            _generateTokenService = generateTokenService;
        }

        public async Task<AuthenticatedUserModel?> Authenticate(LoginViewModel viewModel)
        {
            var user = await _userRepository.GetByEmail(viewModel.Email);
            if(user != null)
            {
                if(user.Password == viewModel.Password)
                {
                    var authenticatedUserModel = new AuthenticatedUserModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Roles = _profileDomainService.GetPermissions(user.UserType).Select(x => x.ToString())
                    };
                    authenticatedUserModel.Token = _generateTokenService.GenerateToken(authenticatedUserModel);
                    return authenticatedUserModel;
                }
            }
            return null;
        }
    }
}
