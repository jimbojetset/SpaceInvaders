using Microsoft.Win32;

namespace Invaders.CPU
{
    internal class Flags
    {
        private uint z; // Zero bit
        private uint s; // Sign bit
        private uint p; // Parity bit
        private uint c; // Carry bit
        private uint ac; // Auxiliary carry bit

        public Flags()
        {
            Z = 0;
            S = 0;
            P = 0;
            c = 0;
            ac = 0;
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

        public uint C
        {
            get { return c; }
            set { c = value; }
        }

        public uint AC
        {
            get { return ac; }
            set { ac = value; }
        }

        public void UpdateCarryByte(ushort value)
        {
            c = (uint)(value > 0xFF ? 1 : 0);
        }

        public void UpdateCarryWord(ushort value)
        {
            c = (uint)(value > 0xFFFF ? 1 : 0);
        }

        public void UpdateZSP(ushort value)
        {
            z = (uint)((value & 0xFF) == 0 ? 1 : 0);
            s = (uint)((value & 0x80) == 0x80 ? 1 : 0);
            p = CalculateParityFlag((ushort)(value & 0xFF));
        }

        public static uint CalculateParityFlag(ushort value)
        {
            int count = 0;
            for (int i = 0; i < 8; i++) // changed to 8 from 16
            {
                if ((value & 0x01) == 1)
                    count += 1;
                value >>= 1;
            }
            return (uint)(((count & 0x1) == 0) ? 1 : 0);
        }

        public byte ToByte()
        {
            /**
             * 7 6 5 4 3 2 1 0
             * S Z 0 A 0 P 1 C **/
             
            int flags = 0b00000010;

            if (s == 1)
                flags = flags | 0b10000000;

            if (z == 1)
                flags = flags | 0b01000000;

            if (ac == 1)
                flags = flags | 0b00010000;

            if (p == 1)
                flags = flags | 0b00000100;

            if (c == 1)
                flags = flags | 0b00000001;
            return (byte)flags;
            //return ((byte)(z | s << 1 | p << 2 | c << 3 | ac << 4));
        }

        public void SetFromByte(byte flags)
        {
            if (0x01 == (flags & 0x01)) z = 0x01; else z = 0x00;
            if (0x02 == (flags & 0x02)) s = 0x01; else s = 0x00;
            if (0x04 == (flags & 0x04)) p = 0x01; else p = 0x00;
            if (0x08 == (flags & 0x08)) c = 0x01; else c = 0x00;
            if (0x10 == (flags & 0x10)) ac = 0x01; else ac = 0x00;


            /*
            s = (uint)((flags & 0b10000000) == 0b10000000 ? 1 : 0);
            z = (uint)((flags & 0b01000000) == 0b01000000 ? 1 : 0);
            ac = (uint)((flags & 0b00010000) == 0b00010000 ? 1 : 0);
            p = (uint)((flags & 0b00000100) == 0b00000100 ? 1 : 0);
            c = (uint)((flags & 0b00000001) == 0b00000001 ? 1 : 0);*/
        }
    }
}