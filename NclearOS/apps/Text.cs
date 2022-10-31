using System;
using NclearOS.files;
using NclearOS.lib;
using NclearOS.input;

namespace NclearOS.text
{
    public static class Text
    {
        public static void Main()
        {
            text:
            switch (Input.Main("Text", ConsoleColor.White))
            {
                case "help":
                    Console.WriteLine("Text App Help\n---------------\nnew - new text file\nopen - open text file\nsave - save current input to text file");
                    Console.WriteLine("Press TAB to quit.");
                    goto text;
                case "new":
                    if (Files.content != null)
                    {
                        Console.WriteLine("Warning: Temp memory cleared");
                        Files.content = null;
                    }
                    Files.content = Console.ReadLine();
                    goto text;
                case "open":
                    Console.WriteLine("\nSelect file...");
                    Files.Main();
                    Console.Write("\nFile opened\n" + Files.textfromfile);
                    Files.content = Console.ReadLine();
                    goto text;
                case "save":
                    Files.Save();
                    goto text;
                case "system/Return":
                    if (Files.content != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Text saved in temp memory");
                    }
                    Lib.Main();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Unknown command, type help for list of commands.");
                    Console.ResetColor();
                    goto text;
            }
        }

    }
}