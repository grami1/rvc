namespace RvcApp.Exceptions;

public class RvcException : Exception
{
    public RvcException()
    {
    }

    public RvcException(string message) : base(message)
    {
    }
}