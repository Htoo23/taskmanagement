using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskmanagementSystem.Core.Entities;
using TaskmanagementSystem.ViewModels;
using System.Collections.Generic;

namespace TaskmanagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        public TaskViewModel TaskViewModel { get; set; }

        public void OnGet()
        {
            // Mock data or actual database data
            TaskViewModel = new TaskViewModel
            {
                Tasks = new List<TaskItem>
                {
                    new TaskItem { Id = 1, Title = "Task 1", Description = "First Task", Priority = "High", Deadline = "2025-01-31", Progress = "In Progress" },
                    new TaskItem { Id = 2, Title = "Task 2", Description = "Second Task", Priority = "Medium", Deadline = "2025-02-15", Progress = "Completed" }
                }
            };
        }
    }
}
