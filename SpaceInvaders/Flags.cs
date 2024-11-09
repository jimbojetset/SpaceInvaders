using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Flags
    {
        private byte z; // Zero bit
        private byte s; // Sign bit
        private byte p; // Parity bit
        private byte cy; // Carry bit
        private byte ac; // Auxiliary carry bit
        private byte pad;

        public Flags()
        {
            this.Z = 0;
            this.S = 0;
            this.P = 0;
            this.cy = 0;
            this.ac = 0;
            this.pad = 3;
        }

        public byte Z
        {
            get { return this.z; }
            set { this.z = value; }
        }

        public byte S
        {
            get { return this.s; }
            set { this.s = value; }
        }

        public byte P
        {
            get { return this.p; }
            set { this.p = value; }
        }

        public byte CY
        {
            get { return this.cy; }
            set { this.cy = value; }
        }

        public byte WordCY
        {
            get { return this.cy; }
            set { this.cy = (byte)((value > 0xFFFF) ? 1 : 0); }
        }

        public byte ByteCY
        {
            get { return this.cy; }
            set { this.cy = (byte)((value > 0xFF) ? 1 : 0); }
        }

        public byte AC
        {
            get { return this.ac; }
            set { this.ac = value; }
        }

        public byte Pad
        {
            get { return this.pad; }
            set { this.pad = value; }
        }
    }
}
