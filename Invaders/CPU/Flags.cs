using Microsoft.Win32;

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
            p = (uint)CalculateParityFlag(value & 0xFF);
        }

        public int CalculateParityFlag(ulong value)
        {
            int count = 0;
            for (int i = 0; i < 16; i++)
            {
                if ((value & 0x01) == 1)
                    count += 1;
                value >>= 1;
            }
            return (0 == (count & 0x1)) ? 1 : 0;
        }

        public byte ToByte()
        {
            /**
             * 7 6 5 4 3 2 1 0
             * S Z 0 A 0 P 1 C
             */
            var flags = 0b00000000;

            if (s == 1)
                flags = flags | 0b10000000;

            if (z == 1)
                flags = flags | 0b01000000;

            if (ac == 1)
                flags = flags | 0b00010000;

            if (p == 1)
                flags = flags | 0b00000100;

            if (cy == 1)
                flags = flags | 0b00000001;

            return (byte)flags;
        }

        public void SetFromByte(byte flags)
        {
            s = (uint)(((flags & 0b10000000) == 0b10000000) ? 1 : 0);
            z = (uint)(((flags & 0b01000000) == 0b01000000) ? 1 : 0);
            ac = (uint)(((flags & 0b00010000) == 0b00010000) ? 1 : 0);
            p = (uint)(((flags & 0b00000100) == 0b00000100) ? 1 : 0);
            cy = (uint)(((flags & 0b00000001) == 0b00000001) ? 1 : 0);
        }
    }
}