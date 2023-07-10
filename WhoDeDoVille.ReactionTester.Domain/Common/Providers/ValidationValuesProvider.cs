namespace WhoDeDoVille.ReactionTester.Domain.Common.Providers;

public static class ValidationValuesProvider
{
    //public const string BoardIdRegex = "\\d{1,2}:\\d{1,2}:[a-z0-9]{63,64}";
    public const string BoardIdRegex = "^(?:[3-9]|[1][0-9]{1}):(?:[3-9]|[1][0-9]{1}):[a-z0-9]{63,64}$";
    public const string BoardListIdRegex = "^(?:[1-9]):(?:[0-9]+)$";
    public const string BoardSequenceIdRegex = "^Board:[1-9]$";
    public const string ColorStringRegex = "^([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
}
