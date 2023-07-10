using Newtonsoft.Json;
using System.Net;

namespace WhoDeDoVille.ReactionTester.Application.Common.Error;

/// <summary>
/// Error structure that is used to send back to client.
/// </summary>
public class ErrorResponse : IErrorResponse
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("status")]
    public HttpStatusCode Status { get; set; }

    [JsonProperty("detail")]
    public string? Detail { get; set; }
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
    [JsonProperty("errors")]
    public IReadOnlyDictionary<string, string[]>? Errors { get; set; }
}