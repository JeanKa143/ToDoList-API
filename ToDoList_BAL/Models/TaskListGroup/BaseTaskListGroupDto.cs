using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskListGroup
{
    public abstract class BaseTaskListGroupDto : IModelDto<int>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
    }
}
