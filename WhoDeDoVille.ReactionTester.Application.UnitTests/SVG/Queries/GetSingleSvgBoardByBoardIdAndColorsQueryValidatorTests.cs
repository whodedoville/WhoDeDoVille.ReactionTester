using WhoDeDoVille.ReactionTester.Application.SVG.Queries;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.SVG.Queries;

public class GetSingleSvgBoardByBoardIdAndColorsQueryValidatorTests
{
    private readonly GetSingleSvgBoardByBoardIdAndColorsQueryValidator _getSingleSvgBoardByBoardIdAndColorsQueryValidator;

    public GetSingleSvgBoardByBoardIdAndColorsQueryValidatorTests()
    {
        _getSingleSvgBoardByBoardIdAndColorsQueryValidator = new GetSingleSvgBoardByBoardIdAndColorsQueryValidator();
    }

    [Theory]
    [InlineData("3:4:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39", "Aa0000", "bB0cD2", "fff")]
    public void Given_GetSingleSvgBoardByBoardIdAndColorsQueryValidator_Is_Valid(
        string BoardId, string Color1, string Color2, string Color3)
    {
        // Arrange
        var getSingleSvgBoardByBoardIdAndColorsQuery = new GetSingleSvgBoardByBoardIdAndColorsQuery
        {
            BoardId = BoardId,
            Color1 = Color1,
            Color2 = Color2,
            Color3 = Color3
        };

        // Act
        var response = _getSingleSvgBoardByBoardIdAndColorsQueryValidator.TestValidate(getSingleSvgBoardByBoardIdAndColorsQuery);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.BoardId);
        response.ShouldNotHaveValidationErrorFor(x => x.Color1);
        response.ShouldNotHaveValidationErrorFor(x => x.Color2);
        response.ShouldNotHaveValidationErrorFor(x => x.Color3);
    }

    [Theory]
    [InlineData("1:20:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39", "Ga0000", "bB0c", "ff")]
    public void Given_GetSingleSvgBoardByBoardIdAndColorsQueryValidator_Is_Invalid(
        string BoardId, string Color1, string Color2, string Color3)
    {
        // Arrange
        var getSingleSvgBoardByBoardIdAndColorsQuery = new GetSingleSvgBoardByBoardIdAndColorsQuery
        {
            BoardId = BoardId,
            Color1 = Color1,
            Color2 = Color2,
            Color3 = Color3
        };

        // Act
        var response = _getSingleSvgBoardByBoardIdAndColorsQueryValidator.TestValidate(getSingleSvgBoardByBoardIdAndColorsQuery);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.BoardId);
        response.ShouldHaveValidationErrorFor(x => x.Color1);
        response.ShouldHaveValidationErrorFor(x => x.Color2);
        response.ShouldHaveValidationErrorFor(x => x.Color3);
    }
}
