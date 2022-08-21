using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.AppUser
{
    public class DeleteUserDto : IModelDto<Guid>
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
