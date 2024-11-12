using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection.Emit;

namespace SpaceInvaders
{
    internal class _8080CPU
    {
        private Registers registers;
        public Registers Registers { get { return registers; } }
        private long Cnt = 0;
        public bool paused = false;
        public bool step = false;
        private bool running = false;
        public bool Running
        {
            get { return running; }
            set { running = value; }
        }
        private bool displayReady = false;
        public bool DisplayReady
            { get { return displayReady; } }

        public _8080CPU()
        {
            registers = new Registers();
        }

        public void ReadROM(string filePath, int addr)
        {
            FileStream romObj = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            romObj.Seek(0, SeekOrigin.Begin);
            registers.PC = (ushort)addr;
            for (int i = addr; i < addr + romObj.Length; i++)
                registers.memory[i] = (byte)romObj.ReadByte();
        }

        public void PauseEmulation()
        {
            
        }

        public void StopEmulation()
        {
            displayReady = false;
            running = false;
        }

        public void RunEmulation()
        {
            Cnt = 0;
            long loopy = 0;
            running = true;
            double timerCounter = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
            byte prevOpcode;
            while (running)
            {
                while (paused)
                {
                    if (step) 
                    { 
                        step = false; 
                        break; 
                    }
                }
                byte opcode = registers.memory[registers.PC];

                CallOpcode(opcode);
                registers.PC++;
                Cnt++;
                loopy++;
                if(loopy % 8080000 == 0)
                { }
                 prevOpcode = opcode;
                displayReady = true;
                if (Cnt == -1)
                {
                    displayReady = true;
                    Cnt = 0;
                    while (displayReady)
                    {
                        var currentTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                        var milisecondsSinceLastUpdate = currentTime - timerCounter;
                        if (milisecondsSinceLastUpdate > 4)
                        {
                            displayReady = false;
                            timerCounter = currentTime;
                        }
                    }
                }

            }
        }

        public byte[] GetVideoRam()
        {
            byte[] videoRAM = new byte[0x1C00];
            Buffer.BlockCopy(registers.memory, 0x2400, videoRAM, 0, 0x1C00);
            return videoRAM;
        }

