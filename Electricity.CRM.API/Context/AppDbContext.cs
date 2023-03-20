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

        public virtual DbSet<ElectricityBiller> ElectricityBiller { get; set; }
        //As Per requirement created 4 different table for electricity user 
        public virtual DbSet<ElectricityUserFlat> ElectricityUserFlat { get; set; }
        public virtual DbSet<ElectricityUserFactory> ElectricityUserFactory { get; set; }
        public virtual DbSet<ElectricityUserResidential> ElectricityUserResidential { get; set; }
        public virtual DbSet<ElectricityUserCommercial> ElectricityUserCommercial { get; set; }

        public virtual DbSet<TechnologyEnablement> TechnologyEnablement { get; set; }
        public virtual DbSet<EducationOrCertification> EducationOrCertification { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<Experience> Experience { get; set; }
        public virtual DbSet<Client> Client { get; set; }

        public virtual DbSet<Resume> Resume { get; set; }
        public virtual DbSet<Project> Project { get; set; }
    }
}
