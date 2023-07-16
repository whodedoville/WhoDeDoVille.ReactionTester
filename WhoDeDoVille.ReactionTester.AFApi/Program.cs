/// <summary>
///     Configuration settings from settings.json file.
/// </summary>
var configuration = ConfigurationSettings.GetConfigurationSettings();

/// <summary>
///     Setup dependency injection.
/// </summary>
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(workerApplication =>
    {
        workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddSingleton(configuration);

        var cosmosDbSettings = new CosmosDbSettings();
        configuration.Bind("ConnectionStrings", cosmosDbSettings);
        services.AddSingleton<ICosmosDbSettings>(cosmosDbSettings);

        services.AddApi();

        services.AddDomainServices();

        services.AddInfrastructure(cosmosDbSettings);

        services.AddApplication();
    })
    .Build();

host.Run();
