namespace WhoDeDoVille.ReactionTester.Application.Common.BaseClass;

/// <summary>
/// Used with mediator to create the handler and mapper.
/// </summary>
public abstract class ApplicationBase
{
    public IMapper Mapper { get; set; }
    public IUserRepository? UserRepository { get; set; }
    public IBoardSequenceRepository? BoardSequenceRepository { get; set; }
    public IBoardRepository? BoardRepository { get; set; }
    public IBoardListRepository? BoardListRepository { get; set; }

    public ApplicationBase() { }

    public ApplicationBase(IUserRepository userRepository, IMapper mapper)
    {
        UserRepository = userRepository;
        Mapper = mapper;
    }

    public ApplicationBase(IBoardSequenceRepository boardSequenceRepository, IMapper mapper)
    {
        BoardSequenceRepository = boardSequenceRepository;
        Mapper = mapper;
    }

    public ApplicationBase(IBoardRepository boardRepository, IMapper mapper)
    {
        BoardRepository = boardRepository;
        Mapper = mapper;
    }

    public ApplicationBase(IBoardListRepository boardListRepository, IMapper mapper)
    {
        BoardListRepository = boardListRepository;
        Mapper = mapper;
    }

    public ApplicationBase(IUserRepository userRepository, IBoardRepository boardRepository, IMapper mapper)
    {
        UserRepository = userRepository;
        BoardRepository = boardRepository;
        Mapper = mapper;
    }
}
