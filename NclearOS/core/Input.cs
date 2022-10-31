using System;
using System.Collections.Generic;

namespace NclearOS.input
{
    public static class Input
    {
        private static List<string> history = new List<string>();
        private static string input;

        public static string Main(string AppName, ConsoleColor TextColor)
        {
            Console.ForegroundColor = TextColor;
            Console.Write("\n" + AppName + "> ");
            input = "";
            int position = 0;
            while (true)
            {
                ConsoleKeyInfo name = Console.ReadKey();
                switch (name.Key)
                {
                    case ConsoleKey.Escape:
                        Console.ResetColor();
                        Console.WriteLine();
                        return null;
                    case ConsoleKey.Enter:
                        Console.ResetColor();
                        Console.WriteLine();
                        history.Add(input);
                        return input;
                    case ConsoleKey.UpArrow:
                        if (history.Count - position > 0)
                        {
                            position++;
                            Console.CursorLeft = 0;
                            Console.Write(new string(' ', Console.WindowWidth - 1));
                            Console.CursorTop--;
                            Console.Write("\n" + AppName + "> ");
                            input = history[history.Count - position];
                            Console.Write(input);
                        }
                        break;
                    case ConsoleKey.Home:
                        Console.ResetColor();
                        Console.WriteLine();
                        return "system/Return";
                    case ConsoleKey.Tab:
                        Console.ResetColor();
                        Console.WriteLine();
                        return "system/Return";
                    case ConsoleKey.F1:
                        Console.ResetColor();
                        Console.WriteLine();
                        return "help";
                    case ConsoleKey.DownArrow:
                        if (position > 1)
                        {
                            position--;
                            Console.CursorLeft = 0;
                            Console.Write(new string(' ', Console.WindowWidth - 1));
                            Console.CursorTop--;
                            Console.Write("\n" + AppName + "> ");
                            input = history[history.Count - position];
                            Console.Write(input);
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (Console.CursorLeft > AppName.Length + 2)
                        {
                            Console.CursorLeft--;
                            Console.Write(" ");
                            input = input.Remove(input.Length - 1);
                            Console.CursorLeft--;
                        }
                        break;
                    default:
                        input += name.KeyChar;
                        switch (input)
                        {
                            case "system/Home" or "system/Return":
                                Console.CursorLeft = 0;
                                Console.Write(new string(' ', Console.WindowWidth - 1));
                                Console.CursorTop--;
                                Console.CursorTop--;
                                Console.Write("\n" + AppName + "> ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write(input);
                                break;
                            default:
                                
                                break;
                        } 
                        break;
                }
            }
        }

        public static void Check()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| OK |");
            Console.ResetColor();
            Console.Write(" NclearOS Input Manager\n");
        }
    }
}