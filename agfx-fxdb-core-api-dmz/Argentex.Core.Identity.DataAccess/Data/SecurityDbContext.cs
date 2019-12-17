using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Argentex.Core.Identity.DataAccess
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activies { get; set; }
        public virtual DbSet<ActivityLog> ActivieLogs { get; set; }
        public virtual DbSet<CountryGroup> CountryGroups { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<PreviousPassword> PreviousPasswords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .ToTable("User");
            builder.Entity<ApplicationUser>()
                .Property(p => p.CreateDate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<ApplicationUser>()
                .Property(p => p.LastPasswordChange)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<ApplicationUser>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.PreviousPasswords);
            builder.Entity<PreviousPassword>()
                .HasOne(e => e.User)
                .WithMany(e => e.PreviousPasswords)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_PreviousPasswords_User");

            builder.Entity<ApplicationRole>().ToTable("Role");
            builder.Entity<ApplicationUserRole>().ToTable("UserRole");
            builder.Entity<ApplicationUserClaim>().ToTable("UserClaim");
            builder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            builder.Entity<ApplicationRoleClaim>().ToTable("RoleClaim");
            builder.Entity<ApplicationUserToken>().ToTable("UserToken");

            //additional tables
            builder.Entity<Activity>().ToTable("Activity");

            builder.Entity<ActivityLog>().ToTable("ActivityLog");

            builder.Entity<CountryGroup>().ToTable("CountryGroup");

            builder.Entity<Country>().ToTable("Country");

            builder.Entity<Report>().ToTable("Report");
            builder.Entity<UserReport>().ToTable("UserReport")
                .HasKey(x => new { x.ReportId, x.UserId });
            builder.Entity<UserReport>().ToTable("UserReport");
            builder.Entity<UserReport>().ToTable("UserReport");
            builder.Entity<Token>().ToTable("Token");
        }
    }
}
