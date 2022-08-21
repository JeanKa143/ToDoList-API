using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.Auth
{
    public class AuthDTO
    {
        [Required]
        public Guid UserId { get; set; } = Guid.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