        private void CallOpcode(byte opcode)
        {
            if (opcode == 0x00) { NOP(); return; }
            if (opcode == 0x01) { OP_01(); return; }
            if (opcode == 0x02) { OP_02(); return; }
            if (opcode == 0x03) { OP_03(); return; }
            if (opcode == 0x04) { OP_04(); return; }
            if (opcode == 0x05) { OP_05(); return; }
            if (opcode == 0x06) { OP_06(); return; }
            if (opcode == 0x07) { OP_07(); return; }
            //if (opcode == 0x08) { OP_08(); return; }
            if (opcode == 0x09) { OP_09(); return; }
            if (opcode == 0x0A) { OP_0A(); return; }
            if (opcode == 0x0B) { OP_0B(); return; }
            if (opcode == 0x0C) { OP_0C(); return; }
            if (opcode == 0x0D) { OP_0D(); return; }
            if (opcode == 0x0E) { OP_0E(); return; }
            if (opcode == 0x0F) { OP_0F(); return; }
            //if (opcode == 0x10) { OP_10(); return; }
            if (opcode == 0x11) { OP_11(); return; }
            if (opcode == 0x12) { OP_12(); return; }
            if (opcode == 0x13) { OP_13(); return; }
            if (opcode == 0x14) { OP_14(); return; }
            if (opcode == 0x15) { OP_15(); return; }
            if (opcode == 0x16) { OP_16(); return; }
            if (opcode == 0x17) { OP_17(); return; }
            //if (opcode == 0x18) { OP_18(); return; }
            if (opcode == 0x19) { OP_19(); return; }
            if (opcode == 0x1A) { OP_1A(); return; }
            if (opcode == 0x1B) { OP_1B(); return; }
            if (opcode == 0x1C) { OP_1C(); return; }
            if (opcode == 0x1D) { OP_1D(); return; }
            if (opcode == 0x1E) { OP_1E(); return; }
            if (opcode == 0x1F) { OP_1F(); return; }
            //if (opcode == 0x20) { OP_20(); return; }
            if (opcode == 0x21) { OP_21(); return; }
            if (opcode == 0x22) { OP_22(); return; }
            if (opcode == 0x23) { OP_23(); return; }
            if (opcode == 0x24) { OP_24(); return; }
            if (opcode == 0x25) { OP_25(); return; }
            if (opcode == 0x26) { OP_26(); return; }
            if (opcode == 0x27) { OP_27(); return; }
            //if (opcode == 0x28) { OP_28(); return; }
            if (opcode == 0x29) { OP_29(); return; }
            if (opcode == 0x2A) { OP_2A(); return; }
            if (opcode == 0x2B) { OP_2B(); return; }
            if (opcode == 0x2C) { OP_2C(); return; }
            if (opcode == 0x2D) { OP_2D(); return; }
            if (opcode == 0x2E) { OP_2E(); return; }
            if (opcode == 0x2F) { OP_2F(); return; }
            //if (opcode == 0x30) { OP_30(); return; }
            if (opcode == 0x31) { OP_31(); return; }
            if (opcode == 0x32) { OP_32(); return; }
            if (opcode == 0x33) { OP_33(); return; }
            if (opcode == 0x34) { OP_34(); return; }
            if (opcode == 0x35) { OP_35(); return; }
            if (opcode == 0x36) { OP_36(); return; }
            if (opcode == 0x37) { OP_37(); return; }
            //if (opcode == 0x38) { OP_38(); return; }
            if (opcode == 0x39) { OP_39(); return; }
            if (opcode == 0x3A) { OP_3A(); return; }
            if (opcode == 0x3B) { OP_3B(); return; }
            if (opcode == 0x3C) { OP_3C(); return; }
            if (opcode == 0x3D) { OP_3D(); return; }
            if (opcode == 0x3E) { OP_3E(); return; }
            if (opcode == 0x3F) { OP_3F(); return; }
            //if (opcode == 0x40) { OP_40(); return; }
            if (opcode == 0x41) { OP_41(); return; }
            if (opcode == 0x42) { OP_42(); return; }
            if (opcode == 0x43) { OP_43(); return; }
            if (opcode == 0x44) { OP_44(); return; }
            if (opcode == 0x45) { OP_45(); return; }
            if (opcode == 0x46) { OP_46(); return; }
            if (opcode == 0x47) { OP_47(); return; }
            if (opcode == 0x48) { OP_48(); return; }
            if (opcode == 0x49) { OP_49(); return; }
            if (opcode == 0x4A) { OP_4A(); return; }
            if (opcode == 0x4B) { OP_4B(); return; }
            if (opcode == 0x4C) { OP_4C(); return; }
            if (opcode == 0x4D) { OP_4D(); return; }
            if (opcode == 0x4E) { OP_4E(); return; }
            if (opcode == 0x4F) { OP_4F(); return; }
            if (opcode == 0x50) { OP_50(); return; }
            if (opcode == 0x51) { OP_51(); return; }
            if (opcode == 0x52) { OP_52(); return; }
            if (opcode == 0x53) { OP_53(); return; }
            if (opcode == 0x54) { OP_54(); return; }
            if (opcode == 0x55) { OP_55(); return; }
            if (opcode == 0x56) { OP_56(); return; }
            if (opcode == 0x57) { OP_57(); return; }
            if (opcode == 0x58) { OP_58(); return; }
            if (opcode == 0x59) { OP_59(); return; }
            if (opcode == 0x5A) { OP_5A(); return; }
            if (opcode == 0x5B) { OP_5B(); return; }
            if (opcode == 0x5C) { OP_5C(); return; }
            if (opcode == 0x5D) { OP_5D(); return; }
            if (opcode == 0x5E) { OP_5E(); return; }
            if (opcode == 0x5F) { OP_5F(); return; }
            if (opcode == 0x60) { OP_60(); return; }
            if (opcode == 0x61) { OP_61(); return; }
            if (opcode == 0x62) { OP_62(); return; }
            if (opcode == 0x63) { OP_63(); return; }
            if (opcode == 0x64) { OP_64(); return; }
            if (opcode == 0x65) { OP_65(); return; }
            if (opcode == 0x66) { OP_66(); return; }
            if (opcode == 0x67) { OP_67(); return; }
            if (opcode == 0x68) { OP_68(); return; }
            if (opcode == 0x69) { OP_69(); return; }
            if (opcode == 0x6A) { OP_6A(); return; }
            if (opcode == 0x6B) { OP_6B(); return; }
            if (opcode == 0x6C) { OP_6C(); return; }
//            if (opcode == 0x6D) { OP_6D(); return; }
            if (opcode == 0x6E) { OP_6E(); return; }
            if (opcode == 0x6F) { OP_6F(); return; }
            if (opcode == 0x70) { OP_70(); return; }
            if (opcode == 0x71) { OP_71(); return; }
            if (opcode == 0x72) { OP_72(); return; }
            if (opcode == 0x73) { OP_73(); return; }
            if (opcode == 0x74) { OP_74(); return; }
            if (opcode == 0x75) { OP_75(); return; }
            //if (opcode == 0x76) { OP_76(); return; }
            if (opcode == 0x77) { OP_77(); return; }
            if (opcode == 0x78) { OP_78(); return; }
            if (opcode == 0x79) { OP_79(); return; }
            if (opcode == 0x7A) { OP_7A(); return; }
            if (opcode == 0x7B) { OP_7B(); return; }
            if (opcode == 0x7C) { OP_7C(); return; }
            if (opcode == 0x7D) { OP_7D(); return; }
            if (opcode == 0x7E) { OP_7E(); return; }
//            if (opcode == 0x7F) { OP_7F(); return; }
            if (opcode == 0x80) { OP_80(); return; }
            if (opcode == 0x81) { OP_81(); return; }
            if (opcode == 0x82) { OP_82(); return; }
            if (opcode == 0x83) { OP_83(); return; }
            if (opcode == 0x84) { OP_84(); return; }
            if (opcode == 0x85) { OP_85(); return; }
            if (opcode == 0x86) { OP_86(); return; }
            if (opcode == 0x87) { OP_87(); return; }
            if (opcode == 0x88) { OP_88(); return; }
            if (opcode == 0x89) { OP_89(); return; }
            if (opcode == 0x8A) { OP_8A(); return; }
            if (opcode == 0x8B) { OP_8B(); return; }
            if (opcode == 0x8C) { OP_8C(); return; }
            if (opcode == 0x8D) { OP_8D(); return; }
            if (opcode == 0x8E) { OP_8E(); return; }
            if (opcode == 0x8F) { OP_8F(); return; }
            if (opcode == 0x90) { OP_90(); return; }
            if (opcode == 0x91) { OP_91(); return; }
            if (opcode == 0x92) { OP_92(); return; }
            if (opcode == 0x93) { OP_93(); return; }
            if (opcode == 0x94) { OP_94(); return; }
            if (opcode == 0x95) { OP_95(); return; }
            if (opcode == 0x96) { OP_96(); return; }
            if (opcode == 0x97) { OP_97(); return; }
            if (opcode == 0x98) { OP_98(); return; }
            if (opcode == 0x99) { OP_99(); return; }
            if (opcode == 0x9A) { OP_9A(); return; }
            if (opcode == 0x9B) { OP_9B(); return; }
            if (opcode == 0x9C) { OP_9C(); return; }
            if (opcode == 0x9D) { OP_9D(); return; }
            if (opcode == 0x9E) { OP_9E(); return; }
            if (opcode == 0x9F) { OP_9F(); return; }
            if (opcode == 0xA0) { OP_A0(); return; }
            if (opcode == 0xA1) { OP_A1(); return; }
            if (opcode == 0xA2) { OP_A2(); return; }
            if (opcode == 0xA3) { OP_A3(); return; }
            if (opcode == 0xA4) { OP_A4(); return; }
            if (opcode == 0xA5) { OP_A5(); return; }
            if (opcode == 0xA6) { OP_A6(); return; }
            if (opcode == 0xA7) { OP_A7(); return; }
            if (opcode == 0xA8) { OP_A8(); return; }
            if (opcode == 0xA9) { OP_A9(); return; }
            if (opcode == 0xAA) { OP_AA(); return; }
            if (opcode == 0xAB) { OP_AB(); return; }
            if (opcode == 0xAC) { OP_AC(); return; }
            if (opcode == 0xAD) { OP_AD(); return; }
            if (opcode == 0xAE) { OP_AE(); return; }
            if (opcode == 0xAF) { OP_AF(); return; }
            if (opcode == 0xB0) { OP_B0(); return; }
            if (opcode == 0xB1) { OP_B1(); return; }
            if (opcode == 0xB2) { OP_B2(); return; }
            if (opcode == 0xB3) { OP_B3(); return; }
            if (opcode == 0xB4) { OP_B4(); return; }
            if (opcode == 0xB5) { OP_B5(); return; }
            if (opcode == 0xB6) { OP_B6(); return; }
            if (opcode == 0xB7) { OP_B7(); return; }
            if (opcode == 0xB8) { OP_B8(); return; }
            if (opcode == 0xB9) { OP_B9(); return; }
            if (opcode == 0xBA) { OP_BA(); return; }
            if (opcode == 0xBB) { OP_BB(); return; }
            if (opcode == 0xBC) { OP_BC(); return; }
            if (opcode == 0xBD) { OP_BD(); return; }
            if (opcode == 0xBE) { OP_BE(); return; }
            if (opcode == 0xBF) { OP_BF(); return; }
            if (opcode == 0xC0) { OP_C0(); return; }
            if (opcode == 0xC1) { OP_C1(); return; }
            if (opcode == 0xC2) { OP_C2(); return; }
            if (opcode == 0xC3) { OP_C3(); return; }
            if (opcode == 0xC4) { OP_C4(); return; }
            if (opcode == 0xC5) { OP_C5(); return; }
            if (opcode == 0xC6) { OP_C6(); return; }
            //if (opcode == 0xC7) { OP_C7(); return; }
            if (opcode == 0xC8) { OP_C8(); return; }
            if (opcode == 0xC9) { OP_C9(); return; }
            if (opcode == 0xCA) { OP_CA(); return; }
            //if (opcode == 0xCB) { OP_CB(); return; }
            if (opcode == 0xCC) { OP_CC(); return; }
            if (opcode == 0xCD) { OP_CD(); return; }
            if (opcode == 0xCE) { OP_CE(); return; }
            //if (opcode == 0xCF) { OP_CF(); return; }
            if (opcode == 0xD0) { OP_D0(); return; }
            if (opcode == 0xD1) { OP_D1(); return; }
            if (opcode == 0xD2) { OP_D2(); return; }
            if (opcode == 0xD3) { OP_D3(); return; }
            if (opcode == 0xD4) { OP_D4(); return; }
            if (opcode == 0xD5) { OP_D5(); return; }
            if (opcode == 0xD6) { OP_D6(); return; }
            //if (opcode == 0xD7) { OP_D7(); return; }
            if (opcode == 0xD8) { OP_D8(); return; }
            //if (opcode == 0xD9) { OP_D9(); return; }
            if (opcode == 0xDA) { OP_DA(); return; }
            if (opcode == 0xDB) { OP_DB(); return; }
            if (opcode == 0xDC) { OP_DC(); return; }
            //if (opcode == 0xDD) { OP_DD(); return; }
            if (opcode == 0xDE) { OP_DE(); return; }
            //if (opcode == 0xDF) { OP_DF(); return; }
            if (opcode == 0xE0) { OP_E0(); return; }
            if (opcode == 0xE1) { OP_E1(); return; }
            if (opcode == 0xE2) { OP_E2(); return; }
            if (opcode == 0xE3) { OP_E3(); return; }
            if (opcode == 0xE4) { OP_E4(); return; }
            if (opcode == 0xE5) { OP_E5(); return; }
            if (opcode == 0xE6) { OP_E6(); return; }
            //if (opcode == 0xE7) { OP_E7(); return; }
            if (opcode == 0xE8) { OP_E8(); return; }
            if (opcode == 0xE9) { OP_E9(); return; }
            if (opcode == 0xEA) { OP_EA(); return; }
            if (opcode == 0xEB) { OP_EB(); return; }
            if (opcode == 0xEC) { OP_EC(); return; }
            //if (opcode == 0xED) { OP_ED(); return; }
            if (opcode == 0xEE) { OP_EE(); return; }
            //if (opcode == 0xEF) { OP_EF(); return; }
            if (opcode == 0xF0) { OP_F0(); return; }
            if (opcode == 0xF1) { OP_F1(); return; }
            if (opcode == 0xF2) { OP_F2(); return; }
            if (opcode == 0xF3) { OP_F3(); return; }
            if (opcode == 0xF4) { OP_F4(); return; }
            if (opcode == 0xF5) { OP_F5(); return; }
            if (opcode == 0xF6) { OP_F6(); return; }
            //if (opcode == 0xF7) { OP_F7(); return; }
            if (opcode == 0xF8) { OP_F8(); return; }
            if (opcode == 0xF9) { OP_F9(); return; }
            if (opcode == 0xFA) { OP_FA(); return; }
            if (opcode == 0xFB) { OP_FB(); return; }
            if (opcode == 0xFC) { OP_FC(); return; }
            //if (opcode == 0xFD) { OP_FD(); return; }
            if (opcode == 0xFE) { OP_FE(); return; }
            //if (opcode == 0xFF) { OP_FF(); return; }
            Debug.WriteLine("INVALID OPCODE - " + opcode.ToString("X2"));
        }

