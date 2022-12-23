using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskListGroup
{
    public class UpdateTaskListGroupDto : BaseTaskListGroupDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
