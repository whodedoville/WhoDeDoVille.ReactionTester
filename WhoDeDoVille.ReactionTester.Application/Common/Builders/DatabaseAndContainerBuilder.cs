using Microsoft.Azure.Cosmos;
using WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;
using WhoDeDoVille.ReactionTester.Application.BoardList.Commands.Generate;
using WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands.Generate;
using WhoDeDoVille.ReactionTester.Logging;

namespace WhoDeDoVille.ReactionTester.Application.Common.Builders;

//TODO: Needs testing.
public class DatabaseAndContainerBuilder
{
    public List<DatabaseAndContainerNamesEnum> DatabaseAndContainerNamesEnums { get; set; }

    private readonly ISender _sender;
    private readonly ILogger _logger;
    private readonly LoggingMessages _loggingMessages;

    public Dictionary<DatabaseAndContainerNamesEnum, DatabaseResponse> DatabaseResponses { get; } = new Dictionary<DatabaseAndContainerNamesEnum, DatabaseResponse>();
    public List<DatabaseAndContainerNamesEnum> DatabaseFailedList { get; } = new List<DatabaseAndContainerNamesEnum>();

    public Dictionary<DatabaseAndContainerNamesEnum, ContainerResponse> ContainerResponses { get; } = new Dictionary<DatabaseAndContainerNamesEnum, ContainerResponse>();
    public List<DatabaseAndContainerNamesEnum> ContainerFailedList { get; } = new List<DatabaseAndContainerNamesEnum>();

    public DatabaseAndContainerBuilder(ISender sender, ILoggerFactory loggerFactory)
    {
        _sender = sender;
        _logger = loggerFactory.CreateLogger<DatabaseAndContainerBuilder>();
        _loggingMessages = new LoggingMessages(_logger);
    }

    public async Task<Dictionary<DatabaseAndContainerNamesEnum, ContainerResponse>> InitializeDatabaseAndContainer()
    {
        DatabaseFailedList.Clear();
        ContainerFailedList.Clear();
        //Check Database creation
        foreach (var dbContainer in DatabaseAndContainerNamesEnums)
        {
            DatabaseResponse dbResData = null;
            switch (dbContainer)
            {
                case DatabaseAndContainerNamesEnum.DATABASE_REACTIONTESTER:

                    break;
            }
            if (dbResData != null)
            {
                if (dbResData.StatusCode.ToString().Substring(0, 1) == "4")
                {
                    DatabaseFailedList.Add(dbContainer);
                }
                DatabaseResponses.Add(dbContainer, dbResData);
            }
        }
        if (DatabaseFailedList.Count > 0)
        {
            _loggingMessages.LogFailedDatabaseCreation(string.Join(",", DatabaseFailedList));
            throw new FailedCreationException("Failed Database Creation");
        }

        //Check container creation
        foreach (var dbContainer in DatabaseAndContainerNamesEnums)
        {
            ContainerResponse containerResData = null;
            switch (dbContainer)
            {
                case DatabaseAndContainerNamesEnum.CONTAINER_BOARDSEQUENCE:
                    containerResData = await _sender.Send(new GenerateBoardSequenceContainerCommand { });
                    //var boardSeqeunceResData = await _sender.Send(new GenerateBoardSequenceContainerCommand { });
                    //ContainerResponses.Add(DatabaseAndContainerNamesEnum.CONTAINER_BOARDSEQUENCE, boardSeqeunceResData);
                    break;
                case DatabaseAndContainerNamesEnum.CONTAINER_BOARD:
                    containerResData = await _sender.Send(new GenerateBoardContainerCommand { });
                    //var boardResData = await _sender.Send(new GenerateBoardContainerCommand { });
                    //ContainerResponses.Add(DatabaseAndContainerNamesEnum.CONTAINER_BOARD, boardResData);
                    break;
                case DatabaseAndContainerNamesEnum.CONTAINER_BOARDLIST:
                    containerResData = await _sender.Send(new GenerateBoardListContainerCommand { });
                    //var boardListResData = await _sender.Send(new GenerateBoardListContainerCommand { });
                    //ContainerResponses.Add(DatabaseAndContainerNamesEnum.CONTAINER_BOARDLIST, boardListResData);
                    break;
            }
            if (containerResData != null)
            {
                if (containerResData.StatusCode.ToString().Substring(0, 1) == "4")
                {
                    ContainerFailedList.Add(dbContainer);
                }
                ContainerResponses.Add(dbContainer, containerResData);
            }
        }
        if (ContainerFailedList.Count > 0)
        {
            _loggingMessages.LogFailedContainerCreation(string.Join(",", ContainerFailedList));
            throw new FailedCreationException("Failed Container Creation");
        }

        return ContainerResponses;
    }
}
