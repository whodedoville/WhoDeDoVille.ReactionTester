namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Baord Answer x,y coordinates
/// </summary>
[Serializable]
public class BoardAnswerEntity
{
    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }
}
