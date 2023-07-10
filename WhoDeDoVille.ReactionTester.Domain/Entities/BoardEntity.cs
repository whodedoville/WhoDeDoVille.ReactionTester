namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Board Entity
/// </summary>
public class BoardEntity : BaseEntity
{
    [JsonProperty("difficultysequence")]
    public string DifficultySequence { get; set; }

    [JsonProperty("partitionkey")]
    public string PartitionKey { get; set; }

    [JsonProperty("createdDT")]
    public DateTime CreatedDt { get; set; }

    [JsonProperty("answer")]
    public List<BoardAnswerEntity> Answer { get; set; } = new List<BoardAnswerEntity>();

    [JsonProperty("directionsCoords")]
    public BoardDirectionsCoordsEntity DirectionsCoords { get; set; } = new BoardDirectionsCoordsEntity();

    [JsonProperty("shapes")]
    public List<BoardShapeEntity> Shapes { get; set; } = new List<BoardShapeEntity>();

    #region FillBoardIdAndKey

    /// <summary>
    /// Fill ID and partition key based on parameters.
    /// </summary>
    public void FillBoardIdAndPartitionKey()
    {
        var hash = GenerateBoardHash();

        Id = $"{Answer.Count}:{Shapes.Count}:{hash}";
        PartitionKey = $"{Answer.Count}:{Shapes.Count}:{hash.Substring(0, 6)}";
    }

    /// <summary>
    /// Hashes answer, directions, shapes.
    /// </summary>
    /// <returns>Hashed string</returns>
    private string GenerateBoardHash()
    {
        string answerString = JsonConvert.SerializeObject(Answer);
        string directionsCoordsString = JsonConvert.SerializeObject(DirectionsCoords);
        string shapesString = JsonConvert.SerializeObject(Shapes);
        string toHash = $"{answerString}{directionsCoordsString}{shapesString}";

        return SharedProvider.ComputeSha256Hash(toHash);
    }
    #endregion
}
