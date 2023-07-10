namespace WhoDeDoVille.ReactionTester.Application.Core.Users.Commands.CreateUser
{
    //internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
    //{
    //    private readonly IDbContext _dbContext;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
    //    /// </summary>
    //    /// <param name="dbContext">The database context.</param>
    //    public CreateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    //    /// <inheritdoc />
    //    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    //    {
    //        Result<TeamName> teamNameResult = TeamName.Create(request.TeamName);
    //        Result<ModelName> modelNameResult = ModelName.Create(request.ModelName);

    //        var result = Result.FirstFailureOrSuccess(teamNameResult, modelNameResult);

    //        if (result.IsFailure)
    //        {
    //            return Result.Failure(result.Error);
    //        }

    //        Maybe<Race> maybeRace = await _dbContext.GetBydIdAsync<Race>(request.RaceId);

    //        if (maybeRace.HasNoValue)
    //        {
    //            return Result.Failure(ValidationErrors.Race.NotFound);
    //        }

    //        Race race = maybeRace.Value;

    //        if (race.Status != RaceStatus.Pending)
    //        {
    //            return Result.Failure(DomainErrors.Race.AlreadyStarted);
    //        }

    //        var vehicleSubtype = (VehicleSubtype)request.VehicleSubtype;

    //        var vehicle = new Vehicle(race, teamNameResult.Value, modelNameResult.Value, request.ManufacturingDate, vehicleSubtype);

    //        _dbContext.Insert(vehicle);

    //        await _dbContext.SaveChangesAsync(cancellationToken);

    //        return Result.Success();
    //    }
    //}
}
