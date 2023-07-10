namespace WhoDeDoVille.ReactionTester.Domain.Exceptions;

/// <summary>
/// Generic bad request exception message.
/// </summary>
public class BadRequestException : ApplicationException
{
    public BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}