using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskListGroup
{
    public class CreateTaskListGroupDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid OwnerId { get; set; }
    }
}
