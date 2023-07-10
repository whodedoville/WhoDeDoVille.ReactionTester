namespace WhoDeDoVille.ReactionTester.Domain.Common.Config;

/// <summary>
/// Board default values used for building the boards.
/// </summary>
public static class BoardConfig
{
    public static readonly int Blocksize = 24; //Size of each grid space
    public static readonly int MarginSpace = 2; //Open space on the margins
    public static readonly int WidthGridCount = 20;
    public static readonly int HeightGridCount = 24;
    public static readonly int DefaultBoardCount = 100;
    public static readonly string Color1 = "ff0000"; //User default color
    public static readonly string Color2 = "2129a3"; //User default color
    public static readonly string Color3 = "28b11b"; //User default color

    // TODO: Change string values to Enum values. This will also need to be changed where the values are used.
    public static readonly List<BoardDifficultyLevel> DifficultyLevelSettings = new()
    {
        new BoardDifficultyLevel() {
            Difficulty = 1,
            DirectionsPosition = new string[] { "beginner" },
            DirectionsShape = new string[] { "horizontal" },
            DirectionsRead = new string[] { "left to right" },
            StepsMin = 3,
            StepsMax = 4,
            StepsRepeats = false,
            ShapesMin = 3,
            ShapesMax = 4
        },
        new BoardDifficultyLevel() {
            Difficulty = 2,
            DirectionsPosition = new string[] { "top" },
            DirectionsShape = new string[] { "horizontal" },
            DirectionsRead = new string[] { "left to right" },
            StepsMin = 3,
            StepsMax = 6,
            StepsRepeats = false,
            ShapesMin = 3,
            ShapesMax = 6
        },
        new BoardDifficultyLevel() {
            Difficulty = 3,
            DirectionsPosition = new string[] { "top" },
            DirectionsShape = new string[] { "horizontal" },
            DirectionsRead = new string[] { "left to right" },
            StepsMin = 4,
            StepsMax = 6,
            StepsRepeats = false,
            ShapesMin = 4,
            ShapesMax = 8
        },
        new BoardDifficultyLevel() {
            Difficulty = 4,
            DirectionsPosition = new string[] {"top", "bottom", "left", "right" },
            DirectionsShape = new string[] { "horizontal", "vertical" },
            DirectionsRead = new string[] {"left to right", "right to left" },
            StepsMin = 4,
            StepsMax = 6,
            StepsRepeats = false,
            ShapesMin = 4,
            ShapesMax = 8
        },
        new BoardDifficultyLevel() {
            Difficulty = 5,
            DirectionsPosition = new string[] { "anywhere" },
            DirectionsShape = new string[] { "horizontal", "vertical" },
            DirectionsRead = new string[] { "left to right", "right to left" },
            StepsMin = 4,
            StepsMax = 6,
            StepsRepeats = true,
            ShapesMin = 4,
            ShapesMax = 8
        },
        new BoardDifficultyLevel() {
            Difficulty = 6,
            DirectionsPosition = new string[] { "anywhere" },
            DirectionsShape = new string[] { "horizontal", "vertical", "diagonal" },
            DirectionsRead = new string[] { "left to right", "right to left" },
            StepsMin = 4,
            StepsMax = 6,
            StepsRepeats = true,
            ShapesMin = 4,
            ShapesMax = 8
        },
        new BoardDifficultyLevel() {
            Difficulty = 7,
            DirectionsPosition = new string[] { "anywhere" },
            DirectionsShape = new string[] { "horizontal", "vertical", "diagonal" },
            DirectionsRead = new string[] { "left to right", "right to left" },
            StepsMin = 8,
            StepsMax = 10,
            StepsRepeats = true,
            ShapesMin = 8,
            ShapesMax = 14
        }
    };
}
