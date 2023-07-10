using ApplicationException = WhoDeDoVille.ReactionTester.Domain.Exceptions.ApplicationException;

namespace WhoDeDoVille.ReactionTester.Application.Common.Exceptions;

/// <summary>
///     Default message sent for validation errors.
/// </summary>
public sealed class ValidationException : ApplicationException
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
        : base("Validation Failure", "One or more validation errors occurred")
        => ErrorsDictionary = errorsDictionary;

    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
}