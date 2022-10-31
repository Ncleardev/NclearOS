using System;
using NclearOS.files;
using NclearOS.lib;
using NclearOS.input;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Threading;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.IO;

namespace NclearOS.sound
{
    public static class Sound
    {
        public static void Main()
        {
        start:
            switch (Input.Main("Sound", ConsoleColor.White))
            {
                case "help":
                    Console.WriteLine("Beeping doesn't work on VirtualBox\n\nSound App Help\n---------------\nnew - new sound project\nopen - play opened sound project\nsave - save current project");
                    Console.WriteLine("Press TAB to quit.");
                    goto start;
                case "new":
                    if (Files.content != null)
                    {
                        Console.WriteLine("Warning: Temp memory cleared");
                        Files.content = null;
                    }
                    Console.WriteLine("Type 'frequency/duration(ms)' and press Enter for new line. Enter q to quit.");
                loop:
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "q":
                            goto start;
                        default:
                            string[] splitit = input.Split('/');
                            int numberone = Convert.ToInt32(splitit[0]);
                            int numbertwo = Convert.ToInt32(splitit[1]);
                            Console.Beep(numberone, numbertwo);
                            Files.content += input + '\n';
                            goto loop;
                    }
                case "open":
                    Console.WriteLine(Files.inputfile);
                    Console.WriteLine("\nSelect file...");
                    Files.Main();
                    string source = Files.textfromfile;
                    Console.Write("\nFile opened\n" + source + "\nPlaying now...");
                    string[] lines = File.ReadAllLines(Files.path + "\\" + Files.inputfile);
                    foreach (string line in lines)
                    {
                        string[] splitit = line.Split('/');
                        int numberone = Convert.ToInt32(splitit[0]);
                        int numbertwo = Convert.ToInt32(splitit[1]);
                        Console.Beep(numberone, numbertwo);
                    }
                    goto start;
                case "save":
                    Files.Save();
                    goto start;
                case "system/Return":
                    if (Files.content != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Project saved in temp memory");
                    }
                    Lib.Main();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Unknown command, type help for list of commands.");
                    Console.ResetColor();
                    goto start;
            }
        }


    }
}