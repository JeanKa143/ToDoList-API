using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskListGroup
{
    public class UpdateTaskListGroupDto : IModelDto<int>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
