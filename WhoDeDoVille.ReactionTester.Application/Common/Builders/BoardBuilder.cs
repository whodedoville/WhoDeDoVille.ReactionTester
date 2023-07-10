using Point = System.Drawing.Point;

namespace WhoDeDoVille.ReactionTester.Application.Common.Builders;

/// <summary>
/// Build BoardEntity based on provided parameters.
/// </summary>
/// <param name="DifficultyLevel">Board Difficulty Level</param>
/// <param name="SequenceNumber">
/// Needs to be a unique number. Supposed to be in sequence by difficulty.
/// </param>
/// <param name="CreatedDt">
/// When the board was created. Should match creationDt on BoardListEntity
/// </param>
/// <param name="DirectionsPosition">Where the directions on the board is located.</param>
/// <param name="DirectionsShape">The shape of the directions that are displayed.</param>
/// <param name="DirectionsRead">If the directions are read left to right or the opposite.</param>
/// <param name="StepsRepeats">If directions are repeated or not.</param>
/// <param name="Steps">Number of steps in the directions.</param>
/// <param name="Shapes">Number of different shapes displayed on the board.</param>
public class BoardBuilder
{
    public BoardEntity Board { get; set; } = new BoardEntity();

    public BoardBuilderFillStatusEnum FillStatus { get; set; } = BoardBuilderFillStatusEnum.EMPTY;
    public int Difficulty { get; init; }
    public string DifficultySequence { get; init; }
    public DateTime CreatedDt { get; init; }
    public string DirectionsPosition { get; init; }
    public string DirectionsShape { get; init; }
    public string DirectionsRead { get; init; }
    public bool StepsRepeats { get; init; }
    public int Steps { get; init; }
    public int Shapes { get; init; }
    private Dictionary<int, List<int>> _gridCoords { get; set; }
    private Dictionary<ShapesEnum, List<ColorsEnum>> _shapeOptions { get; set; }

    public BoardBuilder() { }

    public BoardBuilder(
        int difficulty,
        string difficultySequence,
        DateTime createdDt,
        string directionsPosition,
        string directionsShape,
        string directionsRead,
        bool stepsRepeats,
        int steps,
        int shapes)
    {
        Difficulty = difficulty;
        DifficultySequence = difficultySequence;
        CreatedDt = createdDt;
        DirectionsPosition = directionsPosition;
        DirectionsShape = directionsShape;
        DirectionsRead = directionsRead;
        StepsRepeats = stepsRepeats;
        Steps = steps;
        Shapes = shapes;

        GenerateBoard();
    }

    /// <summary>
    /// Generates board based on initialized parameters.
    /// </summary>
    public void GenerateBoard()
    {
        FillStatus = BoardBuilderFillStatusEnum.FILLING;

        _gridCoords = FillGridCoords();
        _shapeOptions = FillShapeOptions();

        SetDirectionsCoords();
        SetShapes();

        Board.DifficultySequence = DifficultySequence;

        Board.FillBoardIdAndPartitionKey();

        FillStatus = BoardBuilderFillStatusEnum.FILLED;
    }

    #region FillOptions

    /// <summary>
    /// Creates initial list of grid positions.
    /// </summary>
    /// <returns>List of available grid positions.</returns>
    private Dictionary<int, List<int>> FillGridCoords()
    {
        var returnCoords = new Dictionary<int, List<int>>();

        for (int x = 0; x < BoardConfig.WidthGridCount; x++)
        {
            for (int y = 0; y < BoardConfig.HeightGridCount; y++)
            {
                if (returnCoords.ContainsKey(x) == false)
                {
                    returnCoords[x] = new List<int>();
                }
                returnCoords[x].Add(y);
            }
        }
        return returnCoords;
    }

    /// <summary>
    /// Creates initial list of shapes and colors.
    /// </summary>
    /// <returns>List of available shapes and colors.</returns>
    private Dictionary<ShapesEnum, List<ColorsEnum>> FillShapeOptions()
    {
        var returnShapes = new Dictionary<ShapesEnum, List<ColorsEnum>>();

        foreach (ShapesEnum shape in Enum.GetValues(typeof(ShapesEnum)))
        {
            foreach (ColorsEnum color in Enum.GetValues(typeof(ColorsEnum)))
            {
                if (returnShapes.ContainsKey(shape) == false)
                {
                    returnShapes[shape] = new List<ColorsEnum>();
                }
                returnShapes[shape].Add(color);
            }
        }

        return returnShapes;
    }
    #endregion

