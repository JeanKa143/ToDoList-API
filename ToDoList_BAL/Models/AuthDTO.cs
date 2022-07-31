using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models
{
    public class AuthDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
