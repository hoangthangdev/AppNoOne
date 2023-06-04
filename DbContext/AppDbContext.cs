using Microsoft.EntityFrameworkCore;
using Models;

namespace IDbContext
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> User { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<UserRoles> UserRole { get; set; }

        public DbSet<RoleClaims> RoleClaim { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
                .HasKey(x => x.Id);

            builder.Entity<Roles>()
                .HasKey(x => x.Id);

            builder.Entity<UserRoles>()
           .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRoles>()
                .HasOne(ur => ur.Roles)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<UserRoles>()
                .HasOne(ur => ur.Users)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<RoleClaims>()
                .HasKey(x => x.Id);

            builder.Entity<RoleClaims>()
                .HasOne(ur => ur.Roles)
                .WithMany(u => u.RoleClaims)
                .HasForeignKey(ur => ur.RoleId);

            base.OnModelCreating(builder);
        }
    }
}