    #region SetDirectionsCoords

    /// <summary>
    /// Fills board directions based on provided parameters.
    /// </summary>
    public void SetDirectionsCoords()
    {
        var directionsLength = Convert.ToInt32(Steps) + 2;

        GetDirectionsStartCoords(directionsLength);

        if (DirectionsPosition != "beginner") GetDirectionsEndCoords(directionsLength);

        SetDirectionsCoord();

        RemoveDirectionsCoordFromGrid();
    }

    /// <summary>
    /// Sets directions legend starting coordinates based on directions board position and number of steps.
    /// </summary>
    private void GetDirectionsStartCoords(int directionsLength)
    {
        var centerX = BoardConfig.WidthGridCount / 2;
        var centerY = BoardConfig.HeightGridCount / 2;

        Board.DirectionsCoords.StartX = 1;
        Board.DirectionsCoords.StartY = 1;
        Board.DirectionsCoords.EndX = BoardConfig.WidthGridCount;
        Board.DirectionsCoords.EndY = BoardConfig.HeightGridCount;

        switch (DirectionsPosition)
        {
            case "beginner":
                Board.DirectionsCoords.StartX = centerX - directionsLength / 2;
                Board.DirectionsCoords.EndX = Board.DirectionsCoords.StartX + (directionsLength - 1);
                Board.DirectionsCoords.EndY = Board.DirectionsCoords.StartY;
                return;
                break;
            case "top":
                Board.DirectionsCoords.EndY = centerY;
                break;
            case "bottom":
                Board.DirectionsCoords.StartY = centerY;
                break;
            case "left":
                Board.DirectionsCoords.EndX = centerX;
                break;
            case "right":
                Board.DirectionsCoords.StartX = centerX;
                break;
            default:
                // code block
                break;
        }

        Board.DirectionsCoords.StartX = SharedProvider.GetRandomNumber(Board.DirectionsCoords.StartX, Board.DirectionsCoords.EndX + 1);
        Board.DirectionsCoords.StartY = SharedProvider.GetRandomNumber(Board.DirectionsCoords.StartY, Board.DirectionsCoords.EndY + 1);
    }

    /// <summary>
    /// Sets directions legend ending coordinates based on directions board shape and number of steps.
    /// </summary>
    private void GetDirectionsEndCoords(int directionsLength)
    {
        var endDiff = (directionsLength - 1);
        var gridEdgeX = DirectionsPosition == "anywhere"
            ? BoardConfig.WidthGridCount : BoardConfig.WidthGridCount / 2;
        var gridEdgeY = DirectionsPosition == "anywhere"
            ? BoardConfig.HeightGridCount : BoardConfig.HeightGridCount / 2;
        if (gridEdgeX < endDiff) gridEdgeX = BoardConfig.WidthGridCount;
        if (gridEdgeY < endDiff) gridEdgeY = BoardConfig.HeightGridCount;

        var endCoord = new List<int[]>();

        switch (DirectionsShape)
        {
            case "horizontal":
                if (Board.DirectionsCoords.StartX - endDiff > 1)
                {
                    endCoord.Add(new int[]
                    {
                        Board.DirectionsCoords.StartX - endDiff,
                        Board.DirectionsCoords.StartY
                    });
                }

                if (Board.DirectionsCoords.StartX + endDiff < gridEdgeX || endCoord.Count == 0)
                {
                    endCoord.Add(new int[]
                    {
                        Board.DirectionsCoords.StartX + endDiff,
                        Board.DirectionsCoords.StartY
                    });
                }
                break;
            case "vertical":
                if (Board.DirectionsCoords.StartY - endDiff > 1)
                {
                    endCoord.Add(new int[]
                    {
                        Board.DirectionsCoords.StartX,
                        Board.DirectionsCoords.StartY - endDiff
                    });
                }

                if (Board.DirectionsCoords.StartY + endDiff < gridEdgeY || endCoord.Count == 0)
                {
                    endCoord.Add(new int[]
                    {
                        Board.DirectionsCoords.StartX,
                        Board.DirectionsCoords.StartY + endDiff
                    });
                }
                break;
            case "diagonal":
                if (Board.DirectionsCoords.StartX - endDiff > 1)
                {
                    if (Board.DirectionsCoords.StartY - endDiff > 1)
                    {
                        endCoord.Add(new int[]
                        {
                            Board.DirectionsCoords.StartX - endDiff,
                            Board.DirectionsCoords.StartY - endDiff
                        });
                    }

                    if (Board.DirectionsCoords.StartY + endDiff < gridEdgeY || endCoord.Count == 0)
                    {
                        endCoord.Add(new int[]
                        {
                            Board.DirectionsCoords.StartX - endDiff,
                            Board.DirectionsCoords.StartY + endDiff
                        });
                    }
                }

                if (Board.DirectionsCoords.StartX + endDiff < gridEdgeX || endCoord.Count == 0)
                {
                    if (Board.DirectionsCoords.StartY - endDiff > 1)
                    {
                        endCoord.Add(new int[]
                        {
                            Board.DirectionsCoords.StartX + endDiff,
                            Board.DirectionsCoords.StartY - endDiff
                        });
                    }

                    if (Board.DirectionsCoords.StartY + endDiff < gridEdgeY || endCoord.Count == 0)
                    {
                        endCoord.Add(new int[]
                        {
                            Board.DirectionsCoords.StartX + endDiff,
                            Board.DirectionsCoords.StartY + endDiff
                        });
                    }
                }
                break;
        }
        var selEndCoord = SharedProvider.GetRandomNumber(0, endCoord.Count);

        try
        {
            //TODO: Test for index out of range.
            Board.DirectionsCoords.EndX = endCoord[selEndCoord][0];
            Board.DirectionsCoords.EndY = endCoord[selEndCoord][1];
        }
        catch (Exception ex)
        {
            return;
        }

    }

