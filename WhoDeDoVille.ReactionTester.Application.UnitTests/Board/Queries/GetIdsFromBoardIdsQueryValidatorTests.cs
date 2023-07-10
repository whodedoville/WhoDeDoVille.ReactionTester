using WhoDeDoVille.ReactionTester.Application.Board.Queries;
using WhoDeDoVille.ReactionTester.Domain.Common.Config;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.Board.Queries;

public class GetIdsFromBoardIdsQueryValidatorTests
{
    private readonly GetIdsFromBoardIdsQueryValidator _getIdsFromBoardIdsQueryValidator;
    private int? _stepMin { get; set; } = null;
    private int? _stepMax { get; set; } = null;
    private int? _shapeMin { get; set; } = null;
    private int? _shapeMax { get; set; } = null;

    public GetIdsFromBoardIdsQueryValidatorTests()
    {
        _getIdsFromBoardIdsQueryValidator = new GetIdsFromBoardIdsQueryValidator();
        foreach (var dlS in BoardConfig.DifficultyLevelSettings)
        {
            if (_stepMin == null || _stepMin > dlS.StepsMin) _stepMin = dlS.StepsMin;
            if (_stepMax == null || _stepMax < dlS.StepsMax) _stepMax = dlS.StepsMax;
            if (_shapeMin == null || _shapeMin > dlS.ShapesMin) _shapeMin = dlS.ShapesMin;
            if (_shapeMax == null || _shapeMax < dlS.ShapesMax) _shapeMax = dlS.ShapesMax;
        }
    }

    [Fact]
    public void Given_GetIdsFromBoardIdsQueryValidator_Is_Valid()
    {
        // Arrange
        GetIdsFromBoardIdsQuery getIdsFromBoardIdsQuery = new GetIdsFromBoardIdsQuery
        {
            BoardIdList = new List<string>() {
                $"{_stepMin}:{_shapeMin}:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39",
                $"{_stepMax}:{_shapeMax}:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39"
            }
        };

        // Act
        var response = _getIdsFromBoardIdsQueryValidator.TestValidate(getIdsFromBoardIdsQuery);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.BoardIdList);
    }

    [Theory]
    [InlineData("1:15:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39")]
    [InlineData("3:22:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39")]
    [InlineData("22:3:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39")]
    [InlineData("0:22:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39")]
    [InlineData("3:3:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d")]
    [InlineData("3:3:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39v")]
    public void Given_GetIdsFromBoardIdsQueryValidator_Is_Invalid(string BoardId)
    {
        // Arrange
        GetIdsFromBoardIdsQuery getIdsFromBoardIdsQuery = new GetIdsFromBoardIdsQuery
        {
            BoardIdList = new List<string>() { BoardId }
        };

        // Act
        var response = _getIdsFromBoardIdsQueryValidator.TestValidate(getIdsFromBoardIdsQuery);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.BoardIdList);
    }
}
