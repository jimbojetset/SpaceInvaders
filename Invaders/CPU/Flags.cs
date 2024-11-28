namespace Invaders.CPU
{
    internal class Flags
    {
        private uint z; // Zero bit
        private uint s; // Sign bit
        private uint p; // Parity bit
        private uint cy; // Carry bit
        private uint ac; // Auxiliary carry bit
        private uint pad;

        public Flags()
        {
            Z = 0;
            S = 0;
            P = 0;
            cy = 0;
            ac = 0;
            pad = 3;
        }

        public uint Z
        {
            get { return z; }
            set { z = value; }
        }

        public uint S
        {
            get { return s; }
            set { s = value; }
        }

        public uint P
        {
            get { return p; }
            set { p = value; }
        }

        public uint CY
        {
            get { return cy; }
            set { cy = value; }
        }

        public uint AC
        {
            get { return ac; }
            set { ac = value; }
        }

        public uint Pad
        {
            get { return pad; }
            set { pad = value; }
        }

        public void UpdateCarryByte(ulong value)
        {
            cy = (uint)((value > 0x00FF) ? 1 : 0);
        }

        public void UpdateCarryWord(ulong value)
        {
            cy = (uint)((value > 0xFFFF) ? 1 : 0);
        }

        public void UpdateZSP(ulong value)
        {
            z = (uint)(((value & 0xFF) == 0) ? 1 : 0);
            s = (uint)(((value & 0x80) == 0x80) ? 1 : 0);
            p = CalculateParityFlag((byte)value);
        }

        public void UpdateAuxCarryFlag(byte a, byte b)
        {
            ac = (uint)((((a & 0x0f) + (b & 0x0f)) > 0x0f) ? 1 : 0);
        }

        public void UpdateAuxCarryFlag(byte a, byte b, byte c)
        {
            ac = (uint)((((a & 0x0f) + (b & 0x0f) + (c & 0x0f)) > 0x0f) ? 1 : 0);
        }

        public static uint CalculateParityFlag(byte value)
        {
            // 1 is even
            byte num = (byte)(value & 0xff);
            byte total;
            for (total = 0; num > 0; total++)
                num &= (byte)(num - 1);
            return (uint)(((total % 2) == 0) ? 1 : 0);
        }

        public byte ToByte()
        {
            /*       0   0   1
            /*   7 6 5 4 3 2 1 0
                 S Z   A   P   C 
            */
            var flags = 0b00000010;
            if (s == 1)
                flags = flags | 0b10000010;
            if (z == 1)
                flags = flags | 0b01000010;
            if (ac == 1)
                flags = flags | 0b00010010;
            if (p == 1)
                flags = flags | 0b00000110;
            if (cy == 1)
                flags = flags | 0b00000011;
            return (byte)flags;
        }
    }
}