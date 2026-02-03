using Domain.Entities.seguridad;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class SqlDbContext(DbContextOptions<SqlDbContext> options): DbContext(options)
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.ToTable("UserRoles", "SEG");
                        j.HasKey("UserId", "RoleId");
                    });
        }

    }
}
