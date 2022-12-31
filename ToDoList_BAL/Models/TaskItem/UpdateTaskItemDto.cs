using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskItem
{
    public class UpdateTaskItemDto : BaseTaskItemDto, IModelDto<int>
    {
        [Required]
        public int Id { get; set; }
    }
}
