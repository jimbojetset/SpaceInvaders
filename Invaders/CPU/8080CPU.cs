using Microsoft.Win32;
using System.Diagnostics;

namespace Invaders.CPU
{
    internal class _8080CPU
    {
        private bool running = false;
        public bool Running
        {
            get { return running; }
            set { running = value; }
        }
        
        private byte[] portIn = new byte[4]; // 0,1,2,3
        public byte[] PortIn
        {
            set { portIn = value; }
            get { return portIn; }
        }

        private readonly byte[] portOut = new byte[7]; // 2,3,5,6
        public byte[] PortOut
        { 
            get { return portOut; } 
        }

        private readonly byte[] memory;
        public byte[] Memory
        { 
            get { return memory; } 
        }

        private readonly bool displayAvailable = false;
        public bool DisplayAvailable
        {
            get { return displayAvailable; }
        }

        private readonly byte[] video;
        public byte[] Video
        {
            get { return video; }
        }

        private readonly Registers registers;
        private readonly uint videoStartAddress;
        private int hardwareShiftRegisterData = 0;
        private int hardwareShiftRegisterOffset = 0;
        private static int CLOCK_SPEED = 2000000; // 2Mhz
        private static int FREQUENCY = 60; //60 Hz
        private double CPU_CYCLE_LENGTH = 1 / (double)(CLOCK_SPEED / FREQUENCY);
        private double MAX_FRAME_LENGTH = 1 / (double)FREQUENCY / 2 * 1000; // 8.333 milliseconds

        public _8080CPU(ulong memorySize = 0x10000, ushort pc = 0x0000, ushort videoStartAddr = 0x2400, ushort videoLength = 0x1C00)
        {
            memory = new byte[memorySize];
            video = new byte[videoLength];
            videoStartAddress = videoStartAddr;
            if (videoLength != 0 && videoStartAddr != 0) 
                displayAvailable = true;
            registers = new Registers
            {
                PC = pc
            };
        }

        public void LoadROM(string filePath, int addr, int length)
        {
            Array.Copy(File.ReadAllBytes(filePath), 0, memory, addr, length);
        }

        public void Start()
        {
            running = true;
            while (running)
            {
                ExecuteFrame();
                if(registers.INT_ENABLE)
                    Interrupt(1);
                ExecuteFrame();
                if (registers.INT_ENABLE)
                    Interrupt(2);
                if (displayAvailable)
                    Array.Copy(memory, videoStartAddress, video, 0, video.Length);
            }
        }

        private void ExecuteFrame()
        {
            double thisFrameLength = 0;
            Stopwatch stopwatch = new();
            stopwatch.Start();
            while (running && thisFrameLength < MAX_FRAME_LENGTH)
            {
                byte opcode = memory[registers.PC];
                int cycles = CallOpcode(opcode);
                thisFrameLength += cycles * CPU_CYCLE_LENGTH; // 2Mhz div 60Hz = 0.00003 seconds per CPU cycle
                registers.PC++;
            }           
            while (running && stopwatch.ElapsedMilliseconds < MAX_FRAME_LENGTH)
            { /* here to keep the timing honest across different PC's */ }
        }
        

        public void Stop()
        {
            running = false;
        }

        private ushort ReadOpcodeWord()
        {
            return (ushort)(memory[registers.PC + 2] << 8 | memory[registers.PC + 1]);
        }

        private void Call(ushort address, ushort retAddress)
        {
            memory[registers.SP - 1] = (byte)((retAddress >> 8) & 0xFF);
            memory[registers.SP - 2] = (byte)(retAddress & 0xFF);
            registers.PC = address;
            registers.SP -= 2;
        }

        private void Interrupt(int num)
        {
            memory[registers.SP - 1] = (byte)((registers.PC >> 8) & 0xFF);
            memory[registers.SP - 2] = (byte)(registers.PC & 0xFF);
            registers.SP -= 2;
            if (num == 1)
                registers.PC = 0x0008;
            if (num == 2)
                registers.PC = 0x0010;
        }

