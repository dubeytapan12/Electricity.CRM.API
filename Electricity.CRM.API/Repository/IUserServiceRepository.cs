using Electricity.CRM.API.Entity;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(User users);

        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);

        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        int SaveCommit();
    }
}
