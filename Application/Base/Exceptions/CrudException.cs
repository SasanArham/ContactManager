namespace Application.Base.Exceptions
{
    public class CrudException : Exception
    {
        public CrudException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
