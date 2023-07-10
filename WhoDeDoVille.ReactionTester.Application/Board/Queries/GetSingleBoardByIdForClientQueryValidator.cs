namespace WhoDeDoVille.ReactionTester.Application.Board.Queries;

public class GetSingleBoardByIdForClientQueryValidator : AbstractValidator<GetSingleBoardByIdForClientQuery>
{
    public GetSingleBoardByIdForClientQueryValidator()
    {
        RuleFor(v => v.BoardId).Matches(ValidationValuesProvider.BoardIdRegex);
    }
}