        private void OP_01()
        {
            registers.C = registers.memory[(uint)(registers.PC + 1)];
            registers.B = registers.memory[(uint)(registers.PC + 2)];
            registers.PC += 2;
        }

        private void OP_02()
        {
            var addr = registers.BC;
            registers.memory[addr] = registers.A;
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
            uint addr = (uint)(registers.B - 1);
            registers.Flags.UpdateZSP(addr);
            registers.B = (byte)addr;
        }

        private void OP_06()
        {
            registers.B = registers.memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_07()
        {
            int bit7 = ((registers.A & 128) == 128) ? 1 : 0;
            registers.A = (byte)((registers.A << 1) | bit7);
            registers.Flags.CY = (byte)bit7;
        }

        //private void OP_08()
        //{ } // NOP

        private void OP_09()
        {
            var addr = registers.HL + registers.BC;
            registers.Flags.UpdateWordCY(addr);
            registers.HL = (ulong)(addr & 0xFFFF);
        }

        private void OP_0A()
        {
            var addr = registers.BC;
            registers.A = registers.memory[addr];
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
            registers.C = registers.memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_0F()
        {
            int bit0 = registers.A & 0x01;
            registers.A >>= 1;
            registers.A |= (byte)(bit0 << 7);
            registers.Flags.CY = (byte)bit0;
        }

        //private void OP_10()
        //{ } // NOP

        private void OP_11()
        {
            registers.D = registers.memory[registers.PC + 2];
            registers.E = registers.memory[registers.PC + 1];
            registers.PC += 2;
        }

        private void OP_12()
        {
            var addr = registers.DE;
            registers.memory[addr] = registers.A;
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
            registers.D = registers.memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_17()
        {
            uint bit7 = (uint)(((registers.A & 128) == 128) ? 1 : 0);
            uint bit0 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A << 1) | bit0);
            registers.Flags.CY = bit7;
        }

        //private void OP_18()
        //{ } // NOP

        private void OP_19()
        {
            var addr = registers.DE + registers.HL;
            registers.Flags.UpdateWordCY(addr);
            registers.HL = addr & 0xFFFF;
        }

        private void OP_1A()
        {
            var addr = registers.DE;
            registers.A = registers.memory[addr];
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
            registers.E = registers.memory[registers.PC + 1];
            registers.PC++;
        }

        private void OP_1F()
        {
            int bit0 = registers.A & 1;
            uint bit7 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A >> 1) | (bit7 << 7));
            registers.Flags.CY = (byte)bit0;
        }

