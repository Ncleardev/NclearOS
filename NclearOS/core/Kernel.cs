using System;
using Sys = Cosmos.System;
using System.Threading;
using NclearOS.files;
using NclearOS.lib;
using NclearOS.date;
using NclearOS.loading;
using NclearOS.sysinfo;
using NclearOS.input;
using NclearOS.shell;

namespace NclearOS
{
    public class Kernel : Sys.Kernel
    {
        public static string CurrentVersion = "NclearOS Alpha 0.1";
        protected override void BeforeRun()
        {
            KernelBoot();
        }
        protected override void Run()
        {
            try
            {
                Shell.Main();
            }
            catch (Exception e)
            {
                ErrorScreen(Convert.ToString(e));
            }
        }
        public static void ErrorScreen(string errormsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false;
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i <= Console.WindowHeight; i++)
            {
                Thread.Sleep(50);
                Console.WriteLine();
            }
            Console.Clear();
            Console.WriteLine(CurrentVersion + "\n\nCritical System Error\n\n" + errormsg);
            Thread.Sleep(1000);
            Console.Write("\nRestarting computer in 5 seconds...");
            Loading.Main(10);
            Console.CursorLeft = 0;
            Console.Write("Restarting computer in 4 seconds...");
            Loading.Main(10);
            Console.CursorLeft = 0;
            Console.Write("Restarting computer in 3 seconds...");
            Loading.Main(10);
            Console.CursorLeft = 0;
            Console.Write("Restarting computer in 2 seconds...");
            Loading.Main(10);
            Console.CursorLeft = 0;
            Console.Write("Restarting computer in 1 second....");
            Loading.Main(5);
            Console.Clear();
            Loading.Main(5);
            Sys.Power.Reboot();
        }
        public static void KernelBoot()
        {
            //Boot sequence
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorLeft = 0;
            Console.Write("Booting NclearOS...\n\n");
            //Checking system integrity
            nclearos.NclearOS.Boot();
            Input.Check();
            Shell.Check();
            Lib.Boot();
            Files.Check();
            Sysinfo.Check();
            Date.Check();
            Console.Write("\nWelcome to NclearOS");
            Console.Beep(300, 200);
            Thread.Sleep(300);
            Console.Beep(400, 100);
            Thread.Sleep(100);
            Console.Beep(500, 100);
            Thread.Sleep(100);
            Console.Beep(600, 100);
            Thread.Sleep(100);
            Console.Beep(700, 100);
            Console.CursorLeft = 0;
            Console.Write("Press any key to continue");
            Console.ReadKey(true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(CurrentVersion);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public static void KernelScreen()
        {
            Console.ResetColor();
            Console.Clear();
            Console.Write("Press Enter to boot NclearOS");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            KernelBoot();
        }
    }
}