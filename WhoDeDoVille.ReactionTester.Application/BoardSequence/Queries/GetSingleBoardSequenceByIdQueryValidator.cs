namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.Queries;

public class GetSingleBoardSequenceByIdQueryValidator : AbstractValidator<GetSingleBoardSequenceByIdQuery>
{
    public GetSingleBoardSequenceByIdQueryValidator()
    {
        RuleFor(v => v.BoardSequenceId).Matches(ValidationValuesProvider.BoardSequenceIdRegex);
    }
}
