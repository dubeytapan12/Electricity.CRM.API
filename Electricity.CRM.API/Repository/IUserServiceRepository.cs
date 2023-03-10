using Electricity.CRM.API.Entity;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(User users);
        Task<User> GetUserByUserName(string userName);
        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);
        ForgotPassword SaveForgotPassword(ForgotPassword forgotPassword);

        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        void UpdateUserToken(string username, string token);
        void DeleteAllUserTokens(string username);
        Task<bool> IsValidUserForReset(string username, string token);
        void UpdateUserPassword(string username,string token, string password);
        int SaveCommit();
    }
}
