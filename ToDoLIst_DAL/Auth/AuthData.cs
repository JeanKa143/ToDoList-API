﻿namespace ToDoLIst_DAL.Auth
{
    public class AuthData
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
