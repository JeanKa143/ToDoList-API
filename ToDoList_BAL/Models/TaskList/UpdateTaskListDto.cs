using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskList
{
    public class UpdateTaskListDto : IModelDto<int>
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
