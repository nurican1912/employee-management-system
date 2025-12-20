using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("Id");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.Password).HasColumnName("PasswordHash");
                entity.Property(e => e.Role).HasColumnName("Role");
            });
        }
    }
}
