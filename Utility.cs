using System;
using System.Globalization;
using System.Threading;

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

    public static string ReadPassword()
    {
        string password = "";
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    return null;
                case ConsoleKey.Enter:
                    return password;
                case ConsoleKey.Backspace:
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                    break;
                default:
                    password += key.KeyChar;
                    Console.Write("*");
                    break;
            }
        }
    }

    public static void printDotAnimation(int timer = 20)
    {
        Console.WriteLine("");
        for (var x = 0; x < timer; x++)
        {
            Console.Write(".");
            Thread.Sleep(100);
        }
        Console.WriteLine();
    }
}