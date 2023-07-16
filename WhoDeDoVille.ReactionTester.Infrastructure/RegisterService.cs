namespace WhoDeDoVille.ReactionTester.Infrastructure;

/// <summary>
/// Register services for Infrastructure.
/// Sets cosmos client options.
/// Sets cosmos client factory.
/// </summary>
public static class RegisterService
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ICosmosDbSettings cosmosDbSettings
        )
    {
        //var client = new CosmosClient(
        //    accountEndpoint: cosmosDbSettings.COSMOS_ENDPOINT!,
        //    authKeyOrResourceToken: cosmosDbSettings.COSMOS_KEY!,
        //    new CosmosClientOptions()
        //    {
        //        AllowBulkExecution = true,
        //    }
        //);

        var client = new CosmosClient(
            connectionString: cosmosDbSettings.CosmosReactiontesterConnectionString,
            new CosmosClientOptions()
            {
                AllowBulkExecution = true,
            }
        );
        var cosmosDbClientFactory = new CosmosDbContainerFactory(
            client,
            cosmosDbSettings.Database!,
            cosmosDbSettings.RunInitiationCheck!);
        services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);



        return services;
    }
}
