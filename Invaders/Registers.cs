namespace SpaceInvaders
{
    internal class Registers
    {
        //const int INTERRUPT_ENABLED = 1;
        //const int INTERRUPT_DISABLED = 0;
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
        private Flags ?flags = new Flags();
        public byte[] memory = new byte[0x10000]; // = 8k bytes of memory = 65536 bits

        public Registers()
        {
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

        public bool INT_ENABLE
        {
            get { return int_enable; }
            set { int_enable = value; }
        }

        public ulong HL
        {
            get { return (ulong)this.h << 8 | (ulong)this.l; }
            set
            {
                this.h = (byte)((value & 0xFF00) >> 8);
                this.l = (byte)(value & 0x00FF);
            }
        }

        public ulong DE
        {
            get { return (ulong)this.d << 8 | (ulong)this.e; }
            set
            {
                this.d = (byte)((value & 0xFF00) >> 8);
                this.e = (byte)(value & 0x00FF);
            }
        }

        public ulong BC
        {
            get { return (ulong)this.b << 8 | (ulong)this.c; }
            set
            {
                this.b = (byte)((value & 0xFF00) >> 8);
                this.c = (byte)(value & 0x00FF);
            }
        }

    }
}
