using Microsoft.EntityFrameworkCore;
using Taskio.App.IRepository;
using Taskio.Domain.Entities;

namespace Taskio.Persist.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }
    }
}