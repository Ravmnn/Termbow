using System.Text;
using System.Linq;
using System.Collections.Generic;

using Fint;

using Termbow.Ansi;


namespace Termbow.Color.Paint;


public class FintPainter(Scanner fintScanner, Dictionary<int, ColorObject> colorTable) : Painter
{
    public Scanner Scanner => fintScanner;
    public Dictionary<int, ColorObject> ColorTable { get; set; } = colorTable;


    public override string Paint(string source)
    {
        var result = new StringBuilder(source);
        var tokens = Scanner.Scan(new Lexer(source).Tokenize());

        foreach (var token in tokens.Reverse())
        {
            if (token.Id is null || !ColorTable.TryGetValue(token.Id.Value, out var color))
                continue;

            result.Insert(token.End, EscapeCodes.Reset);
            result.Insert(token.Start, color.AsSequence());
        }

        return result.ToString();
    }
}
