using Edge.Bills.Domain.Interfaces;
using Edge.Bills.Shared.ViewModels.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Edge.Bills.WebApi.Security
{
    public class GenerateTokenService : IGenerateTokenService
    {
        public string GenerateToken(AuthenticatedUserModel authenticatedUserModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = SecuritySettings.SecretBytes;
            var claimList = GetClaimList(authenticatedUserModel);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Claim[] GetClaimList(AuthenticatedUserModel authenticatedUserModel)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim(ClaimTypes.PrimarySid, authenticatedUserModel.Id.ToString()));
            claimList.Add(new Claim(ClaimTypes.Name, authenticatedUserModel.Name));
            foreach (var role in authenticatedUserModel.Roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }
            return claimList.ToArray();
        }
    }
}
