using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplicationTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer tmrx = new System.Timers.Timer();
            tmrx.Interval = 2*1000; //set interval of checking here
            tmrx.Elapsed += delegate
            {
                tmrx_func();
            };

            tmrx.Start();
            Console.ReadKey(true);
        }

        private static void tmrx_func()
        {
            Console.WriteLine(DateTime.Now.ToString());
        }
    }
}
