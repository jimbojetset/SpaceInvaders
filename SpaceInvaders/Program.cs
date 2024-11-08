using SpaceInvaders.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Program
    {
        static _8080CPU cpu = new _8080CPU();

        static void Main(string[] args)
        {
            cpu.Initialize(0xffff);
            Load_ROM_Into_RAM(@"data\invaders.h", 0x0000);
            Load_ROM_Into_RAM(@"data\invaders.g", 0x0800);
            Load_ROM_Into_RAM(@"data\invaders.f", 0x1000);
            Load_ROM_Into_RAM(@"data\invaders.e", 0x1800);
        }

        private static void Load_ROM_Into_RAM(string filePath, long start_address)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader br = new BinaryReader(fs);
                var progSize = new FileInfo(filePath).Length;
                byte[] rom = br.ReadBytes((int)progSize);
                cpu.memory.Load(start_address, rom);
            }
        }
    }
}
