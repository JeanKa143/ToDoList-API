namespace ToDoList_BAL.Exceptions
{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(string name, object key) : base($"User does not have access rights to the {name} with id ({key})")
        {
        }
    }
}
