using Microsoft.Azure.Cosmos;

namespace WhoDeDoVille.ReactionTester.Application.BoardList.Commands.Generate;

public class GenerateBoardListContainerCommand : ICommand<ContainerResponse>
{
    public bool CheckInitialized { get; set; } = true;

    public class GenerateBoardListContainerHandler : ApplicationBase, IRequestHandler<GenerateBoardListContainerCommand, ContainerResponse>
    {
        public GenerateBoardListContainerHandler(IBoardListRepository boardListRepository, IMapper mapper)
            : base(boardListRepository: boardListRepository, mapper: mapper) { }

        public async Task<ContainerResponse?> Handle(GenerateBoardListContainerCommand request, CancellationToken cancellationToken)
        {
            var containerSettingsInfo = BoardListRepository.GetContainerSettingsInfo();

            if (request.CheckInitialized == false ||
                (request.CheckInitialized == true && containerSettingsInfo.Initialized == false))
            {
                return await BoardListRepository.GenerateContainerWithReturn();
            }

            return null;
        }
    }
}