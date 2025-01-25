using System.Collections.Generic;
using TaskmanagementSystem.Core.Entities;

namespace TaskmanagementSystem.ViewModels
{
    public class TaskViewModel
    {
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
