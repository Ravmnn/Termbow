using System;
using System.Collections.Generic;
using System.IO;

using Fint;

using Termbow.Color;
using Termbow.Color.Paint;


namespace TermbowTest;


class Program
{
    static void Main()
    {
        var rules = new[]
        {
            new MatchRule(1, "red"), new MatchRule(2, "green"),
            new MatchRule(3, "blue"), new MatchRule(4, "violet")
        };

        var colorTable = new Dictionary<int, ColorObject>
        {
            {1, ColorValue.FGRed},
            {2, ColorValue.FGGreen},
            {3, ColorValue.FGBlue},
            {4, ColorValue.FGMagenta}
        };

        var painter = new FintPainter(new Scanner(rules), colorTable);
        var source = File.ReadAllText("../../../test.txt");

        Console.WriteLine(painter.Paint(source));

        Console.ReadKey();
    }
}
