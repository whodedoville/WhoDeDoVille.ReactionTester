using Microsoft.Azure.Cosmos;

namespace WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

public class GenerateBoardContainerCommand : ICommand<ContainerResponse>
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
