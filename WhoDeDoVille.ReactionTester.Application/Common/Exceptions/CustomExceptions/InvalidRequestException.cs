namespace WhoDeDoVille.ReactionTester.Application.Common.Exceptions.CustomExceptions;

public class InvalidRequestException : Exception
{
    public List<string> Errors { get; }

    public InvalidRequestException(List<string> errors)
    {
        Errors = errors;
    }
}