        private int CallOpcode(byte opcode)
        {
            int cycles = 0;
            switch (opcode)
            {
                case 0x00: cycles = OP_00(); return cycles;
                case 0x01: cycles = OP_01(); return cycles;
                case 0x02: cycles = OP_02(); return cycles;
                case 0x03: cycles = OP_03(); return cycles;
                case 0x04: cycles = OP_04(); return cycles;
                case 0x05: cycles = OP_05(); return cycles;
                case 0x06: cycles = OP_06(); return cycles;
                case 0x07: cycles = OP_07(); return cycles;
                case 0x09: cycles = OP_09(); return cycles;
                case 0x0A: cycles = OP_0A(); return cycles;
                case 0x0B: cycles = OP_0B(); return cycles;
                case 0x0C: cycles = OP_0C(); return cycles;
                case 0x0D: cycles = OP_0D(); return cycles;
                case 0x0E: cycles = OP_0E(); return cycles;
                case 0x0F: cycles = OP_0F(); return cycles;
                case 0x11: cycles = OP_11(); return cycles;
                case 0x12: cycles = OP_12(); return cycles;
                case 0x13: cycles = OP_13(); return cycles;
                case 0x14: cycles = OP_14(); return cycles;
                case 0x15: cycles = OP_15(); return cycles;
                case 0x16: cycles = OP_16(); return cycles;
                case 0x17: cycles = OP_17(); return cycles;
                case 0x19: cycles = OP_19(); return cycles;
                case 0x1A: cycles = OP_1A(); return cycles;
                case 0x1B: cycles = OP_1B(); return cycles;
                case 0x1C: cycles = OP_1C(); return cycles;
                case 0x1D: cycles = OP_1D(); return cycles;
                case 0x1E: cycles = OP_1E(); return cycles;
                case 0x1F: cycles = OP_1F(); return cycles;
                case 0x20: cycles = OP_20(); return cycles; // RIM	1		special
                case 0x21: cycles = OP_21(); return cycles;
                case 0x22: cycles = OP_22(); return cycles;
                case 0x23: cycles = OP_23(); return cycles;
                case 0x24: cycles = OP_24(); return cycles;
                case 0x25: cycles = OP_25(); return cycles;
                case 0x26: cycles = OP_26(); return cycles;
                case 0x27: cycles = OP_27(); return cycles;
                case 0x29: cycles = OP_29(); return cycles;
                case 0x2A: cycles = OP_2A(); return cycles;
                case 0x2B: cycles = OP_2B(); return cycles;
                case 0x2C: cycles = OP_2C(); return cycles;
                case 0x2D: cycles = OP_2D(); return cycles;
                case 0x2E: cycles = OP_2E(); return cycles;
                case 0x2F: cycles = OP_2F(); return cycles;
                case 0x30: cycles = OP_30(); return cycles;  // SIM	1		special
                case 0x31: cycles = OP_31(); return cycles;
                case 0x32: cycles = OP_32(); return cycles;
                case 0x33: cycles = OP_33(); return cycles;
                case 0x34: cycles = OP_34(); return cycles;
                case 0x35: cycles = OP_35(); return cycles;
                case 0x36: cycles = OP_36(); return cycles;
                case 0x37: cycles = OP_37(); return cycles;
                case 0x39: cycles = OP_39(); return cycles;
                case 0x3A: cycles = OP_3A(); return cycles;
                case 0x3B: cycles = OP_3B(); return cycles;
                case 0x3C: cycles = OP_3C(); return cycles;
                case 0x3D: cycles = OP_3D(); return cycles;
                case 0x3E: cycles = OP_3E(); return cycles;
                case 0x3F: cycles = OP_3F(); return cycles;
                case 0x40: cycles = OP_40(); return cycles;
                case 0x41: cycles = OP_41(); return cycles;
                case 0x42: cycles = OP_42(); return cycles;
                case 0x43: cycles = OP_43(); return cycles;
                case 0x44: cycles = OP_44(); return cycles;
                case 0x45: cycles = OP_45(); return cycles;
                case 0x46: cycles = OP_46(); return cycles;
                case 0x47: cycles = OP_47(); return cycles;
                case 0x48: cycles = OP_48(); return cycles;
                case 0x49: cycles = OP_49(); return cycles;
                case 0x4A: cycles = OP_4A(); return cycles;
                case 0x4B: cycles = OP_4B(); return cycles;
                case 0x4C: cycles = OP_4C(); return cycles;
                case 0x4D: cycles = OP_4D(); return cycles;
                case 0x4E: cycles = OP_4E(); return cycles;
                case 0x4F: cycles = OP_4F(); return cycles;
                case 0x50: cycles = OP_50(); return cycles;
                case 0x51: cycles = OP_51(); return cycles;
                case 0x52: cycles = OP_52(); return cycles;
                case 0x53: cycles = OP_53(); return cycles;
                case 0x54: cycles = OP_54(); return cycles;
                case 0x55: cycles = OP_55(); return cycles;
                case 0x56: cycles = OP_56(); return cycles;
                case 0x57: cycles = OP_57(); return cycles;
                case 0x58: cycles = OP_58(); return cycles;
                case 0x59: cycles = OP_59(); return cycles;
                case 0x5A: cycles = OP_5A(); return cycles;
                case 0x5B: cycles = OP_5B(); return cycles;
                case 0x5C: cycles = OP_5C(); return cycles;
                case 0x5D: cycles = OP_5D(); return cycles;
                case 0x5E: cycles = OP_5E(); return cycles;
                case 0x5F: cycles = OP_5F(); return cycles;
                case 0x60: cycles = OP_60(); return cycles;
                case 0x61: cycles = OP_61(); return cycles;
                case 0x62: cycles = OP_62(); return cycles;
                case 0x63: cycles = OP_63(); return cycles;
                case 0x64: cycles = OP_64(); return cycles;
                case 0x65: cycles = OP_65(); return cycles;
                case 0x66: cycles = OP_66(); return cycles;
                case 0x67: cycles = OP_67(); return cycles;
                case 0x68: cycles = OP_68(); return cycles;
                case 0x69: cycles = OP_69(); return cycles;
                case 0x6A: cycles = OP_6A(); return cycles;
                case 0x6B: cycles = OP_6B(); return cycles;
                case 0x6C: cycles = OP_6C(); return cycles;
                case 0x6D: cycles = OP_6D(); return cycles;
                case 0x6E: cycles = OP_6E(); return cycles;
                case 0x6F: cycles = OP_6F(); return cycles;
                case 0x70: cycles = OP_70(); return cycles;
                case 0x71: cycles = OP_71(); return cycles;
                case 0x72: cycles = OP_72(); return cycles;
                case 0x73: cycles = OP_73(); return cycles;
                case 0x74: cycles = OP_74(); return cycles;
                case 0x75: cycles = OP_75(); return cycles;
                case 0x76: cycles = OP_76(); return cycles;
                case 0x77: cycles = OP_77(); return cycles;
                case 0x78: cycles = OP_78(); return cycles;
                case 0x79: cycles = OP_79(); return cycles;
                case 0x7A: cycles = OP_7A(); return cycles;
                case 0x7B: cycles = OP_7B(); return cycles;
                case 0x7C: cycles = OP_7C(); return cycles;
                case 0x7D: cycles = OP_7D(); return cycles;
                case 0x7E: cycles = OP_7E(); return cycles;
                case 0x7F: cycles = OP_7F(); return cycles;
                case 0x80: cycles = OP_80(); return cycles;
                case 0x81: cycles = OP_81(); return cycles;
                case 0x82: cycles = OP_82(); return cycles;
                case 0x83: cycles = OP_83(); return cycles;
                case 0x84: cycles = OP_84(); return cycles;
                case 0x85: cycles = OP_85(); return cycles;
                case 0x86: cycles = OP_86(); return cycles;
                case 0x87: cycles = OP_87(); return cycles;
                case 0x88: cycles = OP_88(); return cycles;
                case 0x89: cycles = OP_89(); return cycles;
                case 0x8A: cycles = OP_8A(); return cycles;
                case 0x8B: cycles = OP_8B(); return cycles;
                case 0x8C: cycles = OP_8C(); return cycles;
                case 0x8D: cycles = OP_8D(); return cycles;
                case 0x8E: cycles = OP_8E(); return cycles;
                case 0x8F: cycles = OP_8F(); return cycles;
                case 0x90: cycles = OP_90(); return cycles;
                case 0x91: cycles = OP_91(); return cycles;
                case 0x92: cycles = OP_92(); return cycles;
                case 0x93: cycles = OP_93(); return cycles;
                case 0x94: cycles = OP_94(); return cycles;
                case 0x95: cycles = OP_95(); return cycles;
                case 0x96: cycles = OP_96(); return cycles;
                case 0x97: cycles = OP_97(); return cycles;
                case 0x98: cycles = OP_98(); return cycles;
                case 0x99: cycles = OP_99(); return cycles;
                case 0x9A: cycles = OP_9A(); return cycles;
                case 0x9B: cycles = OP_9B(); return cycles;
                case 0x9C: cycles = OP_9C(); return cycles;
                case 0x9D: cycles = OP_9D(); return cycles;
                case 0x9E: cycles = OP_9E(); return cycles;
                case 0x9F: cycles = OP_9F(); return cycles;
                case 0xA0: cycles = OP_A0(); return cycles;
                case 0xA1: cycles = OP_A1(); return cycles;
                case 0xA2: cycles = OP_A2(); return cycles;
                case 0xA3: cycles = OP_A3(); return cycles;
                case 0xA4: cycles = OP_A4(); return cycles;
                case 0xA5: cycles = OP_A5(); return cycles;
                case 0xA6: cycles = OP_A6(); return cycles;
                case 0xA7: cycles = OP_A7(); return cycles;
                case 0xA8: cycles = OP_A8(); return cycles;
                case 0xA9: cycles = OP_A9(); return cycles;
                case 0xAA: cycles = OP_AA(); return cycles;
                case 0xAB: cycles = OP_AB(); return cycles;
                case 0xAC: cycles = OP_AC(); return cycles;
                case 0xAD: cycles = OP_AD(); return cycles;
                case 0xAE: cycles = OP_AE(); return cycles;
                case 0xAF: cycles = OP_AF(); return cycles;
                case 0xB0: cycles = OP_B0(); return cycles;
                case 0xB1: cycles = OP_B1(); return cycles;
                case 0xB2: cycles = OP_B2(); return cycles;
                case 0xB3: cycles = OP_B3(); return cycles;
                case 0xB4: cycles = OP_B4(); return cycles;
                case 0xB5: cycles = OP_B5(); return cycles;
                case 0xB6: cycles = OP_B6(); return cycles;
                case 0xB7: cycles = OP_B7(); return cycles;
                case 0xB8: cycles = OP_B8(); return cycles;
                case 0xB9: cycles = OP_B9(); return cycles;
                case 0xBA: cycles = OP_BA(); return cycles;
                case 0xBB: cycles = OP_BB(); return cycles;
                case 0xBC: cycles = OP_BC(); return cycles;
                case 0xBD: cycles = OP_BD(); return cycles;
                case 0xBE: cycles = OP_BE(); return cycles;
                case 0xBF: cycles = OP_BF(); return cycles;
                case 0xC0: cycles = OP_C0(); return cycles;
                case 0xC1: cycles = OP_C1(); return cycles;
                case 0xC2: cycles = OP_C2(); return cycles;
                case 0xC3: cycles = OP_C3(); return cycles;
                case 0xC4: cycles = OP_C4(); return cycles;
                case 0xC5: cycles = OP_C5(); return cycles;
                case 0xC6: cycles = OP_C6(); return cycles;
                case 0xC7: cycles = OP_C7(); return cycles;
                case 0xC8: cycles = OP_C8(); return cycles;
                case 0xC9: cycles = OP_C9(); return cycles;
                case 0xCA: cycles = OP_CA(); return cycles;
                case 0xCC: cycles = OP_CC(); return cycles;
                case 0xCD: cycles = OP_CD(); return cycles;
                case 0xCE: cycles = OP_CE(); return cycles;
                case 0xCF: cycles = OP_CF(); return cycles;
                case 0xD0: cycles = OP_D0(); return cycles;
                case 0xD1: cycles = OP_D1(); return cycles;
                case 0xD2: cycles = OP_D2(); return cycles;
                case 0xD3: cycles = OP_D3(); return cycles;
                case 0xD4: cycles = OP_D4(); return cycles;
                case 0xD5: cycles = OP_D5(); return cycles;
                case 0xD6: cycles = OP_D6(); return cycles;
                case 0xD7: cycles = OP_D7(); return cycles;
                case 0xD8: cycles = OP_D8(); return cycles;
                case 0xDA: cycles = OP_DA(); return cycles;
                case 0xDB: cycles = OP_DB(); return cycles;
                case 0xDC: cycles = OP_DC(); return cycles;
                case 0xDE: cycles = OP_DE(); return cycles;
                case 0xDF: cycles = OP_DF(); return cycles;
                case 0xE0: cycles = OP_E0(); return cycles;
                case 0xE1: cycles = OP_E1(); return cycles;
                case 0xE2: cycles = OP_E2(); return cycles;
                case 0xE3: cycles = OP_E3(); return cycles;
                case 0xE4: cycles = OP_E4(); return cycles;
                case 0xE5: cycles = OP_E5(); return cycles;
                case 0xE6: cycles = OP_E6(); return cycles;
                case 0xE7: cycles = OP_E7(); return cycles;
                case 0xE8: cycles = OP_E8(); return cycles;
                case 0xE9: cycles = OP_E9(); return cycles;
                case 0xEA: cycles = OP_EA(); return cycles;
                case 0xEB: cycles = OP_EB(); return cycles;
                case 0xEC: cycles = OP_EC(); return cycles;
                case 0xEE: cycles = OP_EE(); return cycles;
                case 0xEF: cycles = OP_EF(); return cycles;
                case 0xF0: cycles = OP_F0(); return cycles;
                case 0xF1: cycles = OP_F1(); return cycles;
                case 0xF2: cycles = OP_F2(); return cycles;
                case 0xF3: cycles = OP_F3(); return cycles;
                case 0xF4: cycles = OP_F4(); return cycles;
                case 0xF5: cycles = OP_F5(); return cycles;
                case 0xF6: cycles = OP_F6(); return cycles;
                case 0xF7: cycles = OP_F7(); return cycles;
                case 0xF8: cycles = OP_F8(); return cycles;
                case 0xF9: cycles = OP_F9(); return cycles;
                case 0xFA: cycles = OP_FA(); return cycles;
                case 0xFB: cycles = OP_FB(); return cycles;
                case 0xFC: cycles = OP_FC(); return cycles;
                case 0xFE: cycles = OP_FE(); return cycles;
                case 0xFF: cycles = OP_FF(); return cycles;
                default: throw new NotImplementedException("INVALID OPCODE - " + opcode.ToString("X2"));
            }
        }

