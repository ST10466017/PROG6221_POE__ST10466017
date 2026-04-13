using System;

public static class UI
{
    public static void DisplayHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
        ███████╗ ██████╗ ██████╗  ██████╗ 
        ╚══███╔╝██╔═══██╗██╔══██╗██╔═══██╗
          ███╔╝ ██║   ██║██████╔╝██║   ██║
         ███╔╝  ██║   ██║██╔══██╗██║   ██║
        ███████╗╚██████╔╝██║  ██║╚██████╔╝
        ╚══════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ 
        ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("      >> Zoro: Slice Through Cyber Threats <<");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("=================================================");
        Console.ResetColor();
    }

    public static string GetUserName()
    {
        Console.Write("\n[?] State your name, user: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) name = "Rookie";
        Console.Clear();
        DisplayHeader();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n[!] Welcome, {name}. Zoro stands ready.\n");
        Console.ResetColor();
        return name;
    }

    public static void PrintDivider()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        Console.ResetColor();
    }
}
