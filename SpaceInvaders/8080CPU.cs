using System;
using System.Diagnostics;
using System.IO;

namespace SpaceInvaders
{
    internal class _8080CPU
    {
        public Registers registers;

        public _8080CPU()
        {
            registers = new Registers();
        }

        public void ReadROM(string filePath)
        {
            FileStream romObj = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            romObj.Seek(0, SeekOrigin.Begin);
            registers.PC = 0;
            for (int i = 0; i < romObj.Length; i++)
            {
                registers.memory[i] = (byte)romObj.ReadByte();
            }
        }

        public void RunEmulation()
        {
            while (true)
            {
                CallOpcode(registers.memory[registers.PC]);
            }
        }

        private void CallOpcode(byte opcode)
        {
            string opcodeHex = opcode.ToString("X2");
            if (opcodeHex == "00") NOP();
            if (opcodeHex == "01") OP_01(opcode);
            if (opcodeHex == "02") OP_02(opcode);
            if (opcodeHex == "03") OP_03(opcode);
            if (opcodeHex == "04") OP_04(opcode);
            if (opcodeHex == "05") OP_05(opcode);
            if (opcodeHex == "06") OP_06(opcode);
            if (opcodeHex == "07") OP_07(opcode);
            if (opcodeHex == "08") OP_08(opcode);
            if (opcodeHex == "09") OP_09(opcode);
            if (opcodeHex == "0A") OP_0A(opcode);
            if (opcodeHex == "0B") OP_0B(opcode);
            if (opcodeHex == "0C") OP_0C(opcode);
            if (opcodeHex == "0D") OP_0D(opcode);
            if (opcodeHex == "0E") OP_0E(opcode);
            if (opcodeHex == "0F") OP_0F(opcode);
            if (opcodeHex == "10") OP_10(opcode);
            if (opcodeHex == "11") OP_11(opcode);
            if (opcodeHex == "12") OP_12(opcode);
            if (opcodeHex == "13") OP_13(opcode);
            if (opcodeHex == "14") OP_14(opcode);
            if (opcodeHex == "15") OP_15(opcode);
            if (opcodeHex == "16") OP_16(opcode);
            if (opcodeHex == "17") OP_17(opcode);
            if (opcodeHex == "18") OP_18(opcode);
            if (opcodeHex == "19") OP_19(opcode);
            if (opcodeHex == "1A") OP_1A(opcode);
            if (opcodeHex == "1B") OP_1B(opcode);
            if (opcodeHex == "1C") OP_1C(opcode);
            if (opcodeHex == "1D") OP_1D(opcode);
            if (opcodeHex == "1E") OP_1E(opcode);
            if (opcodeHex == "1F") OP_1F(opcode);
            if (opcodeHex == "20") OP_20(opcode);
            if (opcodeHex == "21") OP_21(opcode);
            if (opcodeHex == "22") OP_22(opcode);
            if (opcodeHex == "23") OP_23(opcode);
            if (opcodeHex == "24") OP_24(opcode);
            if (opcodeHex == "25") OP_25(opcode);
            if (opcodeHex == "26") OP_26(opcode);
            if (opcodeHex == "27") OP_27(opcode);
            if (opcodeHex == "28") OP_28(opcode);
            if (opcodeHex == "29") OP_29(opcode);
            if (opcodeHex == "2A") OP_2A(opcode);
            if (opcodeHex == "2B") OP_2B(opcode);
            if (opcodeHex == "2C") OP_2C(opcode);
            if (opcodeHex == "2D") OP_2D(opcode);
            if (opcodeHex == "2E") OP_2E(opcode);
            if (opcodeHex == "2F") OP_2F(opcode);
            if (opcodeHex == "30") OP_30(opcode);
            if (opcodeHex == "31") OP_31(opcode);
            if (opcodeHex == "32") OP_32(opcode);
            if (opcodeHex == "33") OP_33(opcode);
            if (opcodeHex == "34") OP_34(opcode);
            if (opcodeHex == "35") OP_35(opcode);
            if (opcodeHex == "36") OP_36(opcode);
            if (opcodeHex == "37") OP_37(opcode);
            if (opcodeHex == "38") OP_38(opcode);
            if (opcodeHex == "39") OP_39(opcode);
            if (opcodeHex == "3A") OP_3A(opcode);
            if (opcodeHex == "3B") OP_3B(opcode);
            if (opcodeHex == "3C") OP_3C(opcode);
            if (opcodeHex == "3D") OP_3D(opcode);
            if (opcodeHex == "3E") OP_3E(opcode);
            if (opcodeHex == "3F") OP_3F(opcode);
            if (opcodeHex == "40") OP_40(opcode);
            if (opcodeHex == "41") OP_41(opcode);
            if (opcodeHex == "42") OP_42(opcode);
            if (opcodeHex == "43") OP_43(opcode);
            if (opcodeHex == "44") OP_44(opcode);
            if (opcodeHex == "45") OP_45(opcode);
            if (opcodeHex == "46") OP_46(opcode);
            if (opcodeHex == "47") OP_47(opcode);
            if (opcodeHex == "48") OP_48(opcode);
            if (opcodeHex == "49") OP_49(opcode);
            if (opcodeHex == "4A") OP_4A(opcode);
            if (opcodeHex == "4B") OP_4B(opcode);
            if (opcodeHex == "4C") OP_4C(opcode);
            if (opcodeHex == "4D") OP_4D(opcode);
            if (opcodeHex == "4E") OP_4E(opcode);
            if (opcodeHex == "4F") OP_4F(opcode);
            if (opcodeHex == "50") OP_50(opcode);
            if (opcodeHex == "51") OP_51(opcode);
            if (opcodeHex == "52") OP_52(opcode);
            if (opcodeHex == "53") OP_53(opcode);
            if (opcodeHex == "54") OP_54(opcode);
            if (opcodeHex == "55") OP_55(opcode);
            if (opcodeHex == "56") OP_56(opcode);
            if (opcodeHex == "57") OP_57(opcode);
            if (opcodeHex == "58") OP_58(opcode);
            if (opcodeHex == "59") OP_59(opcode);
            if (opcodeHex == "5A") OP_5A(opcode);
            if (opcodeHex == "5B") OP_5B(opcode);
            if (opcodeHex == "5C") OP_5C(opcode);
            if (opcodeHex == "5D") OP_5D(opcode);
            if (opcodeHex == "5E") OP_5E(opcode);
            if (opcodeHex == "5F") OP_5F(opcode);
            if (opcodeHex == "60") OP_60(opcode);
            if (opcodeHex == "61") OP_61(opcode);
            if (opcodeHex == "62") OP_62(opcode);
            if (opcodeHex == "63") OP_63(opcode);
            if (opcodeHex == "64") OP_64(opcode);
            if (opcodeHex == "65") OP_65(opcode);
            if (opcodeHex == "66") OP_66(opcode);
            if (opcodeHex == "67") OP_67(opcode);
            if (opcodeHex == "68") OP_68(opcode);
            if (opcodeHex == "69") OP_69(opcode);
            if (opcodeHex == "6A") OP_6A(opcode);
            if (opcodeHex == "6B") OP_6B(opcode);
            if (opcodeHex == "6C") OP_6C(opcode);
            if (opcodeHex == "6D") OP_6D(opcode);
            if (opcodeHex == "6E") OP_6E(opcode);
            if (opcodeHex == "6F") OP_6F(opcode);
            if (opcodeHex == "70") OP_70(opcode);
            if (opcodeHex == "71") OP_71(opcode);
            if (opcodeHex == "72") OP_72(opcode);
            if (opcodeHex == "73") OP_73(opcode);
            if (opcodeHex == "74") OP_74(opcode);
            if (opcodeHex == "75") OP_75(opcode);
            if (opcodeHex == "76") OP_76(opcode);
            if (opcodeHex == "77") OP_77(opcode);
            if (opcodeHex == "78") OP_78(opcode);
            if (opcodeHex == "79") OP_79(opcode);
            if (opcodeHex == "7A") OP_7A(opcode);
            if (opcodeHex == "7B") OP_7B(opcode);
            if (opcodeHex == "7C") OP_7C(opcode);
            if (opcodeHex == "7D") OP_7D(opcode);
            if (opcodeHex == "7E") OP_7E(opcode);
            if (opcodeHex == "7F") OP_7F(opcode);
            if (opcodeHex == "80") OP_80(opcode);
            if (opcodeHex == "81") OP_81(opcode);
            if (opcodeHex == "82") OP_82(opcode);
            if (opcodeHex == "83") OP_83(opcode);
            if (opcodeHex == "84") OP_84(opcode);
            if (opcodeHex == "85") OP_85(opcode);
            if (opcodeHex == "86") OP_86(opcode);
            if (opcodeHex == "87") OP_87(opcode);
            if (opcodeHex == "88") OP_88(opcode);
            if (opcodeHex == "89") OP_89(opcode);
            if (opcodeHex == "8A") OP_8A(opcode);
            if (opcodeHex == "8B") OP_8B(opcode);
            if (opcodeHex == "8C") OP_8C(opcode);
            if (opcodeHex == "8D") OP_8D(opcode);
            if (opcodeHex == "8E") OP_8E(opcode);
            if (opcodeHex == "8F") OP_8F(opcode);
            if (opcodeHex == "90") OP_90(opcode);
            if (opcodeHex == "91") OP_91(opcode);
            if (opcodeHex == "92") OP_92(opcode);
            if (opcodeHex == "93") OP_93(opcode);
            if (opcodeHex == "94") OP_94(opcode);
            if (opcodeHex == "95") OP_95(opcode);
            if (opcodeHex == "96") OP_96(opcode);
            if (opcodeHex == "97") OP_97(opcode);
            if (opcodeHex == "98") OP_98(opcode);
            if (opcodeHex == "99") OP_99(opcode);
            if (opcodeHex == "9A") OP_9A(opcode);
            if (opcodeHex == "9B") OP_9B(opcode);
            if (opcodeHex == "9C") OP_9C(opcode);
            if (opcodeHex == "9D") OP_9D(opcode);
            if (opcodeHex == "9E") OP_9E(opcode);
            if (opcodeHex == "9F") OP_9F(opcode);
            if (opcodeHex == "A0") OP_A0(opcode);
            if (opcodeHex == "A1") OP_A1(opcode);
            if (opcodeHex == "A2") OP_A2(opcode);
            if (opcodeHex == "A3") OP_A3(opcode);
            if (opcodeHex == "A4") OP_A4(opcode);
            if (opcodeHex == "A5") OP_A5(opcode);
            if (opcodeHex == "A6") OP_A6(opcode);
            if (opcodeHex == "A7") OP_A7(opcode);
            if (opcodeHex == "A8") OP_A8(opcode);
            if (opcodeHex == "A9") OP_A9(opcode);
            if (opcodeHex == "AA") OP_AA(opcode);
            if (opcodeHex == "AB") OP_AB(opcode);
            if (opcodeHex == "AC") OP_AC(opcode);
            if (opcodeHex == "AD") OP_AD(opcode);
            if (opcodeHex == "AE") OP_AE(opcode);
            if (opcodeHex == "AF") OP_AF(opcode);
            if (opcodeHex == "B0") OP_B0(opcode);
            if (opcodeHex == "B1") OP_B1(opcode);
            if (opcodeHex == "B2") OP_B2(opcode);
            if (opcodeHex == "B3") OP_B3(opcode);
            if (opcodeHex == "B4") OP_B4(opcode);
            if (opcodeHex == "B5") OP_B5(opcode);
            if (opcodeHex == "B6") OP_B6(opcode);
            if (opcodeHex == "B7") OP_B7(opcode);
            if (opcodeHex == "B8") OP_B8(opcode);
            if (opcodeHex == "B9") OP_B9(opcode);
            if (opcodeHex == "BA") OP_BA(opcode);
            if (opcodeHex == "BB") OP_BB(opcode);
            if (opcodeHex == "BC") OP_BC(opcode);
            if (opcodeHex == "BD") OP_BD(opcode);
            if (opcodeHex == "BE") OP_BE(opcode);
            if (opcodeHex == "BF") OP_BF(opcode);
            if (opcodeHex == "C0") OP_C0(opcode);
            if (opcodeHex == "C1") OP_C1(opcode);
            if (opcodeHex == "C2") OP_C2(opcode);
            if (opcodeHex == "C3") OP_C3(opcode);
            if (opcodeHex == "C4") OP_C4(opcode);
            if (opcodeHex == "C5") OP_C5(opcode);
            if (opcodeHex == "C6") OP_C6(opcode);
            if (opcodeHex == "C7") OP_C7(opcode);
            if (opcodeHex == "C8") OP_C8(opcode);
            if (opcodeHex == "C9") OP_C9(opcode);
            if (opcodeHex == "CA") OP_CA(opcode);
            if (opcodeHex == "CB") OP_CB(opcode);
            if (opcodeHex == "CC") OP_CC(opcode);
            if (opcodeHex == "CD") OP_CD(opcode);
            if (opcodeHex == "CE") OP_CE(opcode);
            if (opcodeHex == "CF") OP_CF(opcode);
            if (opcodeHex == "D0") OP_D0(opcode);
            if (opcodeHex == "D1") OP_D1(opcode);
            if (opcodeHex == "D2") OP_D2(opcode);
            if (opcodeHex == "D3") OP_D3(opcode);
            if (opcodeHex == "D4") OP_D4(opcode);
            if (opcodeHex == "D5") OP_D5(opcode);
            if (opcodeHex == "D6") OP_D6(opcode);
            if (opcodeHex == "D7") OP_D7(opcode);
            if (opcodeHex == "D8") OP_D8(opcode);
            if (opcodeHex == "D9") OP_D9(opcode);
            if (opcodeHex == "DA") OP_DA(opcode);
            if (opcodeHex == "DB") OP_DB(opcode);
            if (opcodeHex == "DC") OP_DC(opcode);
            if (opcodeHex == "DD") OP_DD(opcode);
            if (opcodeHex == "DE") OP_DE(opcode);
            if (opcodeHex == "DF") OP_DF(opcode);
            if (opcodeHex == "E0") OP_E0(opcode);
            if (opcodeHex == "E1") OP_E1(opcode);
            if (opcodeHex == "E2") OP_E2(opcode);
            if (opcodeHex == "E3") OP_E3(opcode);
            if (opcodeHex == "E4") OP_E4(opcode);
            if (opcodeHex == "E5") OP_E5(opcode);
            if (opcodeHex == "E6") OP_E6(opcode);
            if (opcodeHex == "E7") OP_E7(opcode);
            if (opcodeHex == "E8") OP_E8(opcode);
            if (opcodeHex == "E9") OP_E9(opcode);
            if (opcodeHex == "EA") OP_EA(opcode);
            if (opcodeHex == "EB") OP_EB(opcode);
            if (opcodeHex == "EC") OP_EC(opcode);
            if (opcodeHex == "ED") OP_ED(opcode);
            if (opcodeHex == "EE") OP_EE(opcode);
            if (opcodeHex == "EF") OP_EF(opcode);
            if (opcodeHex == "F0") OP_F0(opcode);
            if (opcodeHex == "F1") OP_F1(opcode);
            if (opcodeHex == "F2") OP_F2(opcode);
            if (opcodeHex == "F3") OP_F3(opcode);
            if (opcodeHex == "F4") OP_F4(opcode);
            if (opcodeHex == "F5") OP_F5(opcode);
            if (opcodeHex == "F6") OP_F6(opcode);
            if (opcodeHex == "F7") OP_F7(opcode);
            if (opcodeHex == "F8") OP_F8(opcode);
            if (opcodeHex == "F9") OP_F9(opcode);
            if (opcodeHex == "FA") OP_FA(opcode);
            if (opcodeHex == "FB") OP_FB(opcode);
            if (opcodeHex == "FC") OP_FC(opcode);
            if (opcodeHex == "FD") OP_FD(opcode);
            if (opcodeHex == "FE") OP_FE(opcode);
            if (opcodeHex == "FF") OP_FF(opcode);
            registers.PC += 1;
            //throw new Exception("Invalid Opcode");
        }

