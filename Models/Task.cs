using Microsoft.AspNetCore.Mvc;

namespace TaskmanagementSystem.Models
{
    public class Task 
    {

        public int Id { get; set; }

        public string Title {  get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public string Priority {  get; set; }

        public bool IsCompleted { get; set; }
        
    }
}
