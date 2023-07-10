namespace WhoDeDoVille.ReactionTester.Application.User.Queries;

public class GetAllUserQuery : IRequest<UserVM>
{
    public UserStatus Status { get; set; }
    //public class GetAllUserHandler : ApplicationBase, IRequestHandler<GetAllUserQuery, UserVM>
    //{
    //    public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
    //        : base(userRepository: userRepository, mapper: mapper) { }

    //    public async Task<UserVM> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    //    {
    //        //UserGetAllSpecification specification = new(request.Status);
    //        //var data = await UserRepository.GetItemsAsync(specification);
    //        var data = await UserRepository.GetItemsAsyncByEmail(email);
    //        var res = Mapper.Map(data, new List<UserDTO>());

    //        return await Task.FromResult(new UserVM() { UserList = res });
    //    }
    //}
}