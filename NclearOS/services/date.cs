using System;
using Cosmos.HAL;

namespace NclearOS.date
{
    public static class Date
    {
        public static string Main()
        {
            return RTC.Hour + ":" + RTC.Minute + ":" + RTC.Second + "  " + RTC.DayOfTheMonth + "/" + RTC.Month + "/" + "20" + RTC.Year;
        }
        public static void Check()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("| .. |");
            Console.ResetColor();
            Console.Write(" NclearOS Clock");
            try
            {
                var time = "NclearOS booted on " + RTC.DayOfTheMonth + "/" + RTC.Month + "/" + "20" + RTC.Year + " at " + RTC.Hour + ":" + RTC.Minute + ":" + RTC.Second;             
                Console.CursorLeft = 0;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("| OK |");
                Console.ResetColor();
                Console.WriteLine(time);
            }
            catch (Exception e)
            {
                Console.CursorLeft = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("| ERR |");
                Console.WriteLine(e.ToString());
            }
        }
    }
}