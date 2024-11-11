using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Program
    {
        static _8080CPU cpu;
        static Thread cpu_thread;
        static Thread display_thread;

        static void Main(string[] args)
        {
            cpu = new _8080CPU();
            cpu.ReadROM(@"invaders.rom");
            cpu_thread = new Thread(() => cpu.RunEmulation());
            while (!cpu.Running) { }
            display_thread = new Thread(() => display_thread.Start());
            Console.WriteLine("Program Has Exited Gracefully");
            Console.ReadKey();
        }

        static void Display()
        {
            while (cpu != null && cpu.Running)
            {
                while (!cpu.DisplayReady) { }
                byte[] video = cpu.GetVideoRam();

            }
        }
    }
}
