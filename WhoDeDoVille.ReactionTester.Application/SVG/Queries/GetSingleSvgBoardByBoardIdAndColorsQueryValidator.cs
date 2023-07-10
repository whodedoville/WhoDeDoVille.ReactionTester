namespace WhoDeDoVille.ReactionTester.Application.SVG.Queries;

public class GetSingleSvgBoardByBoardIdAndColorsQueryValidator : AbstractValidator<GetSingleSvgBoardByBoardIdAndColorsQuery>
{
    public GetSingleSvgBoardByBoardIdAndColorsQueryValidator()
    {
        RuleFor(v => v.BoardId).Matches(ValidationValuesProvider.BoardIdRegex);
        RuleFor(v => v.Color1).Matches(ValidationValuesProvider.ColorStringRegex);
        RuleFor(v => v.Color2).Matches(ValidationValuesProvider.ColorStringRegex);
        RuleFor(v => v.Color3).Matches(ValidationValuesProvider.ColorStringRegex);
    }
}
