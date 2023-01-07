using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.AppUser
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
