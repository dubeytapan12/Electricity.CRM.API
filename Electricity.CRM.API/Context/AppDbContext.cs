using Electricity.CRM.API.Entity;
using Microsoft.EntityFrameworkCore;

namespace Electricity.CRM.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ForgotPassword> ForgotPasswords { get; set; }
    }
}
