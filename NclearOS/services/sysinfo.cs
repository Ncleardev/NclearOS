using Cosmos.Core;
using System;

namespace NclearOS.sysinfo
{
    public static class Sysinfo
    {
        public static void Main()
        {
            Console.WriteLine("OS: " + Kernel.CurrentVersion);
            Console.WriteLine("Console Width: " + Console.WindowWidth + " | Console Height: " + Console.WindowHeight);
            Console.WriteLine("RAM: " + CPU.GetEndOfKernel() / 1000000 + " MB / " + CPU.GetAmountOfRAM() + " MB");
            Console.WriteLine("CPU: " + CPU.GetCPUBrandString());
            Console.WriteLine("CPU Vender: " + CPU.GetCPUVendorName());
            Console.WriteLine("CPU Uptime: " + CPU.GetCPUUptime());
        }
        public static void Check()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| OK |");
            Console.ResetColor();
            Console.Write(" NclearOS System Info\n");
        }
    }
}