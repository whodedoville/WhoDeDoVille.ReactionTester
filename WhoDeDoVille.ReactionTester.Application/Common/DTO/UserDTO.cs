namespace WhoDeDoVille.ReactionTester.Application.Common.DTO;

/// <summary>
/// General User information
/// </summary>
public class UserDTO : IMapFrom<Domain.Entities.User>
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public UserStatus? Status { get; set; }
    public UserType? UserType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.User, UserDTO>();
        profile.CreateMap<UserDTO, Domain.Entities.User>();
    }
}
