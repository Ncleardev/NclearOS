using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Threading;
using NclearOS.lib;
using NclearOS.input;

namespace NclearOS.calc
{
    public static class Calc
    {
        public static void Domath(string input)
        {
            if (input.Contains('+'))
            {
                string[] splitit = input.Split('+');
                var numberone = Convert.ToDouble(splitit[0]);
                var numbertwo = Convert.ToDouble(splitit[1]);
                Console.Write(" = ");
                Console.Write(numberone + numbertwo);
                Calc.Main();
            }
            else if (input.Contains('-'))
            {
                string[] splitit = input.Split('-');
                var numberone = Convert.ToDouble(splitit[0]);
                var numbertwo = Convert.ToDouble(splitit[1]);
                Console.Write(" = ");
                Console.Write(numberone - numbertwo);
                Calc.Main();
            }
            else if (input.Contains("**"))
            {
                string[] splitit = input.Split("**");
                var numberone = Convert.ToDouble(splitit[0]);
                var numbertwo = Convert.ToDouble(splitit[1]);
                Console.Write(" = ");
                Console.Write(Math.Pow(numberone, numbertwo));
                Calc.Main();
            }
            else if (input.Contains('*'))
            {
                string[] splitit = input.Split('*');
                var numberone = Convert.ToDouble(splitit[0]);
                var numbertwo = Convert.ToDouble(splitit[1]);
                Console.Write(" = ");
                Console.Write(numberone * numbertwo);
                Calc.Main();
            }
            else if (input.Contains('%'))
            {
                string[] splitit = input.Split('%');
                var numberone = Convert.ToDouble(splitit[0]);
                var numbertwo = Convert.ToDouble(splitit[1]);
                Console.Write(" = ");
                Console.Write(numberone % numbertwo);
                Calc.Main();
            }
            else if (input.Contains('/'))
            {
                string[] splitit = input.Split('/');
                var numberone = Convert.ToDouble(splitit[0]);
                var numbertwo = Convert.ToDouble(splitit[1]);
                Console.Write(" = ");
                Console.Write(numberone / numbertwo);
                Calc.Main();
            }
            else
            {
                Calc.Wrong("Wrong Usage");
            }
        }

        public static void Main()
        {
            string input = Input.Main("Calc", ConsoleColor.White);
            switch (input)
            {
                case "system/Return":
                    break;
                case "help":
                    Console.WriteLine("Example: 'number' (operation) 'number'\n2+2\n6-3\n4*3\n12/3\n2**7\n15%4");
                    Console.WriteLine("Press TAB to quit.");
                    Calc.Main();
                    break;
                default:
                    try
                    {
                        Calc.Domath(input);
                    }
                    catch (Exception e)
                    {
                        Calc.Wrong(Convert.ToString(e));
                    }
                    break;
            }
        }
        public static void Wrong(string e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.WriteLine("Example: 'number' (operation) 'number'\n2+2\n6-3\n4*3\n12/3\n2**7\n15%4");
            Console.ForegroundColor = ConsoleColor.White;
            Calc.Main();
        }

    }
}