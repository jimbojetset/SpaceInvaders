using System.Diagnostics;

namespace SpaceInvaders
{
    internal class _8080CPU
    {
        private readonly Registers registers;

        private bool running = false;

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }

        private int vSync = 1;

        public int V_Sync
        { get { return vSync; } }

        private readonly byte[] video = new byte[0x1C00];

        public byte[] Video
        { get { return video; } }

        private byte[] portIn = new byte[4]; // 0,1,2,3

        public byte[] PortIn
        { set { portIn = value; } }

        private byte[] portOut = new byte[7]; // 2,3,5,6

        public byte[] PortOut
        { get { return portOut; } }

        private byte[] memory;

        public byte[] Memory
        { get { return memory; } }

        private int hardwareShiftRegisterData = 0;
        private int hardwareShiftRegisterOffset = 0;

        public _8080CPU(ushort pc)
        {
            memory = new byte[0x4000];//0000-1FFF 8K ROM    2000 - 23FF 1K RAM    2400 - 3FFF 7K Video RAM
            registers = new Registers();
            registers.PC = pc;
        }

        public void LoadROM(string filePath, int addr, int length)
        {
            Array.Copy(File.ReadAllBytes(filePath), 0, memory, addr, length);
        }

        public void Stop()
        {
            running = false;
        }

        public void Start()
        {
            running = true;
            vSync = 1;
            while (running)
            {
                vSync = 1;
                Tick();
                vSync = 2;
                Tick();
                Buffer.BlockCopy(memory, 0x2400, video, 0, video.Length);
            }
        }

