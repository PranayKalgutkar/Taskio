using Microsoft.EntityFrameworkCore;
using Taskio.Domain.Entities;

namespace Taskio.Persist
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ExceptionLog> ExceptionLogs => Set<ExceptionLog>();

        public DbSet<User> Users => Set<User>();
        public DbSet<UserTask> Tasks => Set<UserTask>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly map UserTask to tbl_task
            modelBuilder.Entity<UserTask>().ToTable("tbl_task");

            // Configure foreign keys
            // modelBuilder.Entity<UserTask>()
            //     .HasOne(t => t.AssignedUserNavigation)
            //     .WithMany() // .WithMany(u => u.AssignedTasks) if you add that nav to User
            //     .HasForeignKey(t => t.AssignedUser)
            //     .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<UserTask>()
            //     .HasOne(t => t.CreatedByNavigation)
            //     .WithMany() // .WithMany(u => u.CreatedTasks) if needed
            //     .HasForeignKey(t => t.CreatedBy)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}