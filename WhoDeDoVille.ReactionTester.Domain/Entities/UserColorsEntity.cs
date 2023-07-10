using Svg;
using Color = System.Drawing.Color;

namespace WhoDeDoVille.ReactionTester.Domain.Entities;

/// <summary>
/// Takes 3 colors and converts them to 3 different types.
/// Color, SvgColourServer, Hex.
/// </summary>
public class UserColorsEntity
{
    private List<ColorEntity> _boardColors = new List<ColorEntity>();

    public UserColorsEntity(string Color1, string Color2, string Color3)
    {
        InitializeColors(Color1, Color2, Color3);
    }

    private void InitializeColors(string Color1, string Color2, string Color3)
    {
        _boardColors.Add(new ColorEntity(Color1));
        _boardColors.Add(new ColorEntity(Color2));
        _boardColors.Add(new ColorEntity(Color3));
    }

    public SvgColourServer GetUserSvgColor(ColorsEnum colorEnum)
    {
        return GetUserBoardColor(colorEnum).SvgColor;
    }
    public Color GetUserColorColor(ColorsEnum colorEnum)
    {
        return GetUserBoardColor(colorEnum).ColorColor;
    }
    public string GetUserHexColor(ColorsEnum colorEnum)
    {
        return GetUserBoardColor(colorEnum).HexColor;
    }

    private ColorEntity GetUserBoardColor(ColorsEnum colorEnum)
    {
        return _boardColors[(int)colorEnum];
    }
}
