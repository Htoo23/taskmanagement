﻿
namespace TaskmanagementSystem.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public DateTime Deadline { get; set; }

        public string Status {  get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string ProgressStatus { get; set; }
    }
}
