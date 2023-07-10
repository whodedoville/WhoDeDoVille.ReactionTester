using Microsoft.Azure.Cosmos;

namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands;

public class UpdateOrAddBoardSequenceCommand : ICommand<bool>
{
    public string BoardSequenceId { get; set; }
    public int SequenceNumber { get; set; }
    public DateTime CreatedDt { get; set; }

    public class UpdateOrAddBoardSequenceHandler : ApplicationBase, IRequestHandler<UpdateOrAddBoardSequenceCommand, bool>
    {
        public UpdateOrAddBoardSequenceHandler(IBoardSequenceRepository boardSequenceRepository, IMapper mapper)
            : base(boardSequenceRepository: boardSequenceRepository, mapper: mapper) { }

        public async Task<bool> Handle(UpdateOrAddBoardSequenceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                BoardSequenceEntity boardSequenceEntity = new()
                {
                    Id = request.BoardSequenceId,
                    SequenceNumber = request.SequenceNumber,
                    CreatedDt = request.CreatedDt,
                    UpdatedDt = request.CreatedDt
                };

                List<PatchOperation> patchOperations = new()
                {
                    PatchOperation.Replace("/sequence", boardSequenceEntity.SequenceNumber),
                    PatchOperation.Replace("/updatedDt", boardSequenceEntity.UpdatedDt),
                };

                await BoardSequenceRepository.PatchOrCreateSingleItemAsync(boardSequenceEntity, patchOperations);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
