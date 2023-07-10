using System.Net;

namespace WhoDeDoVille.ReactionTester.Application.Common.Interfaces;

public interface IErrorResponse
{
    public string? Title { get; set; }
    public HttpStatusCode Status { get; set; }
    public string? Detail { get; set; }
    public DateTime CreatedAt { get; set; }
    public IReadOnlyDictionary<string, string[]>? Errors { get; set; }
}
