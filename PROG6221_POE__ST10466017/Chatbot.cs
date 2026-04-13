using System;

public class Chatbot
{
    private string userName;

    public Chatbot(string name)
    {
        userName = name;
    }

    public void Start()
    {
        Console.WriteLine("Type 'help' to see what I can do or 'exit' to leave.");
        while (true)
        {
            UI.PrintDivider();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{userName} >> ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Empty strike. Say something.");
                continue;
            }

            if (input == "exit")
            {
                Console.WriteLine("\nZoro out. Stay sharp, stay safe.");
                break;
            }

            Respond(input);
        }
    }

    private void Respond(string input)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        if (input.Contains("how are you"))
            Console.WriteLine("Three-sword style ready. I'm alert and active.");
        else if (input.Contains("purpose") || input.Contains("what can i ask"))
            Console.WriteLine("Ask me about: passwords, phishing, or safe browsing.");
        else if (input.Contains("password"))
            Console.WriteLine("[SLASH] Use a different password for every account. No exceptions.");
        else if (input.Contains("phishing"))
            Console.WriteLine("[CUT] Never click suspicious links. Verify first, then trust.");
        else if (input.Contains("safe browsing") || input.Contains("browsing"))
            Console.WriteLine("[GUARD] HTTPS only. No sketchy downloads.");
        else if (input.Contains("help"))
            Console.WriteLine("Topics: password, phishing, safe browsing, how are you, purpose");
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I didn't understand that command. Try 'help'.");
        }
        Console.ResetColor();
    }
}
