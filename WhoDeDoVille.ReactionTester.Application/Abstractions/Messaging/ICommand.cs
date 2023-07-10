namespace WhoDeDoVille.ReactionTester.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}