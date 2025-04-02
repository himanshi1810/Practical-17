using Microsoft.EntityFrameworkCore;
using Practical_17.Models;
using Practical_17.ViewModels;

namespace Practical_17.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Role>(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = Role.Admin },
                new Role { Id = 2, Name = Role.User }
            );
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Practical_17.ViewModels.StudentViewModel> StudentViewModel { get; set; } = default!;
        public DbSet<Practical_17.ViewModels.CreateStudentViewModel> CreateStudentViewModel { get; set; } = default!;
        public DbSet<Practical_17.ViewModels.EditStudentViewModel> EditStudentViewModel { get; set; } = default!;
    }
}
