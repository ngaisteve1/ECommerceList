using System;
using System.Globalization;

public static class Utility
{
    private static CultureInfo culture = new CultureInfo("ms-MY");

    public static void PrintMessage(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static string FormatAmount(this decimal amt)
    {
        return string.Format(culture, "{0:C2}", amt);
    }
}