using Microsoft.AspNetCore.Mvc;
using TaskmanagementSystem.Application.UseCases;
using TaskmanagementSystem.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace TaskmanagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly GetTasksUseCase _getTasksUseCase;
        private readonly UpdateTaskProgressUseCase _updateTaskProgressUseCase;


        public TasksController(GetTasksUseCase getTasksUseCase, UpdateTaskProgressUseCase updateTaskProgressUseCase)
        {
            _getTasksUseCase = getTasksUseCase;
            _updateTaskProgressUseCase = updateTaskProgressUseCase;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _getTasksUseCase.ExecuteAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            await _getTasksUseCase.AddAsync(task);
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem task)
        {
            if (id != task.Id)
                return BadRequest();

            await _getTasksUseCase.UpdateAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _getTasksUseCase.DeleteAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}/progress")]
        public async Task<IActionResult> UpdateProgress(int id, [FromBody] string progressStatus)
        {
            await _updateTaskProgressUseCase.ExecuteAsync(id, progressStatus);
            return NoContent();
        }
    }
}
