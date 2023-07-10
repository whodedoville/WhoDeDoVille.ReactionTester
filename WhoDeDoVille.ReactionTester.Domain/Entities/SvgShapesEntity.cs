using Svg;
using WhoDeDoVille.ReactionTester.Domain.Common.Config;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;


namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class SvgShapesEntity
{
    //private int _boardWidth = BoardConfig.WidthGridCount * BoardConfig.Blocksize;
    //private int _boardHeight = BoardConfig.HeightGridCount * BoardConfig.Blocksize;

    //private List<ColorEntity> _boardColors = new List<ColorEntity>();
    //private UserColorsEntity _userColors { get; set; }

    public SvgShapesEntity()
    {
        //_userColors = new UserColorsEntity(Color1, Color2, Color3);
    }

    #region Board Shapes

    /// <summary>
    /// Svg Circular gradiant.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public SvgRadialGradientServer GetSvgRadialGradient(int x, int y)
    {
        //TODO: Needs to be finished
        var svgRadial = new SvgRadialGradientServer()
        {
            CenterX = x * BoardConfig.Blocksize + BoardConfig.Blocksize / 2,
            CenterY = y * BoardConfig.Blocksize + BoardConfig.Blocksize / 2,
            Radius = BoardConfig.Blocksize / 2,
            Fill = new SvgColourServer() { Colour = Color.Red },
        };

        return svgRadial;
    }

    /// <summary>
    /// Svg Circle that fits in the board grid.
    /// </summary>
    /// <param name="colorEnum"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public SvgCircle GetBoardCircle(int x, int y, SvgShapeOptionsEntity? svgParams)
    {
        var halfSize = BoardConfig.Blocksize / 2;
        var svgCircle = new SvgCircle()
        {
            CenterX = (x * BoardConfig.Blocksize) + (halfSize),
            CenterY = (y * BoardConfig.Blocksize) + (halfSize),
            Radius = halfSize - BoardConfig.MarginSpace
        };

        if (svgParams != null) svgCircle = (SvgCircle)AddShapeParameters(svgCircle, svgParams);

        return svgCircle;
    }

    /// <summary>
    /// Svg Rectangle that fits in the board grid.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="SvgColor"></param>
    /// <returns></returns>
    public SvgRectangle GetBoardRectangle(int x, int y, SvgShapeOptionsEntity? svgParams)
    {
        var svgRectangle = new SvgRectangle()
        {
            X = (x * BoardConfig.Blocksize) + BoardConfig.MarginSpace,
            Y = (y * BoardConfig.Blocksize) + BoardConfig.MarginSpace,
            Width = BoardConfig.Blocksize - (BoardConfig.MarginSpace * 2),
            Height = BoardConfig.Blocksize - (BoardConfig.MarginSpace * 2),
        };

        if (svgParams != null) svgRectangle = (SvgRectangle)AddShapeParameters(svgRectangle, svgParams);

        return svgRectangle;
    }

    /// <summary>
    /// Svg Star that fits in the board grid
    /// </summary>
    /// <param name="color"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public SvgPolygon GetBoardStar(int x, int y, SvgShapeOptionsEntity? svgParams)
    {
        var startX = (x * BoardConfig.Blocksize);
        var startY = (y * BoardConfig.Blocksize);
        var fromTop = .35;
        var fromSide = .22;
        //List<Point> points = new(){
        //    new Point(startX, startY + BoardConfig.MarginSpace),
        //    new Point(startX - 7, startY + BoardConfig.Blocksize - BoardConfig.MarginSpace),
        //    new Point(startX + 10, startY + 8),
        //    new Point(startX - 10, startY + 8),
        //    new Point(startX + 7, startY + BoardConfig.Blocksize - BoardConfig.MarginSpace),
        //};

        List<Point> points = new(){
            new Point(
                startX + (BoardConfig.Blocksize / 2),
                startY + BoardConfig.MarginSpace),
            new Point(
                startX + (int)(BoardConfig.Blocksize * fromSide),
                startY + BoardConfig.Blocksize - BoardConfig.MarginSpace),
            new Point(
                startX + BoardConfig.Blocksize - BoardConfig.MarginSpace,
                startY + (int)(BoardConfig.Blocksize * fromTop)),
            new Point(
                startX + BoardConfig.MarginSpace,
                startY + (int)(BoardConfig.Blocksize * fromTop)),
            new Point(
                startX + BoardConfig.Blocksize - (int)(BoardConfig.Blocksize * fromSide),
                startY + BoardConfig.Blocksize - BoardConfig.MarginSpace),
        };

        var svgPolygon = InitializeSvgPolygon(points);

        if (svgParams != null) svgPolygon = (SvgPolygon)AddShapeParameters(svgPolygon, svgParams);

        return svgPolygon;
    }

    /// <summary>
    /// Rectangle can be diagonal.
    /// </summary>
    /// <param name="StartX"></param>
    /// <param name="StartY"></param>
    /// <param name="EndX"></param>
    /// <param name="EndY"></param>
    /// <param name="svgParams"></param>
    /// <returns></returns>
    public SvgPolyline GetBoardRectangleFromStartAndEnd(int StartX, int StartY, int EndX, int EndY,
        SvgShapeOptionsEntity? svgParams)
    {
        var bSize = BoardConfig.Blocksize;
        List<Point> points = new(){
            new Point(StartX * bSize, StartY * bSize),
            new Point(EndX * bSize + bSize, EndY * bSize),
            new Point(EndX * bSize + bSize, EndY * bSize + bSize),
            new Point(StartX * bSize, StartY * bSize + bSize),
            new Point(StartX * bSize, StartY * bSize),
        };
        var svgPolyline = InitializeSvgPloyline(points);

        if (svgParams != null) svgPolyline = (SvgPolyline)AddShapeParameters(svgPolyline, svgParams);

        return svgPolyline;
    }

    #endregion

    #region Board Shape Helpers

    /// <summary>
    /// Initialize Svg Polygon from point list
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private SvgPolygon InitializeSvgPolygon(List<Point> points)
    {
        var svgPolygon = new SvgPolygon()
        {
            Points = ConvertPointsListToSvgPointCollection(points)
        };
        return svgPolygon;
    }

    /// <summary>
    /// Initialize Svg Polyline from point list
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private SvgPolyline InitializeSvgPloyline(List<Point> points)
    {
        var svgPolyline = new SvgPolyline()
        {
            Points = ConvertPointsListToSvgPointCollection(points)
        };
        return svgPolyline;
    }

    /// <summary>
    /// Convert Points list to Svg Point Collection
    /// </summary>
    /// <param name="points">x,y coordinates</param>
    /// <returns></returns>
    private SvgPointCollection ConvertPointsListToSvgPointCollection(List<Point> points)
    {
        var pointCollection = new SvgPointCollection();

        foreach (var point in points)
        {
            pointCollection.Add(point.X);
            pointCollection.Add(point.Y);
        }

        return pointCollection;
    }

    /// <summary>
    /// Add parameters to shape
    /// </summary>
    /// <param name="shape">Svg Shape</param>
    /// <param name="svgParams">Svg Parameters</param>
    /// <returns></returns>
    private dynamic AddShapeParameters(dynamic shape, SvgShapeOptionsEntity? svgParams)
    {
        if (svgParams.Fill is not null) shape.Fill = svgParams.Fill;
        if (svgParams.Stroke is not null) shape.Stroke = svgParams.Stroke;
        if (svgParams.StrokeWidth is not null) shape.StrokeWidth = (SvgUnit)svgParams.StrokeWidth;
        if (svgParams.StrokeLineJoin is not null) shape.StrokeLineJoin = (SvgStrokeLineJoin)svgParams.StrokeLineJoin;
        if (svgParams.StrokeLineCap is not null) shape.StrokeLineCap = (SvgStrokeLineCap)svgParams.StrokeLineCap;
        if (svgParams.Width is not null) shape.Width = (SvgUnit)svgParams.Width;
        if (svgParams.Height is not null) shape.Height = (SvgUnit)svgParams.Height;

        //shape.Fill = svgParams.Fill;
        //shape.Stroke = svgParams.Stroke;
        //shape.StrokeWidth = (SvgUnit)svgParams.StrokeWidth;
        //shape.StrokeLineJoin = (SvgStrokeLineJoin)svgParams.StrokeLineJoin;
        //shape.StrokeLineCap = (SvgStrokeLineCap)svgParams.StrokeLineCap;

        return shape;
    }
    #endregion
}
