using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.TaskList
{
    public class CreateTaskListDto
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int GroupId { get; set; }
    }
}
