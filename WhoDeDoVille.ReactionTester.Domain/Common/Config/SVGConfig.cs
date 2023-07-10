using Svg;
using System.Drawing;

namespace WhoDeDoVille.ReactionTester.Domain.Common.Config
{
    public static class SVGConfig
    {
        public static readonly Dictionary<BoardSVGConfigDirectionsEnum, dynamic> DirectionsConstants = new()
        {
            {BoardSVGConfigDirectionsEnum.Fill, new SvgColourServer() { Colour = Color.Transparent } },
            {BoardSVGConfigDirectionsEnum.Stroke, new SvgColourServer() { Colour = Color.Black } },
            {BoardSVGConfigDirectionsEnum.StrokeWidth, 1 },
            {BoardSVGConfigDirectionsEnum.StrokeLineJoin, SvgStrokeLineJoin.Round },
            {BoardSVGConfigDirectionsEnum.StrokeLineCap, SvgStrokeLineCap.Round },
            {BoardSVGConfigDirectionsEnum.StartColor, new SvgColourServer() { Colour = Color.Green } },
            {BoardSVGConfigDirectionsEnum.EndColor, new SvgColourServer() { Colour = Color.Red } }
        };

        public static readonly Dictionary<BaseBoardSvgShapeEnum, dynamic> ShapeConstants = new()
        {
            {BaseBoardSvgShapeEnum.Fill, new SvgColourServer() { Colour = Color.Transparent } },
            {BaseBoardSvgShapeEnum.Stroke, new SvgColourServer() { Colour = Color.Black } },
            {BaseBoardSvgShapeEnum.StrokeWidth, 1 },
            {BaseBoardSvgShapeEnum.StrokeLineJoin, SvgStrokeLineJoin.Round },
            {BaseBoardSvgShapeEnum.StrokeLineCap, SvgStrokeLineCap.Round }
        };
    }
}