    /// <summary>
    /// Determines if the directions starting and ending coords need to be flipped based on Directions read and other parameters.
    /// </summary>
    private void SetDirectionsCoord()
    {
        var centerX = BoardConfig.WidthGridCount / 2;
        var centerY = BoardConfig.HeightGridCount / 2;
        var startX = Board.DirectionsCoords.StartX;
        var startY = Board.DirectionsCoords.StartY;
        var endX = Board.DirectionsCoords.EndX;
        var endY = Board.DirectionsCoords.EndY;
        var shape = DirectionsShape;
        var read = DirectionsRead;
        var horizontal = false;
        var vertical = false;

        if (shape == "horizontal"
            && (read == "left to right" && startX > endX
            || read == "right to left" && startX < endX))
        {
            horizontal = true;
        }

        if (shape == "vertical"
            && (startX < centerX
                && read == "right to left" && startY < endY
                || read == "left to right" && startY > endY
                || startX > centerX
                && read == "right to left" && startY > endY
                || read == "left to right" && startY < endY))
        {
            vertical = true;
        }

        if (shape == "horizontal" && horizontal == true
            || shape == "vertical" && vertical == true
            || shape == "diagonal" && (horizontal == true || vertical == true))
        {
            (Board.DirectionsCoords.StartX, Board.DirectionsCoords.EndX)
                = (Board.DirectionsCoords.EndX, Board.DirectionsCoords.StartX);
            (Board.DirectionsCoords.StartY, Board.DirectionsCoords.EndY)
                = (Board.DirectionsCoords.EndY, Board.DirectionsCoords.StartY);
        }
    }

    /// <summary>
    /// Removes directions x,y coords from _gridCoords so they cannot be reused.
    /// </summary>
    private void RemoveDirectionsCoordFromGrid()
    {
        BoardDirectionsCoordsEntity directionsCoords = Board.DirectionsCoords;
        var directionsLength = Convert.ToInt32(Steps) + 2;

        var lineHelper = new LineHelperEntity(
            new Point(directionsCoords.StartX, directionsCoords.StartY),
            new Point(directionsCoords.EndX, directionsCoords.EndY)
            );
        var points = lineHelper.getPoints(directionsLength);

        foreach (var p in points)
        {
            RemoveGridCoords(p.X, p.Y);
        }
    }

