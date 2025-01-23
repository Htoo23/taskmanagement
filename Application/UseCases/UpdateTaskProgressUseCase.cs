using TaskmanagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskmanagementSystem.Core.Interfaces;

using Octopus.Client.Repositories.Async;

namespace TaskmanagementSystem.Application.UseCases
{
    public class UpdateTaskProgressUseCase
    {
        private readonly IRepository<TaskItem> _repository;
        public interface ITaskRepository
        {
            Task UpdateProgressAsync(int taskId, string progressStatus);
        }


        public UpdateTaskProgressUseCase(IRepository<TaskItem> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task ExecuteAsync(int taskId, string progressStatus)
        {
            var task = await _repository.GetByIdAsync(taskId);
            task.ProgressStatus = progressStatus;
            await _repository.UpdateAsync(task);

        }
    }
    }
