using Microsoft.Azure.Cosmos;
using WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

namespace WhoDeDoVille.ReactionTester.Application.ReactionTester.Commands.Generate;

public class GenerateReactionTesterDatabaseCommand : ICommand<ContainerResponse>
{
    public bool CheckInitialized { get; set; } = true;

    public class GenerateBoardContainerHandler : ApplicationBase, IRequestHandler<GenerateBoardContainerCommand, ContainerResponse>
    {
        public GenerateBoardContainerHandler(IBoardRepository boardRepository, IMapper mapper)
            : base(boardRepository: boardRepository, mapper: mapper) { }

        public async Task<ContainerResponse?> Handle(GenerateBoardContainerCommand request, CancellationToken cancellationToken)
        {
            var containerSettingsInfo = BoardRepository.GetContainerSettingsInfo();

            if (request.CheckInitialized == false ||
                (request.CheckInitialized == true && containerSettingsInfo.Initialized == false))
            {
                return await BoardRepository.GenerateContainerWithReturn();
            }

            return null;
        }
    }
}
