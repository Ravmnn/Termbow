using System;
using System.Collections.Generic;
using System.Linq;


namespace Termbow.Color;


/// <summary>
/// Specifies how the pattern should behave when the ColorObject list reaches
/// at its end.
/// </summary>
public enum ResetMode
{
    /// <summary>
    /// Returns to the begin of the list.
    /// </summary>
    FromBeginning,

    /// <summary>
    /// Reverts the list indexing direction. If it's going from left-to-right, then
    /// the direction is changed to right-to-left.
    /// </summary>
    Revert
}


/// <summary>
/// A pattern of ColorObjects. A PatternPainter uses this object to perform painting.
/// </summary>
/// <param name="colors"> The ColorObjects as a List of ColorPattern.Color. </param>
public struct ColorPattern(List<ColorPattern.Color> colors)
{
    /// <summary>
    /// Stores useful data with the ColorObject.
    /// </summary>
    /// <param name="colorObject"> The ColorObject. </param>
    /// <param name="length"> The length of the pattern. </param>
    public struct Color
    {
        public ColorObject ColorObject { get; set; }
        public uint Length { get; set; }


        public Color(ColorObject color, uint length)
        {
            ColorObject = color;
            Length = length;
        }
    }


    /// <summary>
    /// The ColorPattern.Color list.
    /// </summary>
    public List<Color> Colors { get; set; } = colors;

    /// <summary>
    /// Array of characters to be ignored by the pattern.
    /// </summary>
    public char[] IgnoreChars { get; set; } = [' '];

    public ResetMode ResetMode { get; set; } = ResetMode.FromBeginning;


    /// <param name="colors"> The ColorPattern.Color list. </param>
    /// <param name="colorLength"> The length to use for all the colors. </param>
    public ColorPattern(List<ColorObject> colors, uint colorLength = 1)
        : this([])
    {
        Colors = (from color in colors select new Color(color, colorLength)).ToList();
    }
}
