using WhoDeDoVille.ReactionTester.Application.Board.Queries;

namespace WhoDeDoVille.ReactionTester.Application.Common.Builders;

/// <summary>
/// Build a list of BoardEntity and BoardListEntity based on Difficulty Level.
/// </summary>
public class BoardAndBoardListBuilder : IBoardAndBoardListBuilder
{
    private int _difficultyLevel { get; }
    private string _sequenceNumber { get; }
    private int _boardCount { get; }
    private DateTime _dateTime { get; } = DateTime.UtcNow;
    public List<BoardBuilder> BoardBuilderList { get; set; } = new List<BoardBuilder>();

    private readonly ISender _sender;

    /// <summary>
    /// Empty builder;
    /// </summary>
    public BoardAndBoardListBuilder() { }

    /// <summary>
    /// Initializes builder with needed values
    /// </summary>
    /// <param name="DifficultyLevel">Board Difficulty Level</param>
    /// <param name="SequenceNumber">
    /// Needs to be a unique number. Supposed to be in sequence by difficulty.
    /// </param>
    /// <param name="BoardCount">Minimum amount of boards to generate</param>
    /// <param name="sender">Mediatr sender</param>
    /// <returns>Boolean if boards were successfully created</returns>
    public BoardAndBoardListBuilder(
        int DifficultyLevel,
        string SequenceNumber,
        int BoardCount,
        ISender sender)
    {
        //TODO: Create tests. Maybe change some private calls to Internal.
        _difficultyLevel = DifficultyLevel;
        _sequenceNumber = SequenceNumber;
        _boardCount = BoardCount;
        _sender = sender;
    }

    /// <summary>
    /// Generates BoardEntity and BoardListEntity based on initialized values.
    /// </summary>
    public async Task GenerateUniqueBoards()
    {
        BoardBuilderFiller();

        await RemoveDuplicatesThenValidateLoop();
    }

    #region BoardBuilderFiller
    /// <summary>
    /// Generates all possible board types based on board difficulty level.
    /// Difficulty level settings <see cref="BoardConfig"/>.
    /// BoardEntity randomly generated after initialization <see cref="BoardEntity"/>.
    /// </summary>
    private void BoardBuilderFiller()
    {
        var difficultyLevelData = BoardConfig.DifficultyLevelSettings[_difficultyLevel - 1];
        var boardBuilderList = new List<BoardBuilder>();

        var boardRanges = new Dictionary<string, string[]>
        {
            ["DirectionsPosition"] = difficultyLevelData.DirectionsPosition,
            ["DirectionsRead"] = difficultyLevelData.DirectionsRead,
            ["DirectionsShape"] = difficultyLevelData.DirectionsShape,
            ["Steps"] = SharedProvider.ArrayOfStringsFromLowAndHighRange(difficultyLevelData.StepsMin, difficultyLevelData.StepsMax),
            ["Shapes"] = SharedProvider.ArrayOfStringsFromLowAndHighRange(difficultyLevelData.ShapesMin, difficultyLevelData.ShapesMax),
        };

        List<BoardBuilder> baseBoardBuilderList = (
            from DirectionsPosition in boardRanges["DirectionsPosition"]
            from DirectionsRead in boardRanges["DirectionsRead"]
            from DirectionsShape in boardRanges["DirectionsShape"]
            from Steps in boardRanges["Steps"]
            from Shapes in boardRanges["Shapes"]
            select new BoardBuilder(
                difficultyLevelData.Difficulty,
                $"{difficultyLevelData.Difficulty}:{_sequenceNumber}",
                _dateTime,
                DirectionsPosition,
                DirectionsShape,
                DirectionsRead,
                difficultyLevelData.StepsRepeats,
                Convert.ToInt32(Steps),
                Convert.ToInt32(Shapes)
                )).ToList();

        boardBuilderList.AddRange(baseBoardBuilderList);

        while (boardBuilderList.Count < _boardCount)
        {
            foreach (var boardBuilder in baseBoardBuilderList)
            {
                boardBuilderList.Add(new BoardBuilder(
                    boardBuilder.Difficulty,
                    boardBuilder.DifficultySequence,
                    boardBuilder.CreatedDt,
                    boardBuilder.DirectionsPosition,
                    boardBuilder.DirectionsShape,
                    boardBuilder.DirectionsRead,
                    boardBuilder.StepsRepeats,
                    boardBuilder.Steps,
                    boardBuilder.Shapes
                    ));
            }
        }

        BoardBuilderList = boardBuilderList;
    }
    #endregion

