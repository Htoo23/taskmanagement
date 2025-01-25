namespace TaskmanagementSystem.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Deadline { get; set; }
        public string Progress { get; set; }
        public string ProgressStatus { get; internal set; }
    }
}
