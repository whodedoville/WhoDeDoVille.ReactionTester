using Svg;

namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class SvgShapeOptionsEntity
{
    public SvgPaintServer? Fill { get; set; } = null;
    public SvgPaintServer? Stroke { get; set; } = null;
    public SvgUnit? StrokeWidth { get; set; } = null;
    public SvgStrokeLineJoin? StrokeLineJoin { get; set; } = null;
    public SvgStrokeLineCap? StrokeLineCap { get; set; } = null;
    public SvgUnit? Radius { get; set; } = null;
    public float? Width { get; set; } = null;
    public float? Height { get; set; } = null;
}
