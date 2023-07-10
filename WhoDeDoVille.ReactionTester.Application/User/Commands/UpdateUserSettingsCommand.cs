

namespace WhoDeDoVille.ReactionTester.Application.User.Commands;

/// <summary>
/// User settings update.
/// </summary>
public class UpdateUserSettingsCommand : IRequest<bool>
{
    public string? ID { get; set; }
    public string? Color1 { get; set; }
    public string? Color2 { get; set; }
    public string? Color3 { get; set; }
    public int? DifficultyLevel { get; set; }
    public bool? IsAi { get; set; }
    public int? Music { get; set; }
    public int? Sound { get; set; }

    public class UpdateUserCommandHandler : ApplicationBase, IRequestHandler<UpdateUserSettingsCommand, bool>
    {
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
            : base(userRepository: userRepository, mapper: mapper) { }

        public async Task<bool> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await UserRepository.GetItemAsync(request.ID);
            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.User), request.ID);
            }

            entity.Color1 = request.Color1;
            entity.Color2 = request.Color2;
            entity.Color3 = request.Color3;
            entity.DifficultyLevel = request.DifficultyLevel;
            entity.IsAi = request.IsAi;
            entity.Music = request.Music;
            entity.Sound = request.Sound;

            await UserRepository.UpdateItemAsync(request.ID, entity);

            return true;
        }
    }
}

