namespace WhoDeDoVille.ReactionTester.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}