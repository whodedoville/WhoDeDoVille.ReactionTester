using AutoMapper;
using WhoDeDoVille.ReactionTester.Application.Common.Mappings;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.Common.Mappings;

public class MappingsForTests
{
    public static IMapper GetMapper()
    {
        var mappingProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
        return new Mapper(configuration);
    }
}
