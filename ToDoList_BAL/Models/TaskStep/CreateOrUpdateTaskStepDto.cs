using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskStep
{
    public class CreateOrUpdateTaskStepDto : IModelDto<int>
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public bool IsDone { get; set; }
    }
}
