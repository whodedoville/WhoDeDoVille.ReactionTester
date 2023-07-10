namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Board Directions x,y coords start and end.
/// </summary>
[Serializable]
public class BoardDirectionsCoordsEntity
{
    [JsonProperty("startx")]
    public int StartX { get; set; }

    [JsonProperty("starty")]
    public int StartY { get; set; }

    [JsonProperty("endx")]
    public int EndX { get; set; }

    [JsonProperty("endy")]
    public int EndY { get; set; }
}
