namespace TaskManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<TaskComment> TaskComments { get; set; }
    }

}