        private void OP_01(byte opcode)
        {
            registers.C = registers.memory[(byte)(registers.PC + 1)];
            registers.B = registers.memory[(byte)(registers.PC + 2)];
            registers.PC += 3;
        }

        private void OP_02(byte opcode)
        {
            var addr = registers.BC;
            registers.memory[addr] = registers.A;
        }

        private void OP_03(byte opcode)
        {
            var addr = registers.BC;
            addr += 1;
            registers.BC = addr;
        }

        private void OP_04(byte opcode)
        {
            registers.B += 1;
            registers.Flags.UpdateZSP(registers.C);
        }

        private void OP_05(byte opcode)
        {
            registers.B -= 1;
            registers.Flags.UpdateZSP(registers.C);
        }

        private void OP_06(byte opcode)
        {
            registers.B = registers.memory[registers.PC + 1];
            registers.PC += 1;
        }

        private void OP_07(byte opcode)
        {
            int bit7 = ((registers.A & 128) == 128) ? 1 : 0;
            registers.A = (byte)((registers.A << 1) | bit7);
            registers.Flags.CY = (byte)bit7;
        }

        private void OP_08(byte opcode)
        { } // unused

        private void OP_09(byte opcode)
        {
            var addr = registers.HL + registers.BC;
            registers.Flags.UpdateByteCY(addr);
            registers.HL = (ulong)(addr & 0xFFFF);
        }

