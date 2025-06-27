using Microsoft.AspNetCore.Mvc;
using Taskio.App.DTOs;
using Taskio.App.IServices;

namespace Taskio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("fetch-tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto dto)
        {
            var result = await _taskService.CreateTask(dto);
            if (!result)
                return BadRequest("Failed to create task");

            return Ok("Task created successfully");
        }
    }
}