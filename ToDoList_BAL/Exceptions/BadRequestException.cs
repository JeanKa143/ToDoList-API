namespace ToDoList_BAL.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException() : base()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }
    }
}