        private int OP_00()
        {
            // NOP
            return 4;
        }

        private int OP_01()
        {
            registers.C = memory[(uint)(registers.PC + 1)];
            registers.B = memory[(uint)(registers.PC + 2)];
            registers.PC += 2;
            return 10;
        }

        private int OP_02()
        {
            var addr = registers.BC;
            memory[registers.BC] = registers.A;
            return 7;
        }

        private int OP_03()
        {
            var addr = registers.BC;
            addr++;
            registers.BC = addr;
            return 5;
        }

        private int OP_04()
        {
            registers.B++;
            registers.Flags.UpdateZSP(registers.B);
            return 5;
        }

        private int OP_05()
        {
            registers.B--;
            registers.Flags.UpdateZSP(registers.B);
            return 5;
        }

        private int OP_06()
        {
            registers.B = memory[registers.PC + 1];
            registers.PC++;
            return 7;
        }

        private int OP_07()
        {
            var bit7 = ((registers.A & 0x80) == 0x80) ? 1 : 0;
            registers.A = (byte)((registers.A << 1) | bit7);
            registers.Flags.CY = (byte)bit7;
            return 4;
        }

        private int OP_09()
        {
            var addr = registers.HL + registers.BC;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = addr & 0xFFFF;
            return 10;
        }

