using NclearOS.input;
using System;
using System.Collections.Generic;

namespace NclearOS.shell
{
    public static class Shell
    {
        public static void Main()
        {
            nclearos.NclearOS.Main(Input.Main("", ConsoleColor.Green));
        }
        public static void Check()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| OK |");
            Console.ResetColor();
            Console.Write(" NclearOS Shell\n");
        }
    }
}