namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Board Difficulty.
/// </summary>
public class BoardDifficultyLevel
{
    // TODO: Change string[] to List if possible
    public int Difficulty { get; set; }
    public string[] DirectionsPosition { get; set; }
    public string[] DirectionsRead { get; set; }
    public string[] DirectionsShape { get; set; }
    public int StepsMin { get; set; }
    public int StepsMax { get; set; }
    public bool StepsRepeats { get; set; }
    public int ShapesMin { get; set; }
    public int ShapesMax { get; set; }
}
