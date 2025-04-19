using System.ComponentModel.DataAnnotations;
namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedUserId { get; set; }

        // Navigation Properties
        public User AssignedUser { get; set; }
        public ICollection<TaskComment> Comments { get; set; }
    }
}
