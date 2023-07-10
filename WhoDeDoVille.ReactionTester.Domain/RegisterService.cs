using Microsoft.Extensions.DependencyInjection;

namespace WhoDeDoVille.ReactionTester.Domain;

/// <summary>
/// Register services for Domain.
/// </summary>
public static class RegisterService
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        //services.AddScoped<IBoardListGeneratorEntity, BoardListGeneratorEntity>();

        return services;
    }
}