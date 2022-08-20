using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.AppUser
{
    public class UpdateUserPasswordDTO : IModel<Guid>
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
