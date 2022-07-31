namespace ToDoList_BAL.Exceptions
{
    public class LoginException : ApplicationException
    {
        public LoginException() : base("Invalid email or password")
        {
        }
    }
}
