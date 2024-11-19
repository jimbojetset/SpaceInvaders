namespace SpaceInvaders
{
    internal class Registers
    {
        private byte a = 0;
        private byte b = 0;
        private byte c = 0;
        private byte d = 0;
        private byte e = 0;
        private byte h = 0;
        private byte l = 0;
        private ushort sp = 0;
        private ushort pc = 0;
        private bool int_enable = false;
        private Flags? flags = new();

        public Registers()
        {
            a = 0;
            b = 0;
            c = 0;
            d = 0;
            e = 0;
            h = 0;
            l = 0;
            sp = 0;
            pc = 0;
            int_enable = false;
            Flags? flags = new();
        }

        public byte A
        {
            get { return a; }
            set { a = value; }
        }

        public byte B
        {
            get { return b; }
            set { b = value; }
        }

        public byte C
        {
            get { return c; }
            set { c = value; }
        }

        public byte D
        {
            get { return d; }
            set { d = value; }
        }

        public byte E
        {
            get { return e; }
            set { e = value; }
        }

        public byte H
        {
            get { return h; }
            set { h = value; }
        }

        public byte L
        {
            get { return l; }
            set { l = value; }
        }

        public ushort SP
        {
            get { return sp; }
            set { sp = value; }
        }

        public ushort PC
        {
            get { return pc; }
            set { pc = value; }
        }

        public Flags Flags
        {
            get { return flags!; }
            set { flags = value; }
        }

        public bool INT_ENABLE
        {
            get { return int_enable; }
            set { int_enable = value; }
        }

        public ulong HL
        {
            get
            {
                return (ulong)h << 8 | (ulong)l;
            }
            set
            {
                h = (byte)((value & 0xFF00) >> 8);
                l = (byte)(value & 0x00FF);
            }
        }

        public ulong DE
        {
            get
            {
                return (ulong)d << 8 | (ulong)e;
            }
            set
            {
                d = (byte)((value & 0xFF00) >> 8);
                e = (byte)(value & 0x00FF);
            }
        }

        public ulong BC
        {
            get
            {
                return (ulong)b << 8 | (ulong)c;
            }
            set
            {
                b = (byte)((value & 0xFF00) >> 8);
                c = (byte)(value & 0x00FF);
            }
        }
    }
}