        private int OP_0A()
        {
            var addr = registers.BC;
            registers.A = memory[addr];
            return 7;
        }

        private int OP_0B()
        {
            var addr = registers.BC;
            addr--;
            registers.BC = addr;
            return 5;
        }

        private int OP_0C()
        {
            registers.C++;
            registers.Flags.UpdateZSP(registers.C);
            return 5;
        }

        private int OP_0D()
        {
            registers.C--;
            registers.Flags.UpdateZSP(registers.C);
            return 5;
        }

        private int OP_0E()
        {
            registers.C = memory[registers.PC + 1];
            registers.PC++;
            return 7;
        }

        private int OP_0F()
        {
            var bit0 = registers.A & 0x01;
            registers.A >>= 1;
            registers.A |= (byte)(bit0 << 7);
            registers.Flags.CY = (byte)bit0;
            return 4;
        }

        private int OP_11()
        {
            registers.D = memory[registers.PC + 2];
            registers.E = memory[registers.PC + 1];
            registers.PC += 2;
            return 10;
        }

        private int OP_12()
        {
            var addr = registers.DE;
            memory[addr] = registers.A;
            return 7;
        }

        private int OP_13()
        {
            var addr = registers.DE; ;
            addr++;
            registers.DE = addr;
            return 5;
        }

        private int OP_14()
        {
            registers.D++;
            registers.Flags.UpdateZSP(registers.D);
            return 5;
        }

        private int OP_15()
        {
            registers.D--;
            registers.Flags.UpdateZSP(registers.D);
            return 5;
        }

        private int OP_16()
        {
            registers.D = memory[registers.PC + 1];
            registers.PC++;
            return 7;
        }

