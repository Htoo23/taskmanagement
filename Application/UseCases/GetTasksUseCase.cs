using TaskmanagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskmanagementSystem.Core.Interfaces;

namespace TaskmanagementSystem.Application.UseCases
{
    public class GetTasksUseCase
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public GetTasksUseCase(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItem>> ExecuteAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task AddAsync(TaskItem task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
