using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace Termbow.Color.Paint;


/// <summary>
/// A Painter for ColorPatterns.
/// </summary>
/// <param name="pattern"> The pattern to be used. </param>
public class PatternPainter(ColorPattern pattern) : Painter
{
    public ColorPattern Pattern { get; set; } = pattern;
    public List<ColorPattern.Color> Colors => Pattern.Colors;

    private ColorPattern.Color CurrentColor => Colors[_colorIndex];


    private int _charIndex;
    private int _colorIndex;
    private int _currentLength;


    // Restart color index when it reaches the colors size.
    private void ResetColorIndex()
    {
        if (Pattern.ResetMode == ResetMode.Revert)
            Colors.Reverse();

        _colorIndex = 0;
    }

    private bool ShouldIgnore(char ch)
        => CurrentColor.Length == 0 || Pattern.IgnoreChars.Contains(ch);

    private void NextColor()
    {
        _currentLength = 1;
        _colorIndex++;
    }


    public override string Paint(string source)
    {
        var builder = new StringBuilder();
        _currentLength = 0;

        for (_charIndex = 0, _colorIndex = 0; _charIndex < source.Length; _charIndex++)
        {
            if (_colorIndex >= Colors.Count)
                ResetColorIndex();

            var ch = source[_charIndex];

            if (ShouldIgnore(ch))
                builder.Append(ch);

            else
            {
                // appends the painted character
                builder.Append(CurrentColor.ColorObject.AsSequence() + ch);

                if (++_currentLength >= CurrentColor.Length)
                    NextColor();
            }
        }

        builder.Append(SequenceFinisher);

        return builder.ToString();
    }
}
