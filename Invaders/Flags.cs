using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
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
            this.Z = 0;
            this.S = 0;
            this.P = 0;
            this.cy = 0;
            this.ac = 0;
            this.pad = 3;
        }

        public uint Z
        {
            get { return this.z; }
            set { this.z = value; }
        }

        public uint S
        {
            get { return this.s; }
            set { this.s = value; }
        }

        public uint P
        {
            get { return this.p; }
            set { this.p = value; }
        }

        public uint CY
        {
            get { return this.cy; }
            set { this.cy = value; }
        }

        public uint AC
        {
            get { return this.ac; }
            set { this.ac = value; }
        }

        public uint Pad
        {
            get { return this.pad; }
            set { this.pad = value; }
        }

        public void UpdateByteCY(ulong value)
        {
            this.CY = (uint)((value > 0xFF) ? 1 : 0);
        }

        public void UpdateWordCY(ulong value)
        {
            this.CY = (uint)((value > 0xFFFF) ? 1 : 0);
        }

        public void UpdateZSP(uint value)
        {

            this.Z = (uint)(((value & 0xFF) == 0) ? 1 : 0);
            this.S = (uint)(((value & 0x80) == 0) ? 1 : 0);
            this.P = CalculateParityFlag(value);
        }

        private uint CalculateParityFlag(uint value)
        {
            int x = 0;
            for (int i = 0; i < 8; i++)
                if ((value & (1 << i)) > 0)
                    x++;
            return (uint)(((x & 1) == 0) ? 1 : 0);
        }
    }
}
