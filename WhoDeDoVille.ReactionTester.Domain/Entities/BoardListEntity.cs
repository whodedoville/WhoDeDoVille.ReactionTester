namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Board List Entity.
/// List of board hashes.
/// </summary>
public class BoardListEntity : BaseEntity
{
    [JsonProperty("difficulty")]
    public int Difficulty { get; set; }

    [JsonProperty("sequencenumber")]
    public string SequenceNumber { get; set; }

    [JsonProperty("createdDT")]
    public DateTime CreatedDt { get; set; }

    [JsonProperty("boardIdList")]
    public List<string>? BoardIdList { get; set; } = new List<string>();
}
