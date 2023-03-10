using Electricity.CRM.API.Context;
using Electricity.CRM.API.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public class UserServiceRepository : IUserServiceRepository
    {
       private readonly AppDbContext _db;

        public UserServiceRepository( AppDbContext db)
        {
            this._db = db;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
           return await _db.Users.FirstOrDefaultAsync(u=>u.UserName == userName);
        }
        public ForgotPassword SaveForgotPassword(ForgotPassword forgotPassword)
        {
            _db.ForgotPasswords.Add(forgotPassword);
            return forgotPassword;
        }
        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _db.UserRefreshToken.Add(user);
            return user;
        }
        public void UpdateUserToken(string username, string token)
        {
            var item = _db.Users.FirstOrDefault(x => x.UserName == username);
            if(item != null)
            {
                item.ForgotPasswordToken = token;
            }
        }
        public void DeleteAllUserTokens(string username)
        {
            var items = _db.UserRefreshToken.Where(u => u.UserName == username);
            if (items != null)
            {
                _db.UserRefreshToken.RemoveRange(items);
            }
        }
        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshToken.Remove(item);
            }
        }

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return _db.SaveChanges();
        }

        public async Task<bool> IsValidUserAsync(User users)
        {
            var u = _db.Users.FirstOrDefault(o => o.UserName == users.UserName);
            if(u != null)
            {
               return await _db.Users.AnyAsync(u=> u.UserName==users.UserName && u.Password==users.Password);
            }
            return false;

        }
        public async Task<bool> IsValidUserForReset(string username, string token)
        {
            return await _db.Users.AnyAsync(u=> u.UserName==username && u.ForgotPasswordToken==token);
        }

        public void UpdateUserPassword(string username, string token, string password)
        {
            var item = _db.Users.FirstOrDefault(u=> u.UserName== username && u.ForgotPasswordToken==token);
            if(item != null)
            {
                item.Password = password;
                item.ForgotPasswordToken = null;
            }
        }

    }
}