        //private void OP_20()
        //{ } // NOP

        private void OP_21()
        {
            registers.H = registers.memory[registers.PC + 2];
            registers.L = registers.memory[registers.PC + 1];
            registers.PC += 2;
        }

        private void OP_22()
        {
            var addr = ReadOpcodeWord();
            registers.memory[addr] = registers.L;
            registers.memory[addr + 1] = registers.H;
            registers.PC += 2;
        }

        private void OP_23()
        {
            var addr = registers.HL;
            addr ++;
            registers.HL = addr;
        }

        private void OP_24()
        {
            registers.H ++;
            registers.Flags.UpdateZSP(registers.H);
        }

        private void OP_25()
        {
            registers.H --;
            registers.Flags.UpdateZSP(registers.H);
        }

        private void OP_26()
        {
            registers.H = registers.memory[registers.PC + 1];
            registers.PC ++;
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

        //private void OP_28()
        //{ } // NOP

        private void OP_29()
        {
            var addr = registers.HL + registers.HL;
            registers.Flags.UpdateWordCY(addr);
            registers.HL = addr & 0xFFFF;
        }

        private void OP_2A()
        {
            var addr = ReadOpcodeWord();
            registers.L = registers.memory[addr];
            registers.H = registers.memory[addr + 1];
            registers.PC += 2;
        }

        private void OP_2B()
        {
            var addr = registers.HL;
            addr --;
            registers.HL = addr;
        }

        private void OP_2C()
        {
            registers.L ++;
            registers.Flags.UpdateZSP(registers.L);
        }

        private void OP_2D()
        {
            registers.L --;
            registers.Flags.UpdateZSP(registers.L);
        }

        private void OP_2E()
        {
            registers.L = registers.memory[registers.PC + 1];
            registers.PC ++;
        }

        private void OP_2F()
        {
            registers.A = registers.A;
        }

        //private void OP_30()
        //{ } // NOP

        private void OP_31()
        {
            registers.SP = ReadOpcodeWord();
            registers.PC += 2;
        }

        private void OP_32()
        { 
            ushort addr = ReadOpcodeWord();
            registers.memory[addr] = registers.A;
            registers.PC += 2;
        }

        private void OP_33()
        {
            registers.SP ++;
        }

        private void OP_34()
        { 
            var addr = registers.HL;
            var value = registers.memory[addr];
            value ++;
            registers.Flags.UpdateZSP(value);
            registers.memory[addr] = (byte)(value & 0xFF);
        }

        private void OP_35()
        {
            var addr = registers.HL;
            var value = registers.memory[addr];
            value --;
            registers.Flags.UpdateZSP(value);
            registers.memory[addr] = (byte)(value & 0xFF);
        }

        private void OP_36()
        {
            var addr = registers.HL;
            var value = registers.memory[registers.PC + 1];
            registers.memory[addr] = value;
            registers.PC ++;
        }

        private void OP_37()
        {
            registers.Flags.CY = 1;
        }

        //private void OP_38()
        //{ } // NOP

        private void OP_39()
        {
            var value = registers.HL + registers.SP;
            registers.Flags.UpdateWordCY(value);
            registers.HL = (value & 0xFFFF);
        }

        private void OP_3A()
        {
            ushort addr = ReadOpcodeWord();
            registers.A = registers.memory[addr];
            registers.PC += 2;
        }

        private void OP_3B()
        {
            registers.SP --;
        }

        private void OP_3C()
        {
            registers.A ++;
            registers.Flags.UpdateZSP(registers.A);
        }

        private void OP_3D()
        {
            registers.A --;
            registers.Flags.UpdateZSP(registers.A);
        }

        private void OP_3E()
        { 
            var addr = registers.memory[registers.PC + 1];
            registers.A = addr;
            registers.PC ++;
        }

        private void OP_3F()
        {
            registers.Flags.CY = (byte)(1 - registers.Flags.CY);
        }

        //private void OP_40()
        //{ } // NOP

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
            registers.B = registers.memory[addr];
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
            registers.C = registers.memory[addr];
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
            registers.D = registers.memory[addr];
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
            registers.E = registers.memory[addr];
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
            registers.H = registers.memory[addr];
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

        //private void OP_6D()
        //{ } // NOP

        private void OP_6E()
        {
            var addr = registers.HL;
            registers.L = registers.memory[addr];
        }

        private void OP_6F()
        {
            registers.L = registers.A;
        }

        private void OP_70()
        {
            ulong addr = registers.HL;
            registers.memory[addr]= registers.B;
        }

        private void OP_71()
        {
            ulong addr = registers.HL;
            registers.memory[addr] = registers.C;
        }

        private void OP_72()
        {
            ulong addr = registers.HL;
            registers.memory[addr] = registers.D;
        }

        private void OP_73()
        {
            ulong addr = registers.HL;
            registers.memory[addr] = registers.E;
        }

        private void OP_74()
        {
            ulong addr = registers.HL;
            registers.memory[addr] = registers.H;
        }

        private void OP_75()
        {
            ulong addr = registers.HL;
            registers.memory[addr] = registers.L;
        }

        //private void OP_76()
        //{ } // NOP

        private void OP_77()
        {
            ulong addr = registers.HL;
            registers.memory[addr] = registers.A;
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
            registers.A = registers.memory[addr];
        }

        //private void OP_7F()
        //{ } // NOP

        private void OP_80()
        {
            var addr = (uint)registers.A + (uint)registers.B;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_81()
        {
            var addr = (uint)registers.A + (uint)registers.C;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_82()
        {
            var addr = (uint)registers.A + (uint)registers.D;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_83()
        {
            var addr = (uint)registers.A + (uint)registers.E;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_84()
        {
            var addr = (uint)registers.A + (uint)registers.H;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_85()
        {
            var addr = (uint)registers.A + (uint)registers.L;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_86()
        {
            var addr = (uint)registers.A + (uint)registers.memory[registers.HL];
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_87()
        {
            var addr = (uint)registers.A + (uint)registers.A;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_88()
        {
            var addr = (uint)registers.A + (uint)registers.B + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_89()
        {
            var addr = (uint)registers.A + (uint)registers.C + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8A()
        {
            var addr = (uint)registers.A + (uint)registers.D + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8B()
        {
            var addr = (uint)registers.A + (uint)registers.E + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8C()
        {
            var addr = (uint)registers.A + (uint)registers.H + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8D()
        {
            var addr = (uint)registers.A + (uint)registers.L + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8E()
        {
            var addr = (uint)registers.A + (uint)registers.memory[registers.HL] + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8F()
        {
            var addr = (uint)registers.A + (uint)registers.A + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_90()
        {
            var addr = (uint)registers.A - (uint)registers.B;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_91()
        {
            var addr = (uint)registers.A - (uint)registers.C;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_92()
        {
            var addr = (uint)registers.A - (uint)registers.D;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_93()
        {
            var addr = (uint)registers.A - (uint)registers.E;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_94()
        {
            var addr = (uint)registers.A - (uint)registers.H;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_95()
        {
            var addr = (uint)registers.A - (uint)registers.L;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_96()
        {
            var addr = (uint)registers.A - (uint)registers.memory[registers.HL];
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_97()
        {
            var addr = (uint)registers.A - (uint)registers.A;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_98()
        {
            var addr = (uint)registers.A - (uint)registers.B - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_99()
        {
            var addr = (uint)registers.A - (uint)registers.C - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9A()
        {
            var addr = (uint)registers.A - (uint)registers.D - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9B()
        {
            var addr = (uint)registers.A - (uint)registers.E - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9C()
        {
            var addr = (uint)registers.A - (uint)registers.H - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9D()
        {
            var addr = (uint)registers.A - (uint)registers.L - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9E()
        {
            var addr2 = registers.HL;
            var addr = (uint)registers.A - (uint)registers.memory[addr2] - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9F()
        {
            var addr = (uint)registers.A - (uint)registers.A - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_A0()
        {
            registers.A &= registers.B;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A1()
        {
            registers.A &= registers.C;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A2()
        {
            registers.A &= registers.D;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A3()
        {
            registers.A &= registers.E;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A4()
        {
            registers.A &= registers.H;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A5()
        {
            registers.A &= registers.L;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A6()
        {
            var addr = registers.HL;
            registers.A &= registers.memory[addr];
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);

        }

        private void OP_A7()
        {
            registers.A &= registers.A;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A8()
        {
            registers.A = (byte)(registers.A ^ registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A9()
        {
            registers.A = (byte)(registers.A ^ registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AA()
        {
            registers.A = (byte)(registers.A ^ registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AB()
        {
            registers.A = (byte)(registers.A ^ registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AC()
        {
            registers.A = (byte)(registers.A ^ registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AD()
        {
            registers.A = (byte)(registers.A ^ registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AE()
        {
            registers.A = (byte)(registers.A ^ registers.memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AF()
        {
            registers.A = (byte)(registers.A ^ registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B0()
        {
            registers.A = (byte)(registers.A | registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B1()
        {
            registers.A = (byte)(registers.A | registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B2()
        {
            registers.A = (byte)(registers.A | registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B3()
        {
            registers.A = (byte)(registers.A | registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B4()
        {
            registers.A = (byte)(registers.A | registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B5()
        {
            registers.A = (byte)(registers.A | registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B6()
        {
            registers.A = (byte)(registers.A | registers.memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B7()
        {
            registers.A = (byte)(registers.A | registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B8()
        {
            var addr = (byte)(registers.A - registers.B);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_B9()
        {
            var addr = (byte)(registers.A - registers.C);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BA()
        {
            var addr = (byte)(registers.A - registers.D);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BB()
        {
            var addr = (byte)(registers.A - registers.E);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BC()
        {
            var addr = (byte)(registers.A - registers.H);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BD()
        {
            var addr = (byte)(registers.A - registers.L);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BE()
        {
            var addr = (byte)(registers.A - registers.memory[registers.HL]);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BF()
        {
            var addr = (byte)(registers.A - registers.A);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_C0()
        { 
            if(registers.Flags.Z == 0)
            {
                Ret();
                registers.PC --;
            }
        }

        private void OP_C1()
        {
            registers.C = registers.memory[registers.SP];
            registers.B = registers.memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_C2()
        {
            if (registers.Flags.Z == 0)
            {
                var addr = ReadOpcodeWord();
                registers.PC = addr;
                registers.PC --;
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
            registers.PC --;
        }

        private void OP_C4()
        {
            if (registers.Flags.Z == 0)
            {
                ushort addr = ReadOpcodeWord();
                ushort retAddr = (ushort)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC --;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_C5()
        {
            registers.memory[registers.SP - 2] = registers.C;
            registers.memory[registers.SP - 1] = registers.B;
            registers.SP -= 2;
        }

        private void OP_C6()
        {
            ulong addr = (ulong)registers.A + registers.memory[registers.PC + 1];
            registers.Flags.UpdateZSP((uint)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        //private void OP_C7()
        //{ } // NOP

        private void OP_C8()
        { 
            if(registers.Flags.Z == 1)
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
            if(registers.Flags.Z != 0)
            {
                ulong addr = ReadOpcodeWord();
                registers.PC = (ushort)addr;
                registers.PC--;
            } else
            {
                registers.PC += 2;
            }
        }

        //private void OP_CB()
        //{ } // NOP

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
            var addr = ReadOpcodeWord();
            ushort retAddr = (ushort)(registers.PC + 3);
            Call(addr, retAddr);
            registers.PC--;
        }

        private void OP_CE()
        {
            ulong addr = registers.A;
            addr += registers.memory[registers.PC + 1];
            addr += registers.Flags.CY;
            registers.Flags.UpdateByteCY(addr);
            registers.Flags.UpdateZSP((uint)addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        //private void OP_CF()
        //{ } // NOP

        private void OP_D0()
        { 
            if(registers.Flags.CY == 0)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_D1()
        {
            registers.E = registers.memory[registers.SP];
            registers.D = registers.memory[registers.SP + 1];
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
            ulong port = registers.memory[registers.PC + 1];
// **** TO DO ****    OUT D8 (special)
            registers.PC++;
        }

        private void OP_D4()
        { 
            if(registers.Flags.CY == 0)
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
            registers.memory[registers.SP - 2] = registers.E;
            registers.memory[registers.SP - 1] = registers.D;
            registers.SP -= 2;
        }

        private void OP_D6()
        {
            ulong data = registers.memory[registers.PC + 1];
            ulong addr = registers.A - data;
            registers.Flags.UpdateByteCY(addr);
            registers.Flags.UpdateZSP((uint)addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        //private void OP_D7()
        //{ } // NOP

        private void OP_D8()
        {
            if (registers.Flags.CY == 1)
            {
                Ret();
                registers.PC--;
            }
        }

        //private void OP_D9()
        //{ } // NOP

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
            ulong port = registers.memory[registers.PC + 1];
            // **** TO DO ****    IN D8 (special)
            registers.A = 0;
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

        //private void OP_DD()
        //{ } // NOP

        private void OP_DE()
        {
            ulong data = registers.memory[registers.PC + 1];
            ulong addr = registers.A - data - registers.Flags.CY;
            registers.Flags.UpdateByteCY(addr);
            registers.Flags.UpdateZSP((uint)addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        //private void OP_DF()
        //{ } // NOP

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
            registers.L = registers.memory[registers.SP];
            registers.H = registers.memory[registers.SP + 1];
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
            registers.L = registers.memory[registers.SP];
            registers.H = registers.memory[registers.SP + 1];
            registers.memory[registers.SP] = (byte)l;
            registers.memory[registers.SP + 1] = (byte)h;
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
            registers.memory[registers.SP - 2] = registers.L;
            registers.memory[registers.SP - 1] = registers.H;
            registers.SP -= 2;
        }

        private void OP_E6()
        {
            ulong addr = (ulong)(registers.A & registers.memory[registers.PC + 1]);
            registers.Flags.UpdateZSP((uint)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        //private void OP_E7()
        //{ } // NOP

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
            registers.PC = MakeWord(registers.H, registers.L);
            registers.PC--;
        }

        private void OP_EA()
        { 
            if(registers.Flags.P == 1)
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

        //private void OP_ED()
        //{ } // NOP

        private void OP_EE()
        {
            registers.A ^= registers.memory[registers.PC + 1];
            registers.Flags.UpdateByteCY(registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.PC++;
        }

        //private void OP_EF()
        //{ } // NOP

        private void OP_F0()
        { 
            if(registers.Flags.S == 0)
            {
                Ret();
                registers.PC--;
            }
        }

        private void OP_F1()
        {
            byte flags = registers.memory[registers.SP];
            registers.Flags.SetFromByte(flags);
            registers.A = registers.memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_F2()
        {
            if (registers.Flags.S == 0)
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
            registers.memory[registers.SP - 2] = registers.Flags.ToByte();
            registers.memory[registers.SP - 1] = registers.A;
            registers.SP -= 2;
        }

        private void OP_F6()
        {
            ulong data = registers.memory[registers.PC + 1];
            ulong value = registers.A | data;
            registers.Flags.UpdateByteCY(value);
            registers.Flags.UpdateZSP((uint)value);
            registers.A = (byte)value;
            registers.PC++;
        }

        //private void OP_F7()
        //{ } // NOP

        private void OP_F8()
        { 
            if(registers.Flags.S == 1)
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

        //private void OP_FD()
        //{ } // NOP

        private void OP_FE()
        {
            ulong addr = (ulong)(registers.A - registers.memory[registers.PC + 1]);
            registers.Flags.UpdateZSP((uint)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.PC++;
        }

        //private void OP_FF()
        //{ } // NOP

        private void NOP()
        { }

        private ushort ReadOpcodeWord()
        {
            return MakeWord(registers.memory[registers.PC + 2], registers.memory[registers.PC + 1]);
        }

        private ushort MakeWord(uint hi, uint lo)
        {
            return (ushort)(hi << 8 | lo);
        }

        private void Call(ushort address, ushort retAddress)
        {
            uint rethi = (uint)((retAddress >> 8) & 0xFF);
            uint retlo = (uint)(retAddress & 0xFF);
            registers.SP--;
            registers.memory[registers.SP] = (byte)rethi;
            registers.SP--;
            registers.memory[registers.SP] = (byte)retlo;
            registers.PC = address;
        }

        private void Ret()
        {
            uint pclo = registers.memory[registers.SP];
            uint pchi = registers.memory[registers.SP + 1];
            registers.PC = MakeWord(pchi, pclo);
            registers.SP += 2;
        }

    }
}