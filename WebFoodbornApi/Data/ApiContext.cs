using Microsoft.EntityFrameworkCore;
using WebFoodbornApi.Models;

namespace WebFoodbornApi.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<Orgnazition> Orgnazitions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orgnazition>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<User>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<Permission>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<Role>().HasQueryFilter(m => m.Status != "删除");
        }
    }
}