        private void OP_0A(byte opcode)
        {
            var addr = registers.BC;
            registers.A = registers.memory[addr];
        }

        private void OP_0B(byte opcode)
        {
            var addr = registers.BC;
            addr -= 1;
            registers.BC = addr;
        }

        private void OP_0C(byte opcode)
        {
            registers.C += 1;
            registers.Flags.UpdateZSP(registers.C);
        }

        private void OP_0D(byte opcode)
        {
            registers.C -= 1;
            registers.Flags.UpdateZSP(registers.C);
        }

        private void OP_0E(byte opcode)
        {
            registers.C = registers.memory[registers.PC + 1];
            registers.PC += 1;
        }

        private void OP_0F(byte opcode)
        {
            int bit0 = registers.A & 0x01;
            registers.A >>= 1;
            registers.A |= (byte)(bit0 << 7);
            registers.Flags.CY = (byte)bit0;
        }

        private void OP_10(byte opcode)
        { } // unused

        private void OP_11(byte opcode)
        {
            registers.D = registers.memory[registers.PC + 2];
            registers.E = registers.memory[registers.PC + 1];
            registers.PC += 2;
        }

        private void OP_12(byte opcode)
        {
            var addr = registers.DE;
            registers.memory[addr] = registers.A;
        }

        private void OP_13(byte opcode)
        {
            var addr = registers.DE; ;
            addr += 1;
            registers.DE = addr;
        }

