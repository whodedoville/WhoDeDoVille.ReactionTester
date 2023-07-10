namespace WhoDeDoVille.ReactionTester.Domain.Exceptions;

public class FailedCreationException : ApplicationException
{
    public FailedCreationException(string message)
        : base("Failed Creation", message)
    {
    }
}
