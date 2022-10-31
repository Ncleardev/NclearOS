using System;
using Cosmos.Core;
using System.Threading;

namespace NclearOS.loading
{
    public static class Loading
    {
        //console loading animation
        public static void Main(int duration)
        {
            while (duration > 0)
            {
                Console.Write("|");
                Console.CursorLeft--;
                Thread.Sleep(100);
                duration--;
                if (duration > 0)
                {
                    Console.Write("/");
                    Console.CursorLeft--;
                    Thread.Sleep(100);
                    duration--;
                    if (duration > 0)
                    {
                        Console.Write("-");
                        Console.CursorLeft--;
                        Thread.Sleep(100);
                        duration--;
                        if (duration > 0)
                        {
                            Console.Write("\\");
                            Console.CursorLeft--;
                            Thread.Sleep(100);
                            duration--;
                        }
                        else { break; }
                    }
                    else { break; }
                }
                else { break; }
                
                
            }
        }
    }
}