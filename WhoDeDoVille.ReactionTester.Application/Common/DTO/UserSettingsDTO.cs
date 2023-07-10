namespace WhoDeDoVille.ReactionTester.Application.Common.DTO
{
    /// <summary>
    /// User settings that are sent to the client.
    /// </summary>
    public class UserSettingsDTO : IMapFrom<Domain.Entities.User>
    {
        public string? Color1 { get; set; }
        public string? Color2 { get; set; }
        public string? Color3 { get; set; }
        public int? DifficultyLevel { get; set; }
        public bool? IsAi { get; set; }
        public int? Music { get; set; }
        public int? Sound { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.User, UserSettingsDTO>();
            profile.CreateMap<UserSettingsDTO, Domain.Entities.User>();
        }
    }
}
