namespace ToDoLIst_DAL.Auth
{
    public interface IAuthManager
    {
        Task<AuthData?> LoginAsync(string email, string password);
        Task<AuthData?> RefreshUserTokenAsync(AuthData authData);
    }
}