    /// <summary>
    /// Removes x,y coords from _gridCoords so they cannot be reused.
    /// </summary>
    private void RemoveGridCoords(int x, int y)
    {
        if (_gridCoords.ContainsKey(x) == true && _gridCoords[x].Contains(y) == true)
        {
            _gridCoords[x].Remove(y);
            if (_gridCoords[x].Count == 0)
            {
                _gridCoords.Remove(x);
            }
        }
    }
    #endregion

    #region SetShapes

    /// <summary>
    /// Randomly selects shapes for steps and fills board x,y coords.
    /// </summary>
    public void SetShapes()
    {
        SelectShapesAnswer();
    }

    /// <summary>
    /// Randomly selects shapes for the board answer based on parameters.
    /// </summary>
    private void SelectShapesAnswer()
    {
        while (Shapes > Board.Shapes.Count || Steps > Board.Answer.Count)
        {
            int x = -1;
            int y = -1;

            if (Shapes <= Board.Shapes.Count)
            {
                RecreatShapeOptionsFromUsed();
            }

            GetRandomShape(out ShapesEnum shape, out ColorsEnum color);
            var shapeMatch = Board.Shapes.Where(s => (s.Shape == shape) && (s.Color == color));
            var shapeRepeat = shapeMatch.Any();


            if (shapeRepeat == true)
            {
                var firstShapeMatch = shapeMatch.First();
                x = firstShapeMatch.X;
                y = firstShapeMatch.Y;
            }
            else
            {
                GetRandomGridCoords(ref x, ref y);
                RemoveGridCoords(x, y);
            }

            if (x >= 0 && y >= 0)
            {
                if (StepsRepeats == false
                || Board.Answer.Count >= Steps
                || shapeRepeat == true)
                {
                    RemoveShapeOption(shape, color);
                }

                if (Board.Answer.Count < Steps)
                {
                    Board.Answer.Add(new BoardAnswerEntity() { X = x, Y = y });
                }

                if (Board.Shapes.Where(s => (s.Shape == shape) && (s.Color == color)).Count() == 0)
                {
                    Board.Shapes.Add(new BoardShapeEntity() { X = x, Y = y, Shape = shape, Color = color });
                }
            }
        }
    }

    /// <summary>
    /// Removes shape from the shape options.
    /// </summary>
    private void RemoveShapeOption(ShapesEnum shape, ColorsEnum color)
    {
        if (_shapeOptions.ContainsKey(shape) == true && _shapeOptions[shape].Contains(color) == true)
        {
            _shapeOptions[shape].Remove(color);
            if (_shapeOptions[shape].Count == 0)
            {
                _shapeOptions.Remove(shape);
            }
        }
    }

    /// <summary>
    /// Remakes the _shapeOptions variable with already used shapes.
    /// For when another answer needs to be selected from already placed shapes.
    /// </summary>
    private void RecreatShapeOptionsFromUsed()
    {
        var newShapesOptions = new Dictionary<ShapesEnum, List<ColorsEnum>>();

        foreach (var bShape in Board.Shapes)
        {
            if (Board.Answer.Where(s => (s.X == bShape.X) && (s.Y == bShape.Y)).Count() < 2)
            {
                if (newShapesOptions.ContainsKey(bShape.Shape) == false)
                {
                    newShapesOptions[bShape.Shape] = new List<ColorsEnum>();
                }
                newShapesOptions[bShape.Shape].Add(bShape.Color);
            }
        }

        _shapeOptions = newShapesOptions;
    }

    /// <summary>
    /// Randomly selects x,y coords from _gridCoords.
    /// </summary>
    private void GetRandomGridCoords(ref int x, ref int y)
    {
        var randX = SharedProvider.GetRandomNumber(0, _gridCoords.Count);
        x = _gridCoords.ElementAt(randX).Key;
        //var listX = _gridCoords.Keys.ToList();
        //x = listX[randX];
        var randY = SharedProvider.GetRandomNumber(0, _gridCoords[randX].Count);
        y = _gridCoords[randX][randY];
    }

    /// <summary>
    /// Randomly selects shape and color from _shapeOptions.
    /// </summary>
    private void GetRandomShape(out ShapesEnum shape, out ColorsEnum color)
    {
        shape = _shapeOptions.ElementAt(SharedProvider.GetRandomNumber(0, _shapeOptions.Keys.Count)).Key;
        color = _shapeOptions[shape][SharedProvider.GetRandomNumber(0, _shapeOptions[shape].Count)];
    }
    #endregion
}
