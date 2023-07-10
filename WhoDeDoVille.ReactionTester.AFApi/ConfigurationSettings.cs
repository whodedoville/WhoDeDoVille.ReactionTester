namespace WhoDeDoVille.ReactionTester.AFApi
{
    public static partial class ConfigurationSettings
    {
        /// <summary>
        /// Gets local settings
        /// </summary>
        public static IConfigurationRoot GetConfigurationSettings()
        {
            var returnConfiguration = new ConfigurationBuilder()
                 .SetBasePath(Environment.CurrentDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

            return returnConfiguration;
        }
    }
}
