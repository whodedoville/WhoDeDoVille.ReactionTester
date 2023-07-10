namespace WhoDeDoVille.ReactionTester.Application.Board.Queries;

public class GetIdsFromBoardIdsQueryValidator : AbstractValidator<GetIdsFromBoardIdsQuery>
{
    public GetIdsFromBoardIdsQueryValidator()
    {
        RuleForEach(v => v.BoardIdList).Matches(ValidationValuesProvider.BoardIdRegex);
    }
}
