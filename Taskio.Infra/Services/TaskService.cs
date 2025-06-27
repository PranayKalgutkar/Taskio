using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskio.App.DTOs;
using Taskio.App.IRepository;
using Taskio.App.IServices;
using Taskio.Domain.Entities;

namespace Taskio.Infra.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<TaskDto>> GetTasks()
        {
            return await _repo.GetTasks();

            // return tasks.Select(t => new TaskDto
            // {
            //     Title = t.Title,
            //     Description = t.Description,
            //     DueDate = t.DueDate,
            //     Status = t.Status,
            //     AssignedUserId = t.AssignedUserId,
            //     CreatedById = t.CreatedById
            // });
        }
        public async Task<bool> CreateTask(TaskDto dto)
        {
            var task = new UserTask
            {
                TaskId = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = dto.Status,
                AssignedUserId = dto.AssignedUserId,
                CreatedById = dto.CreatedById,
                CreatedOn = DateTime.UtcNow
            };

            return await _repo.AddTask(task);
        }
    }
}