    #region RemoveDuplicatesThenValidateLoop
    /// <summary>
    /// Boards that have not been validated are looped through.
    /// There are 10 attempts to regenerate duplicate boards.
    /// </summary>
    private async Task RemoveDuplicatesThenValidateLoop()
    {
        List<BoardBuilder> boardBuilderEntity = BoardBuilderList;

        var toCheck = new List<BoardBuilder>();
        int generateAttempt = 0;
        while ((toCheck = boardBuilderEntity
            .Where(x => x.FillStatus != BoardBuilderFillStatusEnum.VALIDATED)
            .ToList()).Count > 0
            && generateAttempt <= 10)
        {
            boardBuilderEntity = await MarkDuplicateBoards(toCheck);
            boardBuilderEntity = RerunDuplicateBoards(boardBuilderEntity);
            generateAttempt++;
        }

        BoardBuilderList = boardBuilderEntity;

    }

    /// <summary>
    /// Board hashes are checked against the database for duplicates.
    /// Duplicate boards are marked as duplicate otherwise marked as validated.
    /// </summary>
    private async Task<List<BoardBuilder>> MarkDuplicateBoards(List<BoardBuilder> boardBuilderEntity)
    {
        List<string> boardIds =
            (from boardBuilder in boardBuilderEntity
             where boardBuilder.FillStatus == BoardBuilderFillStatusEnum.FILLED
             select boardBuilder.Board.Id).ToList();

        List<BoardIdDTO> responseData = await _sender.Send(new GetIdsFromBoardIdsQuery
        {
            BoardIdList = boardIds
        });

        foreach (var board in boardBuilderEntity)
        {
            if (responseData.Where(x => x.Id == board.Board.Id).Count() > 0)
            {
                board.FillStatus = BoardBuilderFillStatusEnum.DUPLICATE;
            }
            else
            {
                board.FillStatus = BoardBuilderFillStatusEnum.VALIDATED;
            }
        }

        return boardBuilderEntity;
    }

    /// <summary>
    /// Duplicate boards are regenerated.
    /// These will then be rechecked.
    /// </summary>
    private List<BoardBuilder> RerunDuplicateBoards(List<BoardBuilder> boardBuilderEntity)
    {
        foreach (var board in boardBuilderEntity)
        {
            if (board.FillStatus == BoardBuilderFillStatusEnum.DUPLICATE)
            {
                board.GenerateBoard();
            }
        }
        return boardBuilderEntity;
    }
    #endregion

    #region ReturnBoardDataForDatabase

    /// <summary>
    /// Returns BoardSeqeunceEntity.
    /// To be sent to database.
    /// </summary>
    /// <returns>BoardSeqeunceEntity</returns>
    public BoardSequenceEntity GetValidatedBoardSequenceEntity()
    {
        var boardSequenceEntity = new BoardSequenceEntity()
        {
            Id = $"Board:{_difficultyLevel}",
            SequenceNumber = _sequenceNumber,
            CreatedDt = _dateTime,
            UpdatedDt = _dateTime
        };

        return boardSequenceEntity;
    }

    /// <summary>
    /// Returns list of BoardEntity.
    /// To be sent to database.
    /// </summary>
    /// <returns>List of BoardEntities</returns>
    public List<BoardEntity> GetValidatedBoardEntityList()
    {
        var boardEntityList = new List<BoardEntity>();
        foreach (var boardEntity in BoardBuilderList)
        {
            boardEntityList.Add(boardEntity.Board);
        }
        return boardEntityList;
    }

    /// <summary>
    /// Returns BoardListEntity.
    /// To be sent to database.
    /// </summary>
    /// <returns>BoardListEntity</returns>
    public BoardListEntity GetValidatedBoardListEntity()
    {
        var boardListEntity = new BoardListEntity()
        {
            Id = $"{_difficultyLevel}:{_sequenceNumber}",
            Difficulty = _difficultyLevel,
            SequenceNumber = _sequenceNumber,
            CreatedDt = _dateTime
        };

        foreach (var boardBuilder in BoardBuilderList)
        {
            boardListEntity.BoardIdList.Add(boardBuilder.Board.Id);
        }

        return boardListEntity;
    }
    #endregion
}
