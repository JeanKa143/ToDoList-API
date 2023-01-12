using System.ComponentModel.DataAnnotations;
using ToDoList_BAL.Validations;

namespace ToDoList_BAL.Models.AppUser
{
    public class UpdateUserPasswordDto : IModelDto<Guid>
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [NotEqual(nameof(OldPassword), ErrorMessage = "The new password must be different from the old password.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
