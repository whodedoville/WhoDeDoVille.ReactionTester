namespace WhoDeDoVille.ReactionTester.Application.User.Commands;

/// <summary>
/// Single user creation.
/// </summary>
public class AddUserCommand : ICommand<UserSettingsDTO>
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public UserType? UserType { get; set; }
    public string? Color1 { get; set; }
    public string? Color2 { get; set; }
    public string? Color3 { get; set; }
    public int? DifficultyLevel { get; set; }
    public bool? IsAi { get; set; }
    public int? Music { get; set; }
    public int? Sound { get; set; }

    public class AddNewUserHandler : ApplicationBase, IRequestHandler<AddUserCommand, UserSettingsDTO>
    {
        public AddNewUserHandler(IUserRepository userRepository, IMapper mapper)
            : base(userRepository: userRepository, mapper: mapper) { }

        public async Task<UserSettingsDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.User entity = new()
            {
                Email = request.Email,
                Username = request.Username,
                Status = UserStatus.PENDING,
                UserType = request.UserType.ToString() != "" ? request.UserType.ToString() : "user",
                Color1 = "ff0000",
                Color2 = "2129a3",
                Color3 = "28b11b",
                DifficultyLevel = 1,
                IsAi = false,
                Music = 100,
                Sound = 100
            };

            await UserRepository.AddItemAsync(entity);

            var res = Mapper.Map(entity, new UserSettingsDTO());
            return await Task.FromResult(res);
        }
    }
}