        private void OP_14(byte opcode)
        {
            registers.D += 1;
            registers.Flags.UpdateZSP(registers.D);
        }

        private void OP_15(byte opcode)
        {
            registers.D -= 1;
            registers.Flags.UpdateZSP(registers.D);
        }

        private void OP_16(byte opcode)
        {
            registers.D = registers.memory[registers.PC + 1];
            registers.PC += 1;
        }

        private void OP_17(byte opcode)
        {
            uint bit7 = (uint)(((registers.A & 128) == 128) ? 1 : 0);
            uint bit0 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A << 1) | bit0);
        }

        private void OP_18(byte opcode)
        { } // unused

        private void OP_19(byte opcode)
        {
            var addr = registers.DE + registers.HL;
            registers.Flags.UpdateByteCY(addr);
            registers.HL = addr & 0xFFFF;
        }

        private void OP_1A(byte opcode)
        {
            var addr = registers.DE;
            registers.A = registers.memory[addr];
        }

        private void OP_1B(byte opcode)
        {
            var addr = registers.DE;
            addr -= 1;
            registers.DE = addr;
        }

        private void OP_1C(byte opcode)
        {
            registers.E += 1;
            registers.Flags.UpdateZSP(registers.E);
        }

        private void OP_1D(byte opcode)
        {
            registers.E -= 1;
            registers.Flags.UpdateZSP(registers.E);
        }

        private void OP_1E(byte opcode)
        {
            registers.E = registers.memory[registers.PC + 1];
            registers.PC += 1;
        }

        private void OP_1F(byte opcode)
        {
            int bit0 = registers.A & 1;
            uint bit7 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A >> 1) | (bit7 << 7));
            registers.Flags.CY = (byte)bit0;
        }

        private void OP_20(byte opcode)
        { } // unused

        private void OP_21(byte opcode)
        {
            registers.H = registers.memory[registers.PC + 2];
            registers.L = registers.memory[registers.PC + 1];
            registers.PC += 2;

        }

        private void OP_22(byte opcode)
        {
            var addr = ReadOpcodeWord();
            registers.memory[addr] = registers.L;
            registers.memory[addr + 1] = registers.H;
        }

        private void OP_23(byte opcode)
        {
            var addr = registers.HL;
            addr += 1;
            registers.HL = addr;
        }

        private void OP_24(byte opcode)
        {
            registers.H += 1;
            registers.Flags.UpdateZSP(registers.H);
        }

        private void OP_25(byte opcode)
        {
            registers.H -= 1;
            registers.Flags.UpdateZSP(registers.H);
        }

        private void OP_26(byte opcode)
        {
            registers.H = registers.memory[registers.PC + 1];
            registers.PC += 1;
        }

        private void OP_27(byte opcode)
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

        private void OP_28(byte opcode)
        { } // unused

        private void OP_29(byte opcode)
        {
            var addr = registers.HL + registers.HL;
            registers.Flags.UpdateByteCY(addr);
            registers.HL = addr & 0xFFFF;
        }

        private void OP_2A(byte opcode)
        {
            var addr = ReadOpcodeWord();
            registers.L = registers.memory[addr];
            registers.H = registers.memory[addr + 1];
            registers.PC += 2;
        }

        private void OP_2B(byte opcode)
        {
            var addr = registers.HL;
            addr -= 1;
            registers.HL = addr;
        }

        private void OP_2C(byte opcode)
        {
            registers.L += 1;
            registers.Flags.UpdateZSP(registers.L);
        }

        private void OP_2D(byte opcode)
        {
            registers.L -= 1;
            registers.Flags.UpdateZSP(registers.L);
        }

        private void OP_2E(byte opcode)
        {
            registers.L = registers.memory[registers.PC + 1];
            registers.PC += 1;
        }

        private void OP_2F(byte opcode)
        {
            registers.A = (byte)~registers.A;
        }

        private void OP_30(byte opcode)
        { } // unused

        private void OP_31(byte opcode)
        {
            registers.SP = ReadOpcodeWord();
            registers.SP += 2;
        }

        private void OP_32(byte opcode)
        { 
            ushort addr = ReadOpcodeWord();
            registers.memory[addr] = registers.A;
            registers.PC += 2;
        }

        private void OP_33(byte opcode)
        {
            registers.SP += 1;
        }

        private void OP_34(byte opcode)
        { 
            var addr = registers.HL;
            var value = registers.memory[addr];
            value += 1;
            registers.Flags.UpdateZSP(value);
            registers.memory[addr] = (byte)(value & 0xFF);
        }

        private void OP_35(byte opcode)
        {
            var addr = registers.HL;
            var value = registers.memory[addr];
            value -= 1;
            registers.Flags.UpdateZSP(value);
            registers.memory[addr] = (byte)(value & 0xFF);
        }

        private void OP_36(byte opcode)
        {
            var addr = registers.HL;
            var value = registers.memory[registers.PC + 1];
            registers.memory[addr] = value;
            registers.PC += 1;
        }

        private void OP_37(byte opcode)
        {
            registers.Flags.CY = 1;
        }

        private void OP_38(byte opcode)
        { } // unused

        private void OP_39(byte opcode)
        {
            var value = registers.HL + registers.SP;
            registers.Flags.UpdateByteCY(value);
            registers.HL = (value & 0xFFFF);
        }

        private void OP_3A(byte opcode)
        {
            ushort addr = ReadOpcodeWord();
            registers.A = registers.memory[addr];
            registers.PC += 2;
        }

        private void OP_3B(byte opcode)
        {
            registers.SP -= 1;
        }

        private void OP_3C(byte opcode)
        {
            registers.A += 1;
            registers.Flags.UpdateZSP(registers.A);
        }

        private void OP_3D(byte opcode)
        {
            registers.A -= 1;
            registers.Flags.UpdateZSP(registers.A);
        }

        private void OP_3E(byte opcode)
        { 
            var addr = registers.memory[registers.PC + 1];
            registers.A = addr;
            registers.PC += 1;
        }

        private void OP_3F(byte opcode)
        {
            registers.Flags.CY = (byte)(1 - registers.Flags.CY);
        }

        private void OP_40(byte opcode)
        { } // unused

        private void OP_41(byte opcode)
        {
            registers.B = registers.C;
        }

        private void OP_42(byte opcode)
        {
            registers.B = registers.D;
        }

        private void OP_43(byte opcode)
        {
            registers.B = registers.E;
        }

        private void OP_44(byte opcode)
        {
            registers.B = registers.H;
        }

        private void OP_45(byte opcode)
        {
            registers.B = registers.L;
        }

        private void OP_46(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.B = registers.memory[addr];
        }

        private void OP_47(byte opcode)
        {
            registers.B = registers.A;
        }

        private void OP_48(byte opcode)
        {
            registers.C = registers.B;
        }

        private void OP_49(byte opcode)
        {
            registers.C = registers.C;
        }

        private void OP_4A(byte opcode)
        {
            registers.C = registers.D;
        }

        private void OP_4B(byte opcode)
        {
            registers.C = registers.E;
        }

        private void OP_4C(byte opcode)
        {
            registers.C = registers.H;
        }

        private void OP_4D(byte opcode)
        {
            registers.C = registers.L;
        }

        private void OP_4E(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.C = registers.memory[addr];
        }

        private void OP_4F(byte opcode)
        {
            registers.C = registers.A;
        }

        private void OP_50(byte opcode)
        {
            registers.D = registers.B;
        }

        private void OP_51(byte opcode)
        {
            registers.D = registers.C;
        }

        private void OP_52(byte opcode)
        {
            registers.D = registers.D;
        }

        private void OP_53(byte opcode)
        {
            registers.D = registers.E;
        }

        private void OP_54(byte opcode)
        {
            registers.D = registers.H;
        }

        private void OP_55(byte opcode)
        {
            registers.D = registers.L;
        }

        private void OP_56(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.D = registers.memory[addr];
        }

        private void OP_57(byte opcode)
        {
            registers.D = registers.A;
        }

        private void OP_58(byte opcode)
        {
            registers.E = registers.B;
        }

        private void OP_59(byte opcode)
        {
            registers.E = registers.C;
        }

        private void OP_5A(byte opcode)
        {
            registers.E = registers.D;
        }

        private void OP_5B(byte opcode)
        {
            registers.E = registers.E;
        }

        private void OP_5C(byte opcode)
        {
            registers.E = registers.H;
        }

        private void OP_5D(byte opcode)
        {
            registers.E = registers.L;
        }

        private void OP_5E(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.E = registers.memory[addr];
        }

        private void OP_5F(byte opcode)
        {
            registers.E = registers.A;
        }

        private void OP_60(byte opcode)
        {
            registers.H = registers.B;
        }

        private void OP_61(byte opcode)
        {
            registers.H = registers.C;
        }

        private void OP_62(byte opcode)
        {
            registers.H = registers.D;
        }

        private void OP_63(byte opcode)
        {
            registers.H = registers.E;
        }

        private void OP_64(byte opcode)
        {
            registers.H = registers.H;
        }

        private void OP_65(byte opcode)
        {
            registers.H = registers.L;
        }

        private void OP_66(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.H = registers.memory[addr];
        }

        private void OP_67(byte opcode)
        {
            registers.H = registers.A;
        }

        private void OP_68(byte opcode)
        {
            registers.L = registers.B;
        }

        private void OP_69(byte opcode)
        {
            registers.L = registers.C;
        }

        private void OP_6A(byte opcode)
        {
            registers.L = registers.D;
        }

        private void OP_6B(byte opcode)
        {
            registers.L = registers.E;
        }

        private void OP_6C(byte opcode)
        {
            registers.L = registers.H;
        }

        private void OP_6D(byte opcode)
        {
            registers.L = registers.L;
        }

        private void OP_6E(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.L = registers.memory[addr];
        }

        private void OP_6F(byte opcode)
        {
            registers.L = registers.A;
        }

        private void OP_70(byte opcode)
        { }

        private void OP_71(byte opcode)
        { }

        private void OP_72(byte opcode)
        { }

        private void OP_73(byte opcode)
        { }

        private void OP_74(byte opcode)
        { }

        private void OP_75(byte opcode)
        { }

        private void OP_76(byte opcode)
        {
            // HLT
        }

        private void OP_77(byte opcode)
        { }

        private void OP_78(byte opcode)
        {
            registers.A = registers.B;
        }

        private void OP_79(byte opcode)
        {
            registers.A = registers.C;
        }

        private void OP_7A(byte opcode)
        {
            registers.A = registers.D;
        }

        private void OP_7B(byte opcode)
        {
            registers.A = registers.E;
        }

        private void OP_7C(byte opcode)
        {
            registers.A = registers.H;
        }

        private void OP_7D(byte opcode)
        {
            registers.A = registers.L;
        }

        private void OP_7E(byte opcode)
        {
            var addr = (ulong)registers.H << 8 | (ulong)registers.L;
            registers.A = registers.memory[addr];
        }

        private void OP_7F(byte opcode)
        {
            registers.A = registers.A;
        }

        private void OP_80(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.B;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_81(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.C;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_82(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.D;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_83(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.E;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_84(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.H;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_85(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.L;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_86(byte opcode)
        {
            var addr2 = registers.HL;
            var addr = (uint)registers.A + (uint)registers.memory[addr2];
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_87(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.A;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_88(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.B + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_89(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.C + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8A(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.D + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8B(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.E + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8C(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.H + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8D(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.L + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8E(byte opcode)
        {
            var addr2 = registers.HL;
            var addr = (uint)registers.A + (uint)registers.memory[addr2] + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8F(byte opcode)
        {
            var addr = (uint)registers.A + (uint)registers.A + (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_90(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.B;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_91(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.C;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_92(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.D;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_93(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.E;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_94(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.H;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_95(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.L;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_96(byte opcode)
        {
            var addr2 = registers.HL;
            var addr = (uint)registers.A - (uint)registers.memory[addr2];
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_97(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.A;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_98(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.B - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_99(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.C - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9A(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.D - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9B(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.E - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9C(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.H - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9D(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.L - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9E(byte opcode)
        {
            var addr2 = registers.HL;
            var addr = (uint)registers.A - (uint)registers.memory[addr2] - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_9F(byte opcode)
        {
            var addr = (uint)registers.A - (uint)registers.A - (uint)registers.Flags.CY;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateByteCY(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_A0(byte opcode)
        {
            registers.A &= registers.B;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A1(byte opcode)
        {
            registers.A &= registers.C;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A2(byte opcode)
        {
            registers.A &= registers.D;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A3(byte opcode)
        {
            registers.A &= registers.E;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A4(byte opcode)
        {
            registers.A &= registers.H;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A5(byte opcode)
        {
            registers.A &= registers.L;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A6(byte opcode)
        {
            var addr = registers.HL;
            registers.A &= registers.memory[addr];
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);

        }

        private void OP_A7(byte opcode)
        {
            registers.A &= registers.A;
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A8(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_A9(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AA(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AB(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AC(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AD(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AE(byte opcode)
        {
            var addr = registers.HL;
            registers.A = (byte)(registers.A ^ registers.memory[addr]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_AF(byte opcode)
        {
            registers.A = (byte)(registers.A ^ registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B0(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B1(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B2(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B3(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B4(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B5(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B6(byte opcode)
        {
            var addr = registers.HL;
            registers.A = (byte)(registers.A | registers.memory[addr]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B7(byte opcode)
        {
            registers.A = (byte)(registers.A | registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.UpdateByteCY(registers.A);
        }

        private void OP_B8(byte opcode)
        {
            var addr = (byte)(registers.A - registers.B);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_B9(byte opcode)
        {
            var addr = (byte)(registers.A - registers.C);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BA(byte opcode)
        {
            var addr = (byte)(registers.A - registers.D);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BB(byte opcode)
        {
            var addr = (byte)(registers.A - registers.E);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BC(byte opcode)
        {
            var addr = (byte)(registers.A - registers.H);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BD(byte opcode)
        {
            var addr = (byte)(registers.A - registers.L);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BE(byte opcode)
        {
            var addr2 = registers.HL;
            var addr = (byte)(registers.A - registers.memory[addr2]);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_BF(byte opcode)
        {
            var addr = (byte)(registers.A - registers.A);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateByteCY(addr);
        }

        private void OP_C0(byte opcode)
        { 
            if(registers.Flags.Z == 0)
            {
                Ret();
                registers.PC -= 1;
            }
        }

        private void OP_C1(byte opcode)
        {
            registers.C = registers.memory[registers.SP];
            registers.B = registers.memory[registers.SP + 1];
            registers.SP += 2;
        }

        private void OP_C2(byte opcode)
        {
            if (registers.Flags.Z == 0)
            {
                var addr = ReadOpcodeWord();
                registers.PC = addr;
                registers.PC -= 1;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_C3(byte opcode)
        {
            var addr = ReadOpcodeWord();
            registers.PC = addr;
            registers.PC -= 1;
        }

        private void OP_C4(byte opcode)
        {
            if (registers.Flags.Z == 0)
            {
                ushort addr = ReadOpcodeWord();
                ushort retAddr = registers.PC += 3;
                Call(addr, retAddr);
                registers.PC -= 1;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_C5(byte opcode)
        { }

        private void OP_C6(byte opcode)
        { }

        private void OP_C7(byte opcode)
        { }

        private void OP_C8(byte opcode)
        { }

        private void OP_C9(byte opcode)
        { }

        private void OP_CA(byte opcode)
        { }

        private void OP_CB(byte opcode)
        { }

        private void OP_CC(byte opcode)
        { }

        private void OP_CD(byte opcode)
        { }

        private void OP_CE(byte opcode)
        { }

        private void OP_CF(byte opcode)
        { }

        private void OP_D0(byte opcode)
        { }

        private void OP_D1(byte opcode)
        { }

        private void OP_D2(byte opcode)
        { }

        private void OP_D3(byte opcode)
        { }

        private void OP_D4(byte opcode)
        { }

        private void OP_D5(byte opcode)
        { }

        private void OP_D6(byte opcode)
        { }

        private void OP_D7(byte opcode)
        { }

        private void OP_D8(byte opcode)
        { }

        private void OP_D9(byte opcode)
        { }

        private void OP_DA(byte opcode)
        { }

        private void OP_DB(byte opcode)
        { }

        private void OP_DC(byte opcode)
        { }

        private void OP_DD(byte opcode)
        { }

        private void OP_DE(byte opcode)
        { }

        private void OP_DF(byte opcode)
        { }

        private void OP_E0(byte opcode)
        { }

        private void OP_E1(byte opcode)
        { }

        private void OP_E2(byte opcode)
        { }

        private void OP_E3(byte opcode)
        { }

        private void OP_E4(byte opcode)
        { }

        private void OP_E5(byte opcode)
        { }

        private void OP_E6(byte opcode)
        { }

        private void OP_E7(byte opcode)
        { }

        private void OP_E8(byte opcode)
        { }

        private void OP_E9(byte opcode)
        { }

        private void OP_EA(byte opcode)
        { }

        private void OP_EB(byte opcode)
        { }

        private void OP_EC(byte opcode)
        { }

        private void OP_ED(byte opcode)
        { }

        private void OP_EE(byte opcode)
        { }

        private void OP_EF(byte opcode)
        { }

        private void OP_F0(byte opcode)
        { }

        private void OP_F1(byte opcode)
        { }

        private void OP_F2(byte opcode)
        { }

        private void OP_F3(byte opcode)
        { }

        private void OP_F4(byte opcode)
        { }

        private void OP_F5(byte opcode)
        { }

        private void OP_F6(byte opcode)
        { }

        private void OP_F7(byte opcode)
        { }

        private void OP_F8(byte opcode)
        { }

        private void OP_F9(byte opcode)
        { }

        private void OP_FA(byte opcode)
        { }

        private void OP_FB(byte opcode)
        { }

        private void OP_FC(byte opcode)
        { }

        private void OP_FD(byte opcode)
        { }

        private void OP_FE(byte opcode)
        { }

        private void OP_FF(byte opcode)
        { }

        private void NOP()
        {
            int ticks = 18520;
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedTicks < ticks) { }
        }

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
            registers.memory[registers.SP - 1] = (byte)rethi;
            registers.memory[registers.SP - 2] = (byte)retlo;
            registers.SP -= 2;
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