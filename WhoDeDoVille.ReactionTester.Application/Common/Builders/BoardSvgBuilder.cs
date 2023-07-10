using Svg;
using System.Drawing;
using Color = System.Drawing.Color;

namespace WhoDeDoVille.ReactionTester.Application.Common.Builders;

/// <summary>
/// Builds and SVG based on Board.
/// </summary>
public class BoardSvgBuilder
{
    private int _boardWidth = BoardConfig.WidthGridCount * BoardConfig.Blocksize;
    private int _boardHeight = BoardConfig.HeightGridCount * BoardConfig.Blocksize;

    private BoardEntity _board { get; set; } = new BoardEntity();
    private UserColorsEntity _userColors { get; set; }
    private SvgShapesEntity _svgShapes { get; set; }
    public SvgDocument SvgBoardDoc { get; set; } = new SvgDocument()
    {
        ViewBox = new SvgViewBox()
        {
            Width = BoardConfig.WidthGridCount * BoardConfig.Blocksize + BoardConfig.MarginSpace,
            Height = BoardConfig.HeightGridCount * BoardConfig.Blocksize + BoardConfig.MarginSpace,
        }
    };

    public BoardSvgBuilder(BoardEntity Board, string Color1, string Color2, string Color3)
    {
        _board = Board;
        _userColors = new UserColorsEntity(Color1, Color2, Color3);
        _svgShapes = new SvgShapesEntity();

        GenerateSVG();
    }

    public Bitmap ConvertSvgToBmp()
    {
        SvgBoardDoc.ShapeRendering = SvgShapeRendering.Auto;
        Bitmap bmp = SvgBoardDoc.Draw(_boardWidth, _boardHeight);
        return bmp;
    }

    /// <summary>
    /// Fill SvgDocument based on board parameters
    /// </summary>
    private void GenerateSVG()
    {
        InsertBoardBorder();
        InsertDirectionsCoords();
        InsertAnswerDirections();
        InsertBoardShapes();
    }

    #region InsertBoardBorder
    /// <summary>
    /// Inserts Board border to SvgBoardDoc
    /// </summary>
    private void InsertBoardBorder()
    {
        var borderGroup = new SvgGroup();

        var shapeOptions = new SvgShapeOptionsEntity()
        {
            Fill = new SvgColourServer() { Colour = Color.Transparent },
            Stroke = new SvgColourServer() { Colour = Color.Black },
            StrokeWidth = 1,
            Width = SvgBoardDoc.ViewBox.Width,
            Height = SvgBoardDoc.ViewBox.Height
        };

        var svgRectangle = _svgShapes.GetBoardRectangle(0, 0, shapeOptions);

        borderGroup.Children.Add(svgRectangle);

        SvgBoardDoc.Children.Add(borderGroup);
    }
    #endregion

    #region InsertDirectionsCoords
    /// <summary>
    /// Inserts BoardDirectionsCoordsEntity to SvgBoardDoc
    /// </summary>
    private void InsertDirectionsCoords()
    {
        var directionsCoords = _board.DirectionsCoords;
        var directionsConstants = SVGConfig.DirectionsConstants;
        var dirGroup = new SvgGroup();

        var shapeOptions = new SvgShapeOptionsEntity()
        {
            Fill = directionsConstants[BoardSVGConfigDirectionsEnum.Fill],
            Stroke = directionsConstants[BoardSVGConfigDirectionsEnum.Stroke],
            StrokeWidth = directionsConstants[BoardSVGConfigDirectionsEnum.StrokeWidth],
            StrokeLineJoin = directionsConstants[BoardSVGConfigDirectionsEnum.StrokeLineJoin],
            StrokeLineCap = directionsConstants[BoardSVGConfigDirectionsEnum.StrokeLineCap]
        };

        var svgPolyline = _svgShapes.GetBoardRectangleFromStartAndEnd(
            directionsCoords.StartX, directionsCoords.StartY,
            directionsCoords.EndX, directionsCoords.EndY, shapeOptions);

        dirGroup.Children.Add(svgPolyline);

        //var startRadial = _svgShapes.GetSvgRadialGradient(directionsCoords.StartX, directionsCoords.StartY);
        //dirGroup.Children.Add(startRadial);

        //TODO: Use something better than circles for directions start and end indicators

        var circleOptions = new SvgShapeOptionsEntity()
        {
            Fill = directionsConstants[BoardSVGConfigDirectionsEnum.StartColor],
        };

        var startCircle = _svgShapes.GetBoardCircle(directionsCoords.StartX, directionsCoords.StartY, circleOptions);
        dirGroup.Children.Add(startCircle);

        circleOptions.Fill = directionsConstants[BoardSVGConfigDirectionsEnum.EndColor];
        var endCircle = _svgShapes.GetBoardCircle(directionsCoords.EndX, directionsCoords.EndY, circleOptions);
        dirGroup.Children.Add(endCircle);

        SvgBoardDoc.Children.Add(dirGroup);
    }
    #endregion

