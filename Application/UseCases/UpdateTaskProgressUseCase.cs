using TaskmanagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskmanagementSystem.Core.Interfaces;

using Octopus.Client.Repositories.Async;

namespace TaskmanagementSystem.Application.UseCases
{
    public class UpdateTaskProgressUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskProgressUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(int taskId, string progressStatus)
        {
            await _taskRepository.UpdateProgressAsync(taskId, progressStatus);
        }
    }
}
