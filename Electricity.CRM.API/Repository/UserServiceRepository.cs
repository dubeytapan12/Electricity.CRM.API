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

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _db.UserRefreshToken.Add(user);
            return user;
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
    }
}
