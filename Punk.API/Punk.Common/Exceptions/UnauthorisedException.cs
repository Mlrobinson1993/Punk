namespace Punk.Common.Exceptions;

public class UnauthorisedException : Exception
{
    public UnauthorisedException(string message) : base(message)
    {
    }
}