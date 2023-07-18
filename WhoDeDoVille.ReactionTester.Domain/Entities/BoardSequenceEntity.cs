namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class BoardSequenceEntity : BaseEntity
{
    [JsonProperty("sequencenumber")]
    public string SequenceNumber { get; set; }

    [JsonProperty("createdDT")]
    public DateTime CreatedDt { get; set; }

    [JsonProperty("updatedDt")]
    public DateTime UpdatedDt { get; set; }

    [JsonProperty("partitionkey")]
    public string Partitionkey { get; set; } = "Board";
}
