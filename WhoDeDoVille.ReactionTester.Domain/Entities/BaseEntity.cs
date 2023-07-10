namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Base Entity
/// </summary>
public abstract class BaseEntity
{
    [JsonProperty(PropertyName = "id")]
    public virtual string? Id { get; set; }
}
