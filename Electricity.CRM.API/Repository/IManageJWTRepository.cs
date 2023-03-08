using Electricity.CRM.API.Entity;
using System.Security.Claims;

namespace Electricity.CRM.API.Repository
{
    public interface IManageJWTRepository
    {
        Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
