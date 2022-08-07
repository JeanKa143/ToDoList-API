using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.AppUser
{
    public class DeleteUserDTO
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
