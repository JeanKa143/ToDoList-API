﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.Auth
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
