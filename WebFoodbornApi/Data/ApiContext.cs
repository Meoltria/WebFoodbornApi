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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<InitialDiagnosis> InitialDiagnoses { get; set; }
        public DbSet<PastMedicalHistory> PastMedicalHistories { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<FoodInfo> FoodInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orgnazition>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<User>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<Permission>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<Role>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<Patient>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<InitialDiagnosis>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<PastMedicalHistory>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<Symptom>().HasQueryFilter(m => m.Status != "删除");
            modelBuilder.Entity<FoodInfo>().HasQueryFilter(m => m.Status != "删除");
        }
    }
}
