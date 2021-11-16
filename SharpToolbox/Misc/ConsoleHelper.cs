using System;
using System.Collections.Generic;

namespace SharpToolbox.Misc;

public static class ConsoleHelper
{
    /// <summary>
    /// Write line
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="consoleColor">Message Color</param>
    public static void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.White)
    {
        Console.ResetColor();
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Ask a Yes/No question
    /// </summary>
    /// <param name="question">Ask</param>
    /// <param name="yesChar">Yes Char</param>
    /// <param name="noChar">No Char</param>
    /// <param name="foreColor">Ask Color</param>
    /// <returns>true for yes, false for no</returns>
    public static bool Ask(string question, string yesChar = "Y", string noChar = "N", ConsoleColor foreColor = ConsoleColor.White)
    {
        Console.Write($"{question} {yesChar}/{noChar}? ");
        string input;
        ICollection<string> chars = new List<string> {yesChar, noChar};
        while (!chars.Contains(input = Console.ReadLine()))
            Console.Write($"{question} {yesChar}/{noChar}? ");
        return input == yesChar;
    }
}