using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Threading;
using NclearOS.calc;
using NclearOS.text;
using NclearOS.sound;
using NclearOS.files;

namespace NclearOS.lib
{
    public static class Lib
    {
        public static void Main()
        {
            start:
            Console.ResetColor();
            Console.WriteLine("\n1 - Calculator\n2 - Text Editor\n3 - File Manager\n4 - Sound Playground");
            Console.Write("\nLibrary> ");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Calculator");
                    try { Calc.Main(); }
                    catch (Exception e) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("App 'Calc' crashed\n" + e); }
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Text Editor");
                    try { Text.Main(); }
                    catch (Exception e) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("App 'Text' crashed\n" + e); }
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("File Manager");
                    try { Files.Main(); }
                    catch (Exception e) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n\nApp 'Files' crashed\n" + e); }
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Sound Playground");
                    try { Sound.Main(); }
                    catch (Exception e) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n\nApp 'Sound' crashed\n" + e); }
                    break;
                case ConsoleKey.Tab:
                    break;
                case ConsoleKey.F1:
                    Console.WriteLine("\nEnter number to launch app. Press TAB to quit library (or app if launched).");
                    goto start;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUnknown command, press F1 for help.");
                    goto start;
            }
        }
        public static void Boot()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| OK |");
            Console.ResetColor();
            Console.Write(" NclearOS Apps Library\n");
        }
    }
}