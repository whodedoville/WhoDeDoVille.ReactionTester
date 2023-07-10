namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Shape x,y coords, shape, color;
/// </summary>
[Serializable]
public class BoardShapeEntity
{
    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }

    [JsonProperty("shape")]
    public ShapesEnum Shape { get; set; }

    [JsonProperty("color")]
    public ColorsEnum Color { get; set; }
}
