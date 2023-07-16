using Microsoft.Azure.Cosmos;

namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands.Generate;

public class GenerateBoardSequenceContainerCommand : ICommand<ContainerResponse>
{
    public bool CheckInitialized { get; set; } = true;

    public class GenerateBoardSequenceContainerHandler : ApplicationBase, IRequestHandler<GenerateBoardSequenceContainerCommand, ContainerResponse>
    {
        public GenerateBoardSequenceContainerHandler(IBoardSequenceRepository boardSequenceRepository, IMapper mapper)
            : base(boardSequenceRepository: boardSequenceRepository, mapper: mapper) { }

        public async Task<ContainerResponse?> Handle(GenerateBoardSequenceContainerCommand request, CancellationToken cancellationToken)
        {
            var containerSettingsInfo = BoardSequenceRepository.GetContainerSettingsInfo();

            if (request.CheckInitialized == false ||
                (request.CheckInitialized == true && containerSettingsInfo.IsInitialized == false))
            {
                return await BoardSequenceRepository.GenerateContainerWithReturn();
            }

            return null;
        }
    }
}
