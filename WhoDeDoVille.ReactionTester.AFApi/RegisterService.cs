namespace WhoDeDoVille.ReactionTester.AFApi
{
    public static class RegisterService
    {
        /// <summary>
        ///     Azure Function API services.
        /// </summary>
        /// <returns>services</returns>
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBoardSequenceRepository, BoardSequenceRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IBoardListRepository, BoardListRepository>();

            return services;
        }
    }
}
