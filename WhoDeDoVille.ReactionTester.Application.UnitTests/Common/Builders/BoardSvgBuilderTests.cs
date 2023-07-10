using WhoDeDoVille.ReactionTester.Domain.Common.Config;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.Common.Builders;

public class BoardSvgBuilderTests
{
    private readonly Mock<IBoardRepository> _mockIBoardRepository;
    //private readonly IMapper _mapper;
    public BoardSvgBuilderTests()
    {
        _mockIBoardRepository = MockIBoardRepository.GetMock();
        //_mapper = MappingsForTests.GetMapper();
    }

    //public IMapper GetMapper()
    //{
    //    var mappingProfile = new MappingProfile();
    //    var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
    //    return new Mapper(configuration);
    //}

    [Theory]
    [InlineData("3:3:7ad2337a56d22741285b57ac3813f10c0b3432d3c084a4e16caf4537b6e45387")]
    public void WhenCreatingNewBoardSvgBuilder_CreateSvgBoardDoc(string? id)
    {
        var responseData = _mockIBoardRepository.Object.GetItemAsync(id);

        Assert.NotNull(responseData);

        var svgBoardBuilder = new BoardSvgBuilder(responseData.Result, BoardConfig.Color1, BoardConfig.Color2, BoardConfig.Color3);

        Assert.NotNull(svgBoardBuilder);

        var svgBoardDoc = svgBoardBuilder.SvgBoardDoc;

        Assert.NotNull(svgBoardDoc);
    }
}
