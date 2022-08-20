using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.AppUser
{
    public class DeleteUserDTO : IModel<Guid>
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
