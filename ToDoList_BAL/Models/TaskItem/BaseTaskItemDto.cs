using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskItem
{
    public class BaseTaskItemDto
    {
        [Required]
        public int TaskListId { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime? DueDate { get; set; }
        [Required]
        public bool IsImportant { get; set; }
        [Required]
        public bool IsInMyDay { get; set; }
    }
}