        private int OP_17()
        {
            var bit7 = (uint)(((registers.A & 128) == 128) ? 1 : 0);
            var bit0 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A << 1) | bit0);
            registers.Flags.CY = bit7;
            return 4;
        }

        private int OP_19()
        {
            var addr = registers.DE + registers.HL;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = addr & 0xFFFF;
            return 10;
        }

        private int OP_1A()
        {
            var addr = registers.DE;
            registers.A = memory[addr];
            return 7;
        }

        private int OP_1B()
        {
            var addr = (ushort)registers.DE;
            addr--;
            registers.DE = addr;
            return 5;
        }

        private int OP_1C()
        {
            registers.E++;
            registers.Flags.UpdateZSP(registers.E);
            return 5;
        }

        private int OP_1D()
        {
            registers.E--;
            registers.Flags.UpdateZSP(registers.E);
            return 5;
        }

        private int OP_1E()
        {
            registers.E = memory[registers.PC + 1];
            registers.PC++;
            return 7;
        }

        private int OP_1F()
        {
            var bit0 = registers.A & 1;
            var bit7 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A >> 1) | (bit7 << 7));
            registers.Flags.CY = (byte)bit0;
            return 4;
        }

        private int OP_20() // RIM	1		special
        { return 4; }

        private int OP_21()
        {
            registers.H = memory[registers.PC + 2];
            registers.L = memory[registers.PC + 1];
            registers.PC += 2;
            return 10;
        }

        private int OP_22()
        {
            var addr = ReadOpcodeWord();
            memory[addr] = registers.L;
            memory[addr + 1] = registers.H;
            registers.PC += 2;
            return 16;
        }

        private int OP_23()
        {
            var addr = registers.HL;
            addr++;
            registers.HL = addr;
            return 5;
        }

        private int OP_24()
        {
            registers.H++;
            registers.Flags.UpdateZSP(registers.H);
            return 5;
        }

        private int OP_25()
        {
            registers.H--;
            registers.Flags.UpdateZSP(registers.H);
            return 5;
        }

        private int OP_26()
        {
            registers.H = memory[registers.PC + 1];
            registers.PC++;
            return 7;
        }

        private int OP_27()
        {
            byte bits = (byte)(registers.A & 0x0F);
            if ((registers.A & 0x0F) > 9 || registers.Flags.AC == 1)
            {
                registers.A += 6;
                if (bits + 0x06 > 0x10)
                    registers.Flags.AC = 0x01;
                else
                    registers.Flags.AC = 0x00;
            }
            else
                registers.Flags.AC = 0x00;
            if ((registers.A & 0xF0) > 0x90 || registers.Flags.CY == 1)
            {
                ushort addr = (ushort)(registers.A + 0x60);
                registers.Flags.UpdateZSP(addr);
                registers.Flags.UpdateCarryByte(addr);
                registers.A = (byte)(addr & 0xFF);
            }
            else
                registers.Flags.CY = 0x00;
            return 4;
        }

        private int OP_29()
        {
            var addr = registers.HL + registers.HL;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = addr & 0xFFFF;
            return 10;
        }

        private int OP_2A()
        {
            var addr = ReadOpcodeWord();
            registers.L = memory[addr];
            registers.H = memory[addr + 1];
            registers.PC += 2;
            return 16;
        }

        private int OP_2B()
        {
            var addr = registers.HL;
            addr--;
            registers.HL = addr;
            return 5;
        }

        private int OP_2C()
        {
            registers.L++;
            registers.Flags.UpdateZSP(registers.L);
            return 5;
        }

        private int OP_2D()
        {
            registers.L--;
            registers.Flags.UpdateZSP(registers.L);
            return 5;
        }

        private int OP_2E()
        {
            registers.L = memory[registers.PC + 1];
            registers.PC++;
            return 7;
        }

        private int OP_2F()
        {
            registers.A = (byte)~registers.A;
            return 7;
        }

        private int OP_30()  // SIM	1		special
        { return 4; }

        private int OP_31()
        {
            registers.SP = ReadOpcodeWord();
            registers.PC += 2;
            return 10;
        }

        private int OP_32()
        {
            ushort addr = ReadOpcodeWord();
            memory[addr] = registers.A;
            registers.PC += 2;
            return 15;
        }

        private int OP_33()
        {
            registers.SP++;
            return 5;
        }

        private int OP_34()
        {
            var addr = registers.HL;
            var value = memory[addr];
            value++;
            registers.Flags.UpdateZSP(value);
            memory[addr] = (byte)(value & 0xFF);
            return 10;
        }

        private int OP_35()
        {
            var addr = registers.HL;
            var value = memory[addr];
            value--;
            registers.Flags.UpdateZSP(value);
            memory[addr] = (byte)(value & 0xFF);
            return 10;
        }

        private int OP_36()
        {
            var addr = registers.HL;
            var value = memory[registers.PC + 1];
            memory[addr] = value;
            registers.PC++;
            return 10;
        }

        private int OP_37()
        {
            registers.Flags.CY = 1;
            return 4;
        }

        private int OP_39()
        {
            var value = registers.HL + registers.SP;
            registers.Flags.UpdateCarryWord(value);
            registers.HL = (value & 0xFFFF);
            return 10;
        }

        private int OP_3A()
        {
            var addr = ReadOpcodeWord();
            registers.A = memory[addr];
            registers.PC += 2;
            return 13;
        }

        private int OP_3B()
        {
            registers.SP--;
            return 5;
        }

        private int OP_3C()
        {
            registers.A++;
            registers.Flags.UpdateZSP(registers.A);
            return 5;
        }

        private int OP_3D()
        {
            registers.A--;
            registers.Flags.UpdateZSP(registers.A);
            return 5;
        }

        private int OP_3E()
        {
            var addr = memory[registers.PC + 1];
            registers.A = addr;
            registers.PC++;
            return 7;
        }

        private int OP_3F()
        {
            registers.Flags.CY = (byte)~registers.Flags.CY;
            return 4;
        }

        private int OP_40()
        {
            registers.B = registers.B;
            return 5;
        }

        private int OP_41()
        {
            registers.B = registers.C;
            return 5;
        }

        private int OP_42()
        {
            registers.B = registers.D;
            return 5;
        }

        private int OP_43()
        {
            registers.B = registers.E;
            return 5;
        }

        private int OP_44()
        {
            registers.B = registers.H;
            return 5;
        }

        private int OP_45()
        {
            registers.B = registers.L;
            return 5;
        }

        private int OP_46()
        {
            var addr = registers.HL;
            registers.B = memory[addr];
            return 7;
        }

        private int OP_47()
        {
            registers.B = registers.A;
            return 5;
        }

        private int OP_48()
        {
            registers.C = registers.B;
            return 5;
        }

        private int OP_49()
        {
            registers.C = registers.C;
            return 5;
        }

        private int OP_4A()
        {
            registers.C = registers.D;
            return 5;
        }

        private int OP_4B()
        {
            registers.C = registers.E;
            return 5;
        }

        private int OP_4C()
        {
            registers.C = registers.H;
            return 5;
        }

        private int OP_4D()
        {
            registers.C = registers.L;
            return 5;
        }

        private int OP_4E()
        {
            var addr = registers.HL;
            registers.C = memory[addr];
            return 7;
        }

        private int OP_4F()
        {
            registers.C = registers.A;
            return 5;
        }

        private int OP_50()
        {
            registers.D = registers.B;
            return 5;
        }

        private int OP_51()
        {
            registers.D = registers.C;
            return 5;
        }

        private int OP_52()
        {
            registers.D = registers.D;
            return 5;
        }

        private int OP_53()
        {
            registers.D = registers.E;
            return 5;
        }

        private int OP_54()
        {
            registers.D = registers.H;
            return 5;
        }

        private int OP_55()
        {
            registers.D = registers.L;
            return 5;
        }

        private int OP_56()
        {
            var addr = registers.HL;
            registers.D = memory[addr];
            return 7;
        }

        private int OP_57()
        {
            registers.D = registers.A;
            return 5;
        }

        private int OP_58()
        {
            registers.E = registers.B;
            return 5;
        }

        private int OP_59()
        {
            registers.E = registers.C;
            return 5;
        }

        private int OP_5A()
        {
            registers.E = registers.D;
            return 5;
        }

        private int OP_5B()
        {
            registers.E = registers.E;
            return 5;
        }

        private int OP_5C()
        {
            registers.E = registers.H;
            return 5;
        }

        private int OP_5D()
        {
            registers.E = registers.L;
            return 5;
        }

        private int OP_5E()
        {
            var addr = registers.HL;
            registers.E = memory[addr];
            return 7;
        }

        private int OP_5F()
        {
            registers.E = registers.A;
            return 5;
        }

        private int OP_60()
        {
            registers.H = registers.B;
            return 5;
        }

        private int OP_61()
        {
            registers.H = registers.C;
            return 5;
        }

        private int OP_62()
        {
            registers.H = registers.D;
            return 5;
        }

        private int OP_63()
        {
            registers.H = registers.E;
            return 5;
        }

        private int OP_64()
        {
            registers.H = registers.H;
            return 5;
        }

        private int OP_65()
        {
            registers.H = registers.L;
            return 5;
        }

        private int OP_66()
        {
            var addr = registers.HL;
            registers.H = memory[addr];
            return 7;
        }

        private int OP_67()
        {
            registers.H = registers.A;
            return 5;
        }

        private int OP_68()
        {
            registers.L = registers.B;
            return 5;
        }

        private int OP_69()
        {
            registers.L = registers.C;
            return 5;
        }

        private int OP_6A()
        {
            registers.L = registers.D;
            return 5;
        }

        private int OP_6B()
        {
            registers.L = registers.E;
            return 5;
        }

        private int OP_6C()
        {
            registers.L = registers.H;
            return 5;
        }

        private int OP_6D()
        {
            registers.L = registers.L;
            return 5;
        }

        private int OP_6E()
        {
            var addr = registers.HL;
            registers.L = memory[addr];
            return 7;
        }

        private int OP_6F()
        {
            registers.L = registers.A;
            return 5;
        }

        private int OP_70()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.B;
            return 7;
        }

        private int OP_71()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.C;
            return 7;
        }

        private int OP_72()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.D;
            return 7;
        }

        private int OP_73()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.E;
            return 7;
        }

        private int OP_74()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.H;
            return 7;
        }

        private int OP_75()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.L;
            return 7;
        }

        private int OP_76()
        {
            running = false;
            return 7;
        }

        private int OP_77()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.A;
            return 7;
        }

        private int OP_78()
        {
            registers.A = registers.B;
            return 5;
        }

        private int OP_79()
        {
            registers.A = registers.C;
            return 5;
        }

        private int OP_7A()
        {
            registers.A = registers.D;
            return 5;
        }

        private int OP_7B()
        {
            registers.A = registers.E;
            return 5;
        }

        private int OP_7C()
        {
            registers.A = registers.H;
            return 5;
        }

        private int OP_7D()
        {
            registers.A = registers.L;
            return 5;
        }

        private int OP_7E()
        {
            var addr = registers.HL;
            registers.A = memory[addr];
            return 7;
        }

        private int OP_7F()
        {
            registers.A = registers.A;
            return 5;
        }

        private int OP_80()
        {
            var addr = (uint)registers.A + (uint)registers.B;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

            return 4;
        }

        private int OP_81()
        {
            var addr = (uint)registers.A + (uint)registers.C;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

            return 4;
        }

        private int OP_82()
        {
            var addr = (uint)registers.A + (uint)registers.D;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

            return 4;
        }

        private int OP_83()
        {
            var addr = (uint)registers.A + (uint)registers.E;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

            return 4;
        }

        private int OP_84()
        {
            var addr = (uint)registers.A + (uint)registers.H;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

            return 4;
        }

        private int OP_85()
        {
            var addr = (uint)registers.A + (uint)registers.L;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_86()
        {
            var addr = (uint)registers.A + (uint)memory[registers.HL];
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL], 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL]);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 7;
        }

        private int OP_87()
        {
            var addr = (uint)registers.A + (uint)registers.A;
            registers.Flags.UpdateAuxCarryFlag(registers.A, registers.A);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_88()
        {
            var addr = (uint)registers.A + (uint)registers.B + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_89()
        {
            var addr = (uint)registers.A + (uint)registers.C + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_8A()
        {
            var addr = (uint)registers.A + (uint)registers.D + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_8B()
        {
            var addr = (uint)registers.A + (uint)registers.E + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_8C()
        {
            var addr = (uint)registers.A + (uint)registers.H + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_8D()
        {
            var addr = (uint)registers.A + (uint)registers.L + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_8E()
        {
            var addr = (uint)registers.A + (uint)memory[registers.HL] + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL], 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL]);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 7;
        }

        private int OP_8F()
        {
            var addr = (uint)registers.A + (uint)registers.A + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.A, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.A);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_90()
        {
            uint reg = registers.B;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_91()
        {
            uint reg = registers.C;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_92()
        {
            uint reg = registers.D;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_93()
        {
            uint reg = registers.E;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_94()
        {
            uint reg = registers.H;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_95()
        {
            uint reg = registers.L;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_96()
        {
            uint reg = (uint)memory[registers.HL];
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 7;
        }

        private int OP_97()
        {
            uint reg = registers.A;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_98()
        {
            uint reg = registers.B;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_99()
        {
            uint reg = registers.C;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_9A()
        {
            uint reg = registers.D;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_9B()
        {
            uint reg = registers.E;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_9C()
        {
            uint reg = registers.H;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_9D()
        {
            uint reg = registers.L;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_9E()
        {
            uint reg = memory[registers.HL];
            if (registers.Flags.CY == 1)
                reg += 1;
            UInt16 addr = (UInt16)(registers.A + (byte)(~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 7;
        }

        private int OP_9F()
        {
            uint reg = (uint)registers.A;
            if (registers.Flags.CY == 1)
                reg += 1;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
            return 4;
        }

        private int OP_A0()
        {
            registers.A = (byte)(registers.A & registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A1()
        {
            registers.A = (byte)(registers.A & registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A2()
        {
            registers.A = (byte)(registers.A & registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A3()
        {
            registers.A = (byte)(registers.A & registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A4()
        {
            registers.A = (byte)(registers.A & registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A5()
        {
            registers.A = (byte)(registers.A & registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A6()
        {
            registers.A = (byte)(registers.A & memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 7;
        }

        private int OP_A7()
        {
            registers.A = (byte)(registers.A & registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A8()
        {
            registers.A = (byte)(registers.A ^ registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_A9()
        {
            registers.A = (byte)(registers.A ^ registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_AA()
        {
            registers.A = (byte)(registers.A ^ registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_AB()
        {
            registers.A = (byte)(registers.A ^ registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_AC()
        {
            registers.A = (byte)(registers.A ^ registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_AD()
        {
            registers.A = (byte)(registers.A ^ registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_AE()
        {
            registers.A = (byte)(registers.A ^ memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 7;
        }

        private int OP_AF()
        {
            registers.A = (byte)(registers.A ^ registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B0()
        {
            registers.A = (byte)(registers.A | registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B1()
        {
            registers.A = (byte)(registers.A | registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B2()
        {
            registers.A = (byte)(registers.A | registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B3()
        {
            registers.A = (byte)(registers.A | registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B4()
        {
            registers.A = (byte)(registers.A | registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B5()
        {
            registers.A = (byte)(registers.A | registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B6()
        {
            registers.A = (byte)(registers.A | memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 7;
        }

        private int OP_B7()
        {
            registers.A = (byte)(registers.A | registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
            return 4;
        }

        private int OP_B8()
        {
            var addr = (byte)(registers.A - registers.B);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.B)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 4;
        }

        private int OP_B9()
        {
            var addr = (byte)(registers.A - registers.C);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.C)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 4;
        }

        private int OP_BA()
        {
            var addr = (byte)(registers.A - registers.D);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.D)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 4;
        }

        private int OP_BB()
        {
            var addr = (byte)(registers.A - registers.E);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.E)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 4;
        }

        private int OP_BC()
        {
            var addr = (byte)(registers.A - registers.H);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.H)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 4;
        }

        private int OP_BD()
        {
            var addr = (byte)(registers.A - registers.L);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.L)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 4;
        }

        private int OP_BE()
        {
            var addr = (byte)(registers.A - memory[registers.HL]);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < memory[registers.HL])
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
            return 7;
        }

        private int OP_BF()
        {
            registers.Flags.Z = 1;
            registers.Flags.S = 0;
            registers.Flags.P = (uint)Flags.CalculateParityFlag(registers.A);
            registers.Flags.CY = 0;
            return 4;
        }

        private int OP_C0()
        {
            if (registers.Flags.Z == 0)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_C1()
        {
            registers.C = memory[registers.SP];
            registers.B = memory[registers.SP + 1];
            registers.SP += 2;
            return 10;
        }

        private int OP_C2()
        {
            if (registers.Flags.Z == 0)
            {
                var addr = ReadOpcodeWord();
                registers.PC = addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_C3()
        {
            var addr = ReadOpcodeWord();
            registers.PC = addr;
            registers.PC--;
            return 10;
        }

        private int OP_C4()
        {
            if (registers.Flags.Z == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_C5()
        {
            memory[registers.SP - 2] = registers.C;
            memory[registers.SP - 1] = registers.B;
            registers.SP -= 2;
            return 11;
        }

        private int OP_C6()
        {
            var addr = (uint)registers.A + (uint)memory[registers.PC + 1];
            registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.PC + 1]);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
            return 7;
        }

        private int OP_C7()
        {
            Call(0x38, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_C8()
        {
            if (registers.Flags.Z == 1)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_C9()
        {
            registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
            registers.SP += 2;
            registers.PC--;
            return 10;
        }

        private int OP_CA()
        {
            if (registers.Flags.Z == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_CC()
        {
            if (registers.Flags.Z == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_CD()
        {
            var addr = ReadOpcodeWord();
            var retAddr = (ulong)(registers.PC + 3);
            Call((ushort)addr, (ushort)retAddr);
            registers.PC--;
            return 17;
        }

        private int OP_CE()
        {
            uint addr = registers.A;
            addr += memory[registers.PC + 1];
            addr += registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.PC + 1], 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.PC + 1]);
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
            return 7;
        }

        private int OP_CF()
        {
            Call(0x08, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_D0()
        {
            if (registers.Flags.CY == 0)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_D1()
        {
            registers.E = memory[registers.SP];
            registers.D = memory[registers.SP + 1];
            registers.SP += 2;
            return 10;
        }

        private int OP_D2()
        {
            if (registers.Flags.CY == 0)
            {
                var addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_D3()
        {
            var port = memory[registers.PC + 1];
            switch (port)
            {
                case 2:
                    hardwareShiftRegisterOffset = registers.A & 0x07;
                    break;

                case 3:
                    portOut[3] = registers.A;
                    break;

                case 4:
                    hardwareShiftRegisterData = (hardwareShiftRegisterData >> 8) | (registers.A << 8);
                    break;

                case 5:
                    portOut[5] = registers.A;
                    break;
            }
            registers.PC++;
            return 10;
        }

        private int OP_D4()
        {
            if (registers.Flags.CY == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_D5()
        {
            memory[registers.SP - 2] = registers.E;
            memory[registers.SP - 1] = registers.D;
            registers.SP -= 2;
            return 11;
        }

        private int OP_D6()
        {
            uint data = memory[registers.PC + 1];
            uint addr = registers.A - data;
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
            return 7;
        }

        private int OP_D7()
        {
            Call(0x10, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_D8()
        {
            if (registers.Flags.CY == 1)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_DA()
        {
            if (registers.Flags.CY == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_DB()
        {
            var port = memory[registers.PC + 1];
            switch (port)
            {
                case 0:
                    registers.A = portIn[0];
                    break;

                case 1:
                    registers.A = portIn[1];
                    break;

                case 2:
                    registers.A = portIn[2];
                    break;

                case 3:
                    registers.A = (byte)(hardwareShiftRegisterData >> (8 - hardwareShiftRegisterOffset));
                    break;
            }
            registers.PC++;
            return 10;
        }

        private int OP_DC()
        {
            if (registers.Flags.CY == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_DE()
        {
            uint data = memory[registers.PC + 1];
            uint addr = registers.A - data - registers.Flags.CY;
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
            return 7;
        }

        private int OP_DF()
        {
            Call(0x18, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_E0()
        {
            if (registers.Flags.P == 0)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_E1()
        {
            registers.L = memory[registers.SP];
            registers.H = memory[registers.SP + 1];
            registers.SP += 2;
            return 10;
        }

        private int OP_E2()
        {
            if (registers.Flags.P == 0)
            {
                var addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_E3()
        {
            var l = registers.L;
            var h = registers.H;
            registers.L = memory[registers.SP];
            registers.H = memory[registers.SP + 1];
            memory[registers.SP] = l;
            memory[registers.SP + 1] = h;
            return 18;
        }

        private int OP_E4()
        {
            if (registers.Flags.P == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_E5()
        {
            memory[registers.SP - 2] = registers.L;
            memory[registers.SP - 1] = registers.H;
            registers.SP -= 2;
            return 11;
        }

        private int OP_E6()
        {
            uint addr = (uint)(registers.A & memory[registers.PC + 1]);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
            return 7;
        }

        private int OP_E7()
        {
            Call(0x20, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_E8()
        {
            if (registers.Flags.P == 1)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_E9()
        {
            registers.PC = (ushort)(registers.H << 8 | registers.L);
            registers.PC--;
            return 5;
        }

        private int OP_EA()
        {
            if (registers.Flags.P == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_EB()
        {
            (registers.D, registers.H) = (registers.H, registers.D);
            (registers.L, registers.E) = (registers.E, registers.L);
            return 5;
        }

        private int OP_EC()
        {
            if (registers.Flags.P == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_EE()
        {
            registers.A ^= memory[registers.PC + 1];
            registers.Flags.UpdateCarryByte(registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.PC++;
            return 7;
        }

        private int OP_EF()
        {
            Call(0x28, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_F0()
        {
            if (registers.Flags.P == 1)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_F1()
        {
            registers.A = memory[registers.SP + 1];
            var flags = memory[registers.SP];
            if (0x01 == (flags & 0x01)) registers.Flags.Z = 0x01; else registers.Flags.Z = 0x00;
            if (0x02 == (flags & 0x02)) registers.Flags.S = 0x01; else registers.Flags.S = 0x00;
            if (0x04 == (flags & 0x04)) registers.Flags.P = 0x01; else registers.Flags.P = 0x00;
            if (0x08 == (flags & 0x08)) registers.Flags.CY = 0x01; else registers.Flags.CY = 0x00;
            if (0x10 == (flags & 0x10)) registers.Flags.AC = 0x01; else registers.Flags.AC = 0x00;
            registers.SP += 2;
            return 10;
        }

        private int OP_F2()
        {
            if (registers.Flags.P == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_F3()
        {
            registers.INT_ENABLE = false;
            return 4;
        }

        private int OP_F4()
        {
            if (registers.Flags.S == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_F5()
        {
            memory[registers.SP - 1] = registers.A;
            byte addr = ((byte)(registers.Flags.Z | registers.Flags.S << 1 | registers.Flags.P << 2 | registers.Flags.CY << 3 | registers.Flags.AC << 4));
            memory[registers.SP - 2] = addr;
            registers.SP -= 2;
            return 11;
        }

        private int OP_F6()
        {
            uint data = memory[registers.PC + 1];
            uint value = registers.A | data;
            registers.Flags.UpdateCarryByte(value);
            registers.Flags.UpdateZSP(value);
            registers.A = (byte)value;
            registers.PC++;
            return 7;
        }

        private int OP_F7()
        {
            Call(0x30, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }

        private int OP_F8()
        {
            if (registers.Flags.S == 1)
            {
                registers.PC = (ushort)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
                return 11;
            }
            return 5;
        }

        private int OP_F9()
        {
            registers.SP = (ushort)registers.HL;
            return 5;
        }

        private int OP_FA()
        {
            if (registers.Flags.S == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
            return 10;
        }

        private int OP_FB()
        {
            registers.INT_ENABLE = true;
            return 4;
        }

        private int OP_FC()
        {
            if (registers.Flags.S == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
                return 17;
            }
            else
            {
                registers.PC += 2;
            }
            return 11;
        }

        private int OP_FE()
        {
            UInt16 addr = (UInt16)(registers.A + (byte)(~(memory[registers.PC + 1]) & 0xFF) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)(~(memory[registers.PC + 1]) & 0xFF), 1);
            registers.PC++;
            return 7;
        }

        private int OP_FF()
        {
            Call(0x38, (ushort)(registers.PC + 2));
            registers.PC--;
            return 11;
        }
    }
}