        private void Tick()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool hit_v_sync = false;
            while (!hit_v_sync && running)
            {
                CallOpcode(memory[registers.PC]);
                registers.PC++;
                if (stopwatch.ElapsedMilliseconds > 8.33 && running)
                {
                    Interrupt(vSync);
                    hit_v_sync = true;
                }
            }
        }

        private void CallOpcode(byte opcode)
        {
            switch (opcode)
            {
                case 0x00: OP_00(); return;
                case 0x01: OP_01(); return;
                case 0x02: OP_02(); return;
                case 0x03: OP_03(); return;
                case 0x04: OP_04(); return;
                case 0x05: OP_05(); return;
                case 0x06: OP_06(); return;
                case 0x07: OP_07(); return;
                case 0x08: OP_08(); return;
                case 0x09: OP_09(); return;
                case 0x0A: OP_0A(); return;
                case 0x0B: OP_0B(); return;
                case 0x0C: OP_0C(); return;
                case 0x0D: OP_0D(); return;
                case 0x0E: OP_0E(); return;
                case 0x0F: OP_0F(); return;
                case 0x10: OP_10(); return;
                case 0x11: OP_11(); return;
                case 0x12: OP_12(); return;
                case 0x13: OP_13(); return;
                case 0x14: OP_14(); return;
                case 0x15: OP_15(); return;
                case 0x16: OP_16(); return;
                case 0x17: OP_17(); return;
                case 0x18: OP_18(); return;
                case 0x19: OP_19(); return;
                case 0x1A: OP_1A(); return;
                case 0x1B: OP_1B(); return;
                case 0x1C: OP_1C(); return;
                case 0x1D: OP_1D(); return;
                case 0x1E: OP_1E(); return;
                case 0x1F: OP_1F(); return;
                case 0x20: OP_20(); return;
                case 0x21: OP_21(); return;
                case 0x22: OP_22(); return;
                case 0x23: OP_23(); return;
                case 0x24: OP_24(); return;
                case 0x25: OP_25(); return;
                case 0x26: OP_26(); return;
                case 0x27: OP_27(); return;
                case 0x28: OP_28(); return;
                case 0x29: OP_29(); return;
                case 0x2A: OP_2A(); return;
                case 0x2B: OP_2B(); return;
                case 0x2C: OP_2C(); return;
                case 0x2D: OP_2D(); return;
                case 0x2E: OP_2E(); return;
                case 0x2F: OP_2F(); return;
                case 0x30: OP_30(); return;
                case 0x31: OP_31(); return;
                case 0x32: OP_32(); return;
                case 0x33: OP_33(); return;
                case 0x34: OP_34(); return;
                case 0x35: OP_35(); return;
                case 0x36: OP_36(); return;
                case 0x37: OP_37(); return;
                case 0x38: OP_38(); return;
                case 0x39: OP_39(); return;
                case 0x3A: OP_3A(); return;
                case 0x3B: OP_3B(); return;
                case 0x3C: OP_3C(); return;
                case 0x3D: OP_3D(); return;
                case 0x3E: OP_3E(); return;
                case 0x3F: OP_3F(); return;
                case 0x40: OP_40(); return;
                case 0x41: OP_41(); return;
                case 0x42: OP_42(); return;
                case 0x43: OP_43(); return;
                case 0x44: OP_44(); return;
                case 0x45: OP_45(); return;
                case 0x46: OP_46(); return;
                case 0x47: OP_47(); return;
                case 0x48: OP_48(); return;
                case 0x49: OP_49(); return;
                case 0x4A: OP_4A(); return;
                case 0x4B: OP_4B(); return;
                case 0x4C: OP_4C(); return;
                case 0x4D: OP_4D(); return;
                case 0x4E: OP_4E(); return;
                case 0x4F: OP_4F(); return;
                case 0x50: OP_50(); return;
                case 0x51: OP_51(); return;
                case 0x52: OP_52(); return;
                case 0x53: OP_53(); return;
                case 0x54: OP_54(); return;
                case 0x55: OP_55(); return;
                case 0x56: OP_56(); return;
                case 0x57: OP_57(); return;
                case 0x58: OP_58(); return;
                case 0x59: OP_59(); return;
                case 0x5A: OP_5A(); return;
                case 0x5B: OP_5B(); return;
                case 0x5C: OP_5C(); return;
                case 0x5D: OP_5D(); return;
                case 0x5E: OP_5E(); return;
                case 0x5F: OP_5F(); return;
                case 0x60: OP_60(); return;
                case 0x61: OP_61(); return;
                case 0x62: OP_62(); return;
                case 0x63: OP_63(); return;
                case 0x64: OP_64(); return;
                case 0x65: OP_65(); return;
                case 0x66: OP_66(); return;
                case 0x67: OP_67(); return;
                case 0x68: OP_68(); return;
                case 0x69: OP_69(); return;
                case 0x6A: OP_6A(); return;
                case 0x6B: OP_6B(); return;
                case 0x6C: OP_6C(); return;
                case 0x6D: OP_6D(); return;
                case 0x6E: OP_6E(); return;
                case 0x6F: OP_6F(); return;
                case 0x70: OP_70(); return;
                case 0x71: OP_71(); return;
                case 0x72: OP_72(); return;
                case 0x73: OP_73(); return;
                case 0x74: OP_74(); return;
                case 0x75: OP_75(); return;
                case 0x76: OP_76(); return;
                case 0x77: OP_77(); return;
                case 0x78: OP_78(); return;
                case 0x79: OP_79(); return;
                case 0x7A: OP_7A(); return;
                case 0x7B: OP_7B(); return;
                case 0x7C: OP_7C(); return;
                case 0x7D: OP_7D(); return;
                case 0x7E: OP_7E(); return;
                case 0x7F: OP_7F(); return;
                case 0x80: OP_80(); return;
                case 0x81: OP_81(); return;
                case 0x82: OP_82(); return;
                case 0x83: OP_83(); return;
                case 0x84: OP_84(); return;
                case 0x85: OP_85(); return;
                case 0x86: OP_86(); return;
                case 0x87: OP_87(); return;
                case 0x88: OP_88(); return;
                case 0x89: OP_89(); return;
                case 0x8A: OP_8A(); return;
                case 0x8B: OP_8B(); return;
                case 0x8C: OP_8C(); return;
                case 0x8D: OP_8D(); return;
                case 0x8E: OP_8E(); return;
                case 0x8F: OP_8F(); return;
                case 0x90: OP_90(); return;
                case 0x91: OP_91(); return;
                case 0x92: OP_92(); return;
                case 0x93: OP_93(); return;
                case 0x94: OP_94(); return;
                case 0x95: OP_95(); return;
                case 0x96: OP_96(); return;
                case 0x97: OP_97(); return;
                case 0x98: OP_98(); return;
                case 0x99: OP_99(); return;
                case 0x9A: OP_9A(); return;
                case 0x9B: OP_9B(); return;
                case 0x9C: OP_9C(); return;
                case 0x9D: OP_9D(); return;
                case 0x9E: OP_9E(); return;
                case 0x9F: OP_9F(); return;
                case 0xA0: OP_A0(); return;
                case 0xA1: OP_A1(); return;
                case 0xA2: OP_A2(); return;
                case 0xA3: OP_A3(); return;
                case 0xA4: OP_A4(); return;
                case 0xA5: OP_A5(); return;
                case 0xA6: OP_A6(); return;
                case 0xA7: OP_A7(); return;
                case 0xA8: OP_A8(); return;
                case 0xA9: OP_A9(); return;
                case 0xAA: OP_AA(); return;
                case 0xAB: OP_AB(); return;
                case 0xAC: OP_AC(); return;
                case 0xAD: OP_AD(); return;
                case 0xAE: OP_AE(); return;
                case 0xAF: OP_AF(); return;
                case 0xB0: OP_B0(); return;
                case 0xB1: OP_B1(); return;
                case 0xB2: OP_B2(); return;
                case 0xB3: OP_B3(); return;
                case 0xB4: OP_B4(); return;
                case 0xB5: OP_B5(); return;
                case 0xB6: OP_B6(); return;
                case 0xB7: OP_B7(); return;
                case 0xB8: OP_B8(); return;
                case 0xB9: OP_B9(); return;
                case 0xBA: OP_BA(); return;
                case 0xBB: OP_BB(); return;
                case 0xBC: OP_BC(); return;
                case 0xBD: OP_BD(); return;
                case 0xBE: OP_BE(); return;
                case 0xBF: OP_BF(); return;
                case 0xC0: OP_C0(); return;
                case 0xC1: OP_C1(); return;
                case 0xC2: OP_C2(); return;
                case 0xC3: OP_C3(); return;
                case 0xC4: OP_C4(); return;
                case 0xC5: OP_C5(); return;
                case 0xC6: OP_C6(); return;
                case 0xC7: OP_C7(); return;
                case 0xC8: OP_C8(); return;
                case 0xC9: OP_C9(); return;
                case 0xCA: OP_CA(); return;
                case 0xCB: OP_CB(); return;
                case 0xCC: OP_CC(); return;
                case 0xCD: OP_CD(); return;
                case 0xCE: OP_CE(); return;
                case 0xCF: OP_CF(); return;
                case 0xD0: OP_D0(); return;
                case 0xD1: OP_D1(); return;
                case 0xD2: OP_D2(); return;
                case 0xD3: OP_D3(); return;
                case 0xD4: OP_D4(); return;
                case 0xD5: OP_D5(); return;
                case 0xD6: OP_D6(); return;
                case 0xD7: OP_D7(); return;
                case 0xD8: OP_D8(); return;
                case 0xD9: OP_D9(); return;
                case 0xDA: OP_DA(); return;
                case 0xDB: OP_DB(); return;
                case 0xDC: OP_DC(); return;
                case 0xDD: OP_DD(); return;
                case 0xDE: OP_DE(); return;
                case 0xDF: OP_DF(); return;
                case 0xE0: OP_E0(); return;
                case 0xE1: OP_E1(); return;
                case 0xE2: OP_E2(); return;
                case 0xE3: OP_E3(); return;
                case 0xE4: OP_E4(); return;
                case 0xE5: OP_E5(); return;
                case 0xE6: OP_E6(); return;
                case 0xE7: OP_E7(); return;
                case 0xE8: OP_E8(); return;
                case 0xE9: OP_E9(); return;
                case 0xEA: OP_EA(); return;
                case 0xEB: OP_EB(); return;
                case 0xEC: OP_EC(); return;
                case 0xED: OP_ED(); return;
                case 0xEE: OP_EE(); return;
                case 0xEF: OP_EF(); return;
                case 0xF0: OP_F0(); return;
                case 0xF1: OP_F1(); return;
                case 0xF2: OP_F2(); return;
                case 0xF3: OP_F3(); return;
                case 0xF4: OP_F4(); return;
                case 0xF5: OP_F5(); return;
                case 0xF6: OP_F6(); return;
                case 0xF7: OP_F7(); return;
                case 0xF8: OP_F8(); return;
                case 0xF9: OP_F9(); return;
                case 0xFA: OP_FA(); return;
                case 0xFB: OP_FB(); return;
                case 0xFC: OP_FC(); return;
                case 0xFD: OP_FD(); return;
                case 0xFE: OP_FE(); return;
                case 0xFF: OP_FF(); return;
                default: throw new NotImplementedException("INVALID OPCODE - " + opcode.ToString("X2"));
            }
        }

        private void OP_00()
        {
            // NOP
        }

        private void OP_01()
        {
            registers.C = memory[(uint)(registers.PC + 1)];
            registers.B = memory[(uint)(registers.PC + 2)];
            registers.PC += 2;
        }

        private void OP_02()
        {
            var addr = registers.BC;
            memory[addr] = registers.A;
        }

        private void OP_03()
        {
            var addr = registers.BC;
            addr++;
            registers.BC = addr;
        }

        private void OP_04()
        {
            registers.B++;
            registers.Flags.UpdateZSP(registers.B);
        }

        private void OP_05()
        {
            registers.B--;
            registers.Flags.UpdateZSP(registers.B);
        }

        private void OP_06()
        {
            registers.B = memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_07()
        {
            int bit7 = ((registers.A & 0x80) == 0x80) ? 1 : 0;
            registers.A = (byte)((registers.A << 1) | bit7);
            registers.Flags.CY = (byte)bit7;
        }

        private void OP_08()
        { }

        private void OP_09()
        {
            var addr = registers.HL + registers.BC;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = (ulong)(addr & 0xFFFF);
        }

        private void OP_0A()
        {
            var addr = registers.BC;
            registers.A = memory[addr];
        }

        private void OP_0B()
        {
            var addr = registers.BC;
            addr--;
            registers.BC = addr;
        }

        private void OP_0C()
        {
            registers.C++;
            registers.Flags.UpdateZSP(registers.C);
        }

        private void OP_0D()
        {
            registers.C--;
            registers.Flags.UpdateZSP(registers.C);
        }

        private void OP_0E()
        {
            registers.C = memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_0F()
        {
            int bit0 = registers.A & 0x01;
            registers.A >>= 1;
            registers.A |= (byte)(bit0 << 7);
            registers.Flags.CY = (byte)bit0;
        }

        private void OP_10()
        { }

        private void OP_11()
        {
            registers.D = memory[registers.PC + 2];
            registers.E = memory[registers.PC + 1];
            registers.PC += 2;
        }

        private void OP_12()
        {
            var addr = registers.DE;
            memory[addr] = registers.A;
        }

        private void OP_13()
        {
            var addr = registers.DE; ;
            addr++;
            registers.DE = addr;
        }

        private void OP_14()
        {
            registers.D++;
            registers.Flags.UpdateZSP(registers.D);
        }

        private void OP_15()
        {
            registers.D--;
            registers.Flags.UpdateZSP(registers.D);
        }

        private void OP_16()
        {
            registers.D = memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_17()
        {
            uint bit7 = (uint)(((registers.A & 128) == 128) ? 1 : 0);
            uint bit0 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A << 1) | bit0);
            registers.Flags.CY = bit7;
        }

        private void OP_18()
        { }

        private void OP_19()
        {
            var addr = registers.DE + registers.HL;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = addr & 0xFFFF;
        }

        private void OP_1A()
        {
            var addr = registers.DE;
            registers.A = memory[addr];
        }

        private void OP_1B()
        {
            ushort addr = (ushort)registers.DE;
            addr--;
            registers.DE = addr;
        }

        private void OP_1C()
        {
            registers.E++;
            registers.Flags.UpdateZSP(registers.E);
        }

        private void OP_1D()
        {
            registers.E--;
            registers.Flags.UpdateZSP(registers.E);
        }

        private void OP_1E()
        {
            registers.E = memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_1F()
        {
            int bit0 = registers.A & 1;
            uint bit7 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A >> 1) | (bit7 << 7));
            registers.Flags.CY = (byte)bit0;
        }

        private void OP_20()
        { }

        private void OP_21()
        {
            registers.H = memory[registers.PC + 2];
            registers.L = memory[registers.PC + 1];
            registers.PC += 2;
        }

        private void OP_22()
        {
            var addr = ReadOpcodeWord();
            memory[addr] = registers.L;
            memory[addr + 1] = registers.H;
            registers.PC += 2;
        }

        private void OP_23()
        {
            var addr = registers.HL;
            addr++;
            registers.HL = addr;
        }

        private void OP_24()
        {
            registers.H++;
            registers.Flags.UpdateZSP(registers.H);
        }

        private void OP_25()
        {
            registers.H--;
            registers.Flags.UpdateZSP(registers.H);
        }

        private void OP_26()
        {
            registers.H = memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_27()
        {
            if ((registers.A & 0x0F) > 9)
            {
                registers.A += 6;
            }
            if ((1 == registers.Flags.CY) || ((registers.A & 0xF0) > 0x90))
            {
                registers.A += 0x60;
                registers.Flags.CY = 1;
                registers.Flags.UpdateZSP(registers.A);
            }
        }

        private void OP_28()
        { }

        private void OP_29()
        {
            var addr = registers.HL + registers.HL;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = addr & 0xFFFF;
        }

        private void OP_2A()
        {
            var addr = ReadOpcodeWord();
            registers.L = memory[addr];
            registers.H = memory[addr + 1];
            registers.PC += 2;
        }

        private void OP_2B()
        {
            var addr = registers.HL;
            addr--;
            registers.HL = addr;
        }

        private void OP_2C()
        {
            registers.L++;
            registers.Flags.UpdateZSP(registers.L);
        }

        private void OP_2D()
        {
            registers.L--;
            registers.Flags.UpdateZSP(registers.L);
        }

        private void OP_2E()
        {
            registers.L = memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_2F()
        {
            registers.A = (byte)~registers.A;
        }

        private void OP_30()
        { }

        private void OP_31()
        {
            registers.SP = ReadOpcodeWord();
            registers.PC += 2;
        }

        private void OP_32()
        {
            ushort addr = ReadOpcodeWord();
            memory[addr] = registers.A;
            registers.PC += 2;
        }

        private void OP_33()
        {
            registers.SP++;
        }

        private void OP_34()
        {
            var addr = registers.HL;
            var value = memory[addr];
            value++;
            registers.Flags.UpdateZSP(value);
            memory[addr] = (byte)(value & 0xFF);
        }

        private void OP_35()
        {
            var addr = registers.HL;
            var value = memory[addr];
            value--;
            registers.Flags.UpdateZSP(value);
            memory[addr] = (byte)(value & 0xFF);
        }

        private void OP_36()
        {
            var addr = registers.HL;
            var value = memory[registers.PC + 1];
            memory[addr] = value;
            registers.PC++;
        }

        private void OP_37()
        {
            registers.Flags.CY = 1;
        }

        private void OP_38()
        { }

        private void OP_39()
        {
            var value = registers.HL + registers.SP;
            registers.Flags.UpdateCarryWord(value);
            registers.HL = (value & 0xFFFF);
        }

        private void OP_3A()
        {
            ushort addr = ReadOpcodeWord();
            registers.A = memory[addr];
            registers.PC += 2;
        }

        private void OP_3B()
        {
            registers.SP--;
        }

        private void OP_3C()
        {
            registers.A++;
            registers.Flags.UpdateZSP(registers.A);
        }

        private void OP_3D()
        {
            registers.A--;
            registers.Flags.UpdateZSP(registers.A);
        }

        private void OP_3E()
        {
            var addr = memory[registers.PC + 1];
            registers.A = addr;
            registers.PC++;
        }

        private void OP_3F()
        {
            registers.Flags.CY = (byte)~registers.Flags.CY;
        }

        private void OP_40()
        { }

        private void OP_41()
        {
            registers.B = registers.C;
        }

        private void OP_42()
        {
            registers.B = registers.D;
        }

        private void OP_43()
        {
            registers.B = registers.E;
        }

        private void OP_44()
        {
            registers.B = registers.H;
        }

        private void OP_45()
        {
            registers.B = registers.L;
        }

        private void OP_46()
        {
            var addr = registers.HL;
            registers.B = memory[addr];
        }

        private void OP_47()
        {
            registers.B = registers.A;
        }

        private void OP_48()
        {
            registers.C = registers.B;
        }

        private void OP_49()
        {
            registers.C = registers.C;
        }

        private void OP_4A()
        {
            registers.C = registers.D;
        }

        private void OP_4B()
        {
            registers.C = registers.E;
        }

        private void OP_4C()
        {
            registers.C = registers.H;
        }

        private void OP_4D()
        {
            registers.C = registers.L;
        }

        private void OP_4E()
        {
            var addr = registers.HL;
            registers.C = memory[addr];
        }

        private void OP_4F()
        {
            registers.C = registers.A;
        }

        private void OP_50()
        {
            registers.D = registers.B;
        }

        private void OP_51()
        {
            registers.D = registers.C;
        }

        private void OP_52()
        {
            registers.D = registers.D;
        }

        private void OP_53()
        {
            registers.D = registers.E;
        }

        private void OP_54()
        {
            registers.D = registers.H;
        }

        private void OP_55()
        {
            registers.D = registers.L;
        }

        private void OP_56()
        {
            var addr = registers.HL;
            registers.D = memory[addr];
        }

        private void OP_57()
        {
            registers.D = registers.A;
        }

        private void OP_58()
        {
            registers.E = registers.B;
        }

        private void OP_59()
        {
            registers.E = registers.C;
        }

        private void OP_5A()
        {
            registers.E = registers.D;
        }

        private void OP_5B()
        {
            registers.E = registers.E;
        }

        private void OP_5C()
        {
            registers.E = registers.H;
        }

        private void OP_5D()
        {
            registers.E = registers.L;
        }

        private void OP_5E()
        {
            var addr = registers.HL;
            registers.E = memory[addr];
        }

        private void OP_5F()
        {
            registers.E = registers.A;
        }

        private void OP_60()
        {
            registers.H = registers.B;
        }

        private void OP_61()
        {
            registers.H = registers.C;
        }

        private void OP_62()
        {
            registers.H = registers.D;
        }

        private void OP_63()
        {
            registers.H = registers.E;
        }

        private void OP_64()
        {
            registers.H = registers.H;
        }

        private void OP_65()
        {
            registers.H = registers.L;
        }

        private void OP_66()
        {
            var addr = registers.HL;
            registers.H = memory[addr];
        }

        private void OP_67()
        {
            registers.H = registers.A;
        }

        private void OP_68()
        {
            registers.L = registers.B;
        }

        private void OP_69()
        {
            registers.L = registers.C;
        }

        private void OP_6A()
        {
            registers.L = registers.D;
        }

        private void OP_6B()
        {
            registers.L = registers.E;
        }

        private void OP_6C()
        {
            registers.L = registers.H;
        }

        private void OP_6D()
        { }

        private void OP_6E()
        {
            var addr = registers.HL;
            registers.L = memory[addr];
        }

        private void OP_6F()
        {
            registers.L = registers.A;
        }

        private void OP_70()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.B;
        }

        private void OP_71()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.C;
        }

        private void OP_72()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.D;
        }

        private void OP_73()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.E;
        }

        private void OP_74()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.H;
        }

        private void OP_75()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.L;
        }

        private void OP_76()
        { }

        private void OP_77()
        {
            ulong addr = registers.HL;
            memory[addr] = registers.A;
        }

        private void OP_78()
        {
            registers.A = registers.B;
        }

        private void OP_79()
        {
            registers.A = registers.C;
        }

        private void OP_7A()
        {
            registers.A = registers.D;
        }

        private void OP_7B()
        {
            registers.A = registers.E;
        }

        private void OP_7C()
        {
            registers.A = registers.H;
        }

        private void OP_7D()
        {
            registers.A = registers.L;
        }

        private void OP_7E()
        {
            var addr = registers.HL;
            registers.A = memory[addr];
        }

        private void OP_7F()
        { }

        private void OP_80()
        {
            uint addr = (uint)registers.A + (uint)registers.B;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_81()
        {
            var addr = (uint)registers.A + (uint)registers.C;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_82()
        {
            var addr = (uint)registers.A + (uint)registers.D;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_83()
        {
            var addr = (uint)registers.A + (uint)registers.E;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_84()
        {
            var addr = (uint)registers.A + (uint)registers.H;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_85()
        {
            var addr = (uint)registers.A + (uint)registers.L;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_86()
        {
            var addr = (uint)registers.A + (uint)memory[registers.HL];
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_87()
        {
            var addr = (uint)registers.A + (uint)registers.A;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_88()
        {
            var addr = (uint)registers.A + (uint)registers.B + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_89()
        {
            var addr = (uint)registers.A + (uint)registers.C + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8A()
        {
            var addr = (uint)registers.A + (uint)registers.D + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8B()
        {
            var addr = (uint)registers.A + (uint)registers.E + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8C()
        {
            var addr = (uint)registers.A + (uint)registers.H + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8D()
        {
            var addr = (uint)registers.A + (uint)registers.L + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8E()
        {
            var addr = (uint)registers.A + (uint)memory[registers.HL] + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8F()
        {
            var addr = (uint)registers.A + (uint)registers.A + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_90()
        {
            var addr = (uint)registers.A - (uint)registers.B;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_91()
        {
            var addr = (uint)registers.A - (uint)registers.C;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_92()
        {
            var addr = (uint)registers.A - (uint)registers.D;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_93()
        {
            var addr = (uint)registers.A - (uint)registers.E;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_94()
        {
            var addr = (uint)registers.A - (uint)registers.H;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_95()
        {
            var addr = (uint)registers.A - (uint)registers.L;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_96()
        {
            var addr = (uint)registers.A - (uint)memory[registers.HL];
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_97()
        {
            var addr = (uint)registers.A - (uint)registers.A;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_98()
        {
            var addr = (uint)registers.A - (uint)registers.B - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_99()
        {
            var addr = (uint)registers.A - (uint)registers.C - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9A()
        {
            var addr = (uint)registers.A - (uint)registers.D - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9B()
        {
            var addr = (uint)registers.A - (uint)registers.E - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9C()
        {
            var addr = (uint)registers.A - (uint)registers.H - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9D()
        {
            var addr = (uint)registers.A - (uint)registers.L - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9E()
        {
            var addr2 = registers.HL;
            var addr = (uint)registers.A - (uint)memory[addr2] - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9F()
        {
            var addr = (uint)registers.A - (uint)registers.A - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_A0()
        {
            registers.A = (byte)(registers.A & registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A1()
        {
            registers.A = (byte)(registers.A & registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A2()
        {
            registers.A = (byte)(registers.A & registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A3()
        {
            registers.A = (byte)(registers.A & registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A4()
        {
            registers.A = (byte)(registers.A & registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A5()
        {
            registers.A = (byte)(registers.A & registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A6()
        {
            registers.A = (byte)(registers.A & memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A7()
        {
            registers.A = (byte)(registers.A & registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A8()
        {
            registers.A = (byte)(registers.A ^ registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_A9()
        {
            registers.A = (byte)(registers.A ^ registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_AA()
        {
            registers.A = (byte)(registers.A ^ registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_AB()
        {
            registers.A = (byte)(registers.A ^ registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_AC()
        {
            registers.A = (byte)(registers.A ^ registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_AD()
        {
            registers.A = (byte)(registers.A ^ registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_AE()
        {
            registers.A = (byte)(registers.A ^ memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_AF()
        {
            registers.A = (byte)(registers.A ^ registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B0()
        {
            registers.A = (byte)(registers.A | registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B1()
        {
            registers.A = (byte)(registers.A | registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B2()
        {
            registers.A = (byte)(registers.A | registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B3()
        {
            registers.A = (byte)(registers.A | registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B4()
        {
            registers.A = (byte)(registers.A | registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B5()
        {
            registers.A = (byte)(registers.A | registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B6()
        {
            registers.A = (byte)(registers.A | memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B7()
        {
            registers.A = (byte)(registers.A | registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateCarryByte(registers.A);
        }

        private void OP_B8()
        {
            var addr = (byte)(registers.A - registers.B);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.B)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_B9()
        {
            var addr = (byte)(registers.A - registers.C);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.C)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_BA()
        {
            var addr = (byte)(registers.A - registers.D);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.D)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_BB()
        {
            var addr = (byte)(registers.A - registers.E);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.E)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_BC()
        {
            var addr = (byte)(registers.A - registers.H);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.H)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_BD()
        {
            var addr = (byte)(registers.A - registers.L);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < registers.L)
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_BE()
        {
            var addr = (byte)(registers.A - memory[registers.HL]);
            registers.Flags.UpdateZSP(addr);
            if (registers.A < memory[registers.HL])
                registers.Flags.CY = 1;
            else
                registers.Flags.CY = 0;
        }

        private void OP_BF()
        {
            registers.Flags.Z = 1;
            registers.Flags.S = 0;
            registers.Flags.P = (uint)registers.Flags.CalculateParityFlag(registers.A);
            registers.Flags.CY = 0;
        }

        private void OP_C0()
        {
            if (registers.Flags.Z == 0)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_C1()
        {
            registers.C = memory[registers.SP];
            registers.B = memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_C2()
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
        }

        private void OP_C3()
        {
            var addr = ReadOpcodeWord();
            registers.PC = addr;
            registers.PC--;
        }

        private void OP_C4()
        {
            if (registers.Flags.Z == 0)
            {
                ushort addr = ReadOpcodeWord();
                ushort retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_C5()
        {
            memory[registers.SP - 2] = registers.C;
            memory[registers.SP - 1] = registers.B;
            registers.SP -= 2;
        }

        private void OP_C6()
        {
            ulong addr = (ulong)registers.A + memory[registers.PC + 1];
            registers.Flags.UpdateZSP((uint)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_C7()
        { }

        private void OP_C8()
        {
            if (registers.Flags.Z == 1)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_C9()
        {
            Ret();
            registers.PC--;
        }

        private void OP_CA()
        {
            if (registers.Flags.Z == 1)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_CB()
        { }

        private void OP_CC()
        {
            if (registers.Flags.Z == 1)
            {
                ulong addr = ReadOpcodeWord();
                ulong retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_CD()
        {
            ulong addr = ReadOpcodeWord();
            ulong retAddr = (ulong)(registers.PC + 3);
            Call((ushort)addr, (ushort)retAddr);
            registers.PC--;
        }

        private void OP_CE()
        {
            ulong addr = registers.A;
            addr += memory[registers.PC + 1];
            addr += registers.Flags.CY;
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP((uint)addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_CF()
        { }

        private void OP_D0()
        {
            if (registers.Flags.CY == 0)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_D1()
        {
            registers.E = memory[registers.SP];
            registers.D = memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_D2()
        {
            if (registers.Flags.CY == 0)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_D3()
        {
            uint port = memory[registers.PC + 1];
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
        }

        private void OP_D4()
        {
            if (registers.Flags.CY == 0)
            {
                ulong addr = ReadOpcodeWord();
                ulong retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_D5()
        {
            memory[registers.SP - 2] = registers.E;
            memory[registers.SP - 1] = registers.D;
            registers.SP -= 2;
        }

        private void OP_D6()
        {
            ulong data = memory[registers.PC + 1];
            ulong addr = registers.A - data;
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP((uint)addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_D7()
        { }

        private void OP_D8()
        {
            if (registers.Flags.CY == 1)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_D9()
        { }

        private void OP_DA()
        {
            if (registers.Flags.CY == 1)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_DB()
        {
            uint port = memory[registers.PC + 1];
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
        }

        private void OP_DC()
        {
            if (registers.Flags.CY == 1)
            {
                ulong addr = ReadOpcodeWord();
                ulong retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_DD()
        { }

        private void OP_DE()
        {
            ulong data = memory[registers.PC + 1];
            ulong addr = registers.A - data - registers.Flags.CY;
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP((uint)addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_DF()
        { }

        private void OP_E0()
        {
            if (registers.Flags.P == 0)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_E1()
        {
            registers.L = memory[registers.SP];
            registers.H = memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_E2()
        {
            if (registers.Flags.P == 0)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_E3()
        {
            uint l = registers.L;
            uint h = registers.H;
            registers.L = memory[registers.SP];
            registers.H = memory[registers.SP + 1];
            memory[registers.SP] = (byte)l;
            memory[registers.SP + 1] = (byte)h;
        }

        private void OP_E4()
        {
            if (registers.Flags.P == 0)
            {
                ushort addr = ReadOpcodeWord();
                ushort retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_E5()
        {
            memory[registers.SP - 2] = registers.L;
            memory[registers.SP - 1] = registers.H;
            registers.SP -= 2;
        }

        private void OP_E6()
        {
            ulong addr = (ulong)(registers.A & memory[registers.PC + 1]);
            registers.Flags.UpdateZSP((uint)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_E7()
        { }

        private void OP_E8()
        {
            if (registers.Flags.P == 1)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_E9()
        {
            registers.PC = (ushort)(registers.H << 8 | registers.L);
            registers.PC--;
        }

        private void OP_EA()
        {
            if (registers.Flags.P == 1)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_EB()
        {
            byte temp = registers.H;
            registers.H = registers.D;
            registers.D = temp;
            temp = registers.L;
            registers.L = registers.E;
            registers.E = temp;
        }

        private void OP_EC()
        {
            if (registers.Flags.P == 1)
            {
                ushort addr = ReadOpcodeWord();
                ushort retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_ED()
        { }

        private void OP_EE()
        {
            registers.A ^= memory[registers.PC + 1];
            registers.Flags.UpdateCarryByte(registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.PC++;
        }

        private void OP_EF()
        { }

        private void OP_F0()
        {
            if (registers.Flags.P == 1)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_F1()
        {
            byte flags = memory[registers.SP];
            registers.Flags.SetFromByte(flags);
            registers.A = memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_F2()
        {
            if (registers.Flags.P == 1)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_F3()
        {
            registers.INT_ENABLE = false;
        }

        private void OP_F4()
        {
            if (registers.Flags.S == 0)
            {
                ushort addr = ReadOpcodeWord();
                ushort retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_F5()
        {
            memory[registers.SP - 2] = registers.Flags.ToByte();
            memory[registers.SP - 1] = registers.A;
            registers.SP -= 2;
        }

        private void OP_F6()
        {
            ulong data = memory[registers.PC + 1];
            ulong value = registers.A | data;
            registers.Flags.UpdateCarryByte(value);
            registers.Flags.UpdateZSP((uint)value);
            registers.A = (byte)value;
            registers.PC++;
        }

        private void OP_F7()
        { }

        private void OP_F8()
        {
            if (registers.Flags.S == 1)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_F9()
        {
            registers.SP = (ushort)registers.HL;
        }

        private void OP_FA()
        {
            if (registers.Flags.S == 1)
            {
                ulong ADDR = ReadOpcodeWord();
                registers.PC = (ushort)ADDR;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_FB()
        {
            registers.INT_ENABLE = true;
        }

        private void OP_FC()
        {
            if (registers.Flags.S == 1)
            {
                ulong addr = ReadOpcodeWord();
                ulong retAddr = (ulong)(registers.PC + 3);
                Call((ushort)addr, (ushort)retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_FD()
        { }

        private void OP_FE()
        {
            ulong addr = (ulong)(registers.A - memory[registers.PC + 1]);
            registers.Flags.UpdateZSP((uint)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.PC++;
        }

        private void OP_FF()
        { }

        private ushort ReadOpcodeWord()
        {
            return (ushort)(memory[registers.PC + 2] << 8 | memory[registers.PC + 1]);
        }

        private void Call(ushort address, ushort retAddress)
        {
            byte rethi = (byte)((retAddress >> 8) & 0xFF);
            byte retlo = (byte)(retAddress & 0xFF);
            memory[registers.SP - 1] = rethi;
            memory[registers.SP - 2] = retlo;
            registers.PC = address;
            registers.SP -= 2;
        }

        private void Ret()
        {
            uint sphi = memory[registers.SP + 1];
            uint splo = memory[registers.SP];
            registers.PC = (ushort)(sphi << 8 | splo);
            registers.SP += 2;
        }

        private void Interrupt(int num)
        {
            if (!registers.INT_ENABLE) 
                return;
            byte pchi = (byte)((registers.PC >> 8) & 0xFF);
            byte pclo = (byte)(registers.PC & 0xFF);
            memory[registers.SP - 1] = pchi;
            memory[registers.SP - 2] = pclo;
            registers.SP -= 2;
            if (num == 1) 
                registers.PC = 0x0008;
            if (num == 2) 
                registers.PC = 0x0010;
        }
    }
}