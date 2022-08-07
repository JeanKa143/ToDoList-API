namespace ToDoList_BAL.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key) : base($"{name} with id ({key}) was not found")
        {
        }
    }
}
