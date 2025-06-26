using Microsoft.EntityFrameworkCore;
using Taskio.Domain.Entities;

namespace Taskio.Persist
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ExceptionLog> ExceptionLogs => Set<ExceptionLog>();

        public DbSet<User> Users => Set<User>();
    }
}