using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Taskio.App.DTOs;
using Taskio.App.IRepository;
using Taskio.Domain.Entities;

namespace Taskio.Persist.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TaskDto>> GetTasks()
        {
            var result = await (from t in _dbContext.Tasks
                                join au in _dbContext.Users on t.AssignedUserId equals au.UserId
                                join cu in _dbContext.Users on t.CreatedById equals cu.UserId
                                select new TaskDto
                                {
                                    Title = t.Title,
                                    Description = t.Description,
                                    DueDate = t.DueDate,
                                    Status = t.Status,
                                    AssignedUserId = au.UserId,
                                    AssignedUser = au.FullName,
                                    CreatedById = t.CreatedById,
                                    CreatedBy = cu.FullName
                                }).ToListAsync();
            return result;
        }

        public async Task<bool> AddTask(UserTask task)
        {
            _dbContext.Tasks.Add(task);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}