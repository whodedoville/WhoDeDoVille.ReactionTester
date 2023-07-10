using Svg;
using System.Drawing;
using Color = System.Drawing.Color;

namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class ColorEntity
{
    public SvgColourServer SvgColor { get; set; }
    public Color ColorColor { get; set; }
    public string HexColor { get; set; } //Does not have the #

    public ColorEntity(string hexColor)
    {
        HexColor = CheckAndRemoveHashFromHexColor(hexColor);
        ConvertHexToColor(HexColor);
        ConvertColorToSvgColor(ColorColor);
    }
    public ColorEntity(Color colorColor)
    {
        ColorColor = colorColor;
        ConvertColorToHex(ColorColor);
        ConvertColorToSvgColor(ColorColor);
    }
    public ColorEntity(SvgColourServer svgColor)
    {
        SvgColor = svgColor;
        ConvertSvgColorToColor(SvgColor);
        ConvertColorToHex(ColorColor);
    }

    private void ConvertHexToColor(string color)
    {
        ColorColor = ColorTranslator.FromHtml($"#{color}");
    }
    private void ConvertColorToSvgColor(Color color)
    {
        SvgColor = new SvgColourServer() { Colour = color };
    }
    private void ConvertColorToHex(Color color)
    {
        int nColorWin32 = ColorTranslator.ToWin32(color);
        HexColor = CheckAndRemoveHashFromHexColor(string.Format("{0:X8}", nColorWin32));
    }
    private void ConvertSvgColorToColor(SvgColourServer SvgColor)
    {
        ColorColor = SvgColor.Colour;
    }

    private string CheckAndRemoveHashFromHexColor(string HexColor)
    {
        if (HexColor.StartsWith("#") == true)
        {
            HexColor = HexColor[1..];
        }
        return HexColor;
    }
}
