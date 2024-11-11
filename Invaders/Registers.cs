using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Registers
    {
        const int INTERRUPT_ENABLED = 1;
        const int INTERRUPT_DISABLED = 0;
        private byte a;
        private byte b;
        private byte c;
        private byte d;
        private byte e;
        private byte h;
        private byte l;
        private ushort sp;
        private ushort pc;
        private byte int_enable;
        private Flags ?flags;
        public byte[] memory; //16K

        public Registers()
        {
            this.Flags = new Flags();
            this.memory = new byte[0x10000]; // = 8k bytes of memory = 65536 bits
            this.INT_ENABLE = INTERRUPT_ENABLED;
        }

        public byte A
        {
            get { return this.a; }
            set { this.a = value; }
        }

        public byte B
        {
            get { return this.b; }
            set { this.b = value; }
        }

        public byte C
        {
            get { return this.c; }
            set { this.c = value; }
        }

        public byte D
        {
            get { return this.d; }
            set { this.d = value; }
        }

        public byte E
        {
            get { return this.e; }
            set { this.e = value; }
        }

        public byte H
        {
            get { return this.h; }
            set { this.h = value; }
        }

        public byte L
        {
            get { return this.l; }
            set { this.l = value; }
        }

        public ushort SP
        {
            get { return this.sp; }
            set { this.sp = value; }
        }

        public ushort PC
        {
            get { return this.pc; }
            set { this.pc = value; }
        }

        public Flags Flags
        {
            get { return flags!; }
            set { flags = value; }
        }

        public byte INT_ENABLE
        {
            get { return int_enable; }
            set { int_enable = value; }
        }

        public ulong HL
        {
            get { return (ulong)this.H << 8 | (ulong)this.L; }
            set
            {
                this.h = (byte)((value & 0xFF00) >> 8);
                this.l = (byte)(value & 0x00FF);
            }
        }

        public ulong DE
        {
            get { return (ulong)this.D << 8 | (ulong)this.E; }
            set
            {
                this.d = (byte)((value & 0xFF00) >> 8);
                this.e = (byte)(value & 0x00FF);
            }
        }

        public ulong BC
        {
            get { return (ulong)this.B << 8 | (ulong)this.C; }
            set
            {
                this.b = (byte)((value & 0xFF00) >> 8);
                this.c = (byte)(value & 0x00FF);
            }
        }

    }
}
