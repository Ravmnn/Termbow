﻿using System.Linq;

using Termbow.Ansi;


namespace Termbow.Color;


/// <summary>
/// Represents a sequence with both foreground, background and color mode of a text.
/// </summary>
/// <param name="fg"> The foreground element. </param>
/// <param name="bg"> The background element. </param>
/// <param name="mode"> The color mode. </param>
public class ColorObject(SequenceElement? fg, SequenceElement? bg, ColorMode? mode)
{
    public static ColorObject None => new ColorObject(null, null, null);


    public SequenceElement? Foreground { get; set; } = fg;
    public SequenceElement? Background { get; set; } = bg;
    public ColorMode? Mode { get; set; } = mode;



    /// <returns> A Color16-initialized ColorObject. </returns>
    /// <param name="fg"> The foreground color code. </param>
    /// <param name="bg"> The background color code. </param>
    /// <param name="mode"> The color mode. </param>
    public static ColorObject FromColor16(Color16? fg = null, Color16? bg = null, ColorMode? mode = null) =>
        new ColorObject(new ColorCodeElement(fg), new ColorCodeElement(bg), mode);


    /// <returns> A Color256-initialized ColorObject. </returns>
    /// <param name="fg"> The foreground color code. </param>
    /// <param name="bg"> The background color code. </param>
    /// <param name="mode"> The color mode. </param>
    public static ColorObject FromColor256(byte? fg = null, byte? bg = null, ColorMode? mode = null) =>
        new ColorObject(new Color256Element(fg), new Color256Element(bg, ColorLayer.Background), mode);



    /// <returns> A ColorRGB-initialized ColorObject. </returns>
    /// <param name="fg"> The foreground color code. </param>
    /// <param name="bg"> The background color code. </param>
    /// <param name="mode"> The color mode. </param>
    public static ColorObject FromColorRGB(ColorRGB? fg = null, ColorRGB? bg = null, ColorMode? mode = null) =>
        new ColorObject(new ColorRGBElement(fg), new ColorRGBElement(bg, ColorLayer.Background), mode);



    /// <returns> A ColorMode-initialized ColorObject. </returns>
    /// <param name="mode"> The color mode. </param>
    public static ColorObject FromColorMode(ColorMode? mode = null) => new ColorObject(null, null, mode);



    // Use the ColorValue static members if Color16 is needed.

    public static implicit operator ColorObject(byte fg) => FromColor256(fg);
    public static implicit operator ColorObject(ColorRGB fg) => FromColorRGB(fg);
    public static implicit operator ColorObject(ColorMode mode) => FromColorMode(mode);





    /// <summary>
    /// Try to merge two ColorObject properties. The Left-sided ColorObject have a higher priority.
    /// </summary>
    /// <param name="left"> The left-sided ColorObject. </param>
    /// <param name="right"> The right-sided ColorObject. </param>
    /// <returns> A new ColorObject with the properties merged. </returns>
    public static ColorObject operator +(ColorObject left, ColorObject right) =>
        new ColorObject(fg: left.Foreground?.IsValid() is true ? left.Foreground : right.Foreground,
            bg: left.Background?.IsValid() is true ? left.Background : right.Background, mode: left.Mode ?? right.Mode);



    /// <returns> This ColorObject converted into an string sequence. </returns>
    public string AsSequence()
        => SequenceBuilder.BuildEscapeSequence([
            ((int?)Mode)?.ToString(), Foreground?.BuildSequenceIfValid(), Background?.BuildSequenceIfValid(),
        ]);
}
