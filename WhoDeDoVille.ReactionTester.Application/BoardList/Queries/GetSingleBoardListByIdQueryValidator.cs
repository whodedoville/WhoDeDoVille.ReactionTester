namespace WhoDeDoVille.ReactionTester.Application.BoardList.Queries;

public class GetSingleBoardListByIdQueryValidator : AbstractValidator<GetSingleBoardListByIdQuery>
{
    public GetSingleBoardListByIdQueryValidator()
    {
        RuleFor(v => v.BoardListId).Matches(ValidationValuesProvider.BoardListIdRegex);
    }
}
