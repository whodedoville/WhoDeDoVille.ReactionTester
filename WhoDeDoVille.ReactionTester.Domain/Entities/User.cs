namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// User Entity.
/// </summary>
public class User : BaseEntity
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public UserStatus? Status { get; set; }
    [JsonProperty(PropertyName = "UserType")]
    public string? UserType { get; set; }
    public string? Color1 { get; set; }
    public string? Color2 { get; set; }
    public string? Color3 { get; set; }
    public int? DifficultyLevel { get; set; }
    public bool? IsAi { get; set; }
    public int? Music { get; set; }
    public int? Sound { get; set; }
}