    #region InsertAnswerDirections

    /// <summary>
    /// Insert Answer shapes into directions box
    /// </summary>
    private void InsertAnswerDirections()
    {
        var directionsCoords = _board.DirectionsCoords;
        var boardAnswer = _board.Answer;
        var boardShapes = _board.Shapes;
        var answerGroup = new SvgGroup();

        var lineHelper = new LineHelperEntity(
            new Point(directionsCoords.StartX, directionsCoords.StartY),
            new Point(directionsCoords.EndX, directionsCoords.EndY)
            );
        var points = lineHelper.getPoints(boardAnswer.Count + 2);

        for (int i = 0; i < boardAnswer.Count; i++)
        {
            var answer = boardAnswer[i];
            var boardShape = boardShapes.Where(s => s.X == answer.X && s.Y == answer.Y).First();
            if (boardShape != null)
            {
                var answerShape = GetBoardShape(boardShape.Shape, boardShape.Color, points[i + 1].X, points[i + 1].Y);

                if (answerShape is not null) answerGroup.Children.Add(answerShape);
            }
        }
        SvgBoardDoc.Children.Add(answerGroup);
    }

    #endregion

    #region InsertBoardShapes

    /// <summary>
    /// Insert Board shapes into board grid
    /// </summary>
    private void InsertBoardShapes()
    {
        var boardShapes = _board.Shapes;
        var shapesGroup = new SvgGroup();

        foreach (var boardShape in boardShapes)
        {
            var shape = GetBoardShape(boardShape.Shape, boardShape.Color, boardShape.X, boardShape.Y);

            if (shape is not null) shapesGroup.Children.Add(shape);
        }

        SvgBoardDoc.Children.Add(shapesGroup);
    }

    #endregion

    #region helpers

    private dynamic GetBoardShape(ShapesEnum shape, ColorsEnum color, int x, int y)
    {
        var shapeConstants = SVGConfig.ShapeConstants;

        var shapeOptions = new SvgShapeOptionsEntity()
        {
            Fill = _userColors.GetUserSvgColor(color),
            Stroke = shapeConstants[BaseBoardSvgShapeEnum.Stroke],
            StrokeWidth = shapeConstants[BaseBoardSvgShapeEnum.StrokeWidth],
            StrokeLineJoin = shapeConstants[BaseBoardSvgShapeEnum.StrokeLineJoin],
            StrokeLineCap = shapeConstants[BaseBoardSvgShapeEnum.StrokeLineCap]
        };

        switch (shape)
        {
            case ShapesEnum.CIRCLE:
                return _svgShapes.GetBoardCircle(x, y, shapeOptions);
                break;
            case ShapesEnum.SQUARE:
                return _svgShapes.GetBoardRectangle(x, y, shapeOptions);
                break;
            case ShapesEnum.STAR:
                return _svgShapes.GetBoardStar(x, y, shapeOptions);
                break;
        }
        return null;
    }
    #endregion
}
