namespace TaskManagementAPI.Models
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TaskItemId { get; set; }
        public int UserId { get; set; }

        // Navigation Properties
        public TaskItem TaskItem { get; set; }
        public User User { get; set; }
    }
}
