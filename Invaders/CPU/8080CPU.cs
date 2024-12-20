using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

namespace Invaders.CPU
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

        private bool displayAvailable = false;
        public bool DisplayAvailable
        { get { return displayAvailable; } }

        private int @int = 1;

        private uint videoStartAddress;

        public int Int
        { get { return @int; } }

        private byte[] video;
        public byte[] Video
        {
            get { return video; }
        }

        private byte[] portIn = new byte[4]; // 0,1,2,3
        public byte[] PortIn
        {
            set { portIn = value; }
            get { return portIn; }
        }

        private readonly byte[] portOut = new byte[7]; // 2,3,5,6
        public byte[] PortOut
        { get { return portOut; } }

        private readonly byte[] memory;
        public byte[] Memory
        { get { return memory; } }

        private int hardwareShiftRegisterData = 0;
        private int hardwareShiftRegisterOffset = 0;

        public _8080CPU(uint memorySize = 0x10000, uint pc = 0x0000, uint videoStartAddr = 0x2400, uint videoLength = 0x1C00, bool testROM = false, bool debugOut = false)
        {
            memory = new byte[memorySize];
            video = new byte[videoLength];
            videoStartAddress = videoStartAddr;
            if(videoLength != 0 && videoStartAddr != 0) displayAvailable = true;
            registers = new Registers();
            registers.PC = pc;
        }

        public void LoadROM(string filePath, int addr, int length)
        {
            Array.Copy(File.ReadAllBytes(filePath), 0, memory, addr, length);
        }

        public void Start()
        {
            running = true;
            @int = 1;
            while (running)
            {
                @int = 1;
                Tick();
                @int = 2;
                Tick();
                if(displayAvailable)
                    Array.Copy(memory, videoStartAddress, video, 0, video.Length);
            }
        }

        private void Tick()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool interrupted = false;
            while (!interrupted && running)
            {
                byte opcode = memory[registers.PC];
                CallOpcode(opcode);
                registers.PC++;
                if (stopwatch.ElapsedMilliseconds > 8 && running)
                {
                    Interrupt(@int);
                    interrupted = true;
                }
            }
        }

        public void Stop()
        {
            running = false;
        }

        private void CallOpcode(byte opcode)
        {
            string func = "OP_" + opcode.ToString("X2");
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(func, BindingFlags.NonPublic | BindingFlags.Instance)!;
            theMethod.Invoke(this, null);
        }

#pragma warning disable IDE0051 // Remove unused private members
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
            memory[registers.BC] = registers.A;
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
            var bit7 = ((registers.A & 0x80) == 0x80) ? 1 : 0;
            registers.A = (byte)((registers.A << 1) | bit7);
            registers.Flags.CY = (byte)bit7;
        }

        private void OP_09()
        {
            var addr = registers.HL + registers.BC;
            registers.Flags.UpdateCarryWord(addr);
            registers.HL = (uint)(addr & 0xFFFF);
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
            var bit0 = registers.A & 0x01;
            registers.A >>= 1;
            registers.A |= (byte)(bit0 << 7);
            registers.Flags.CY = (byte)bit0;
        }

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
            var bit7 = (uint)(((registers.A & 128) == 128) ? 1 : 0);
            var bit0 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A << 1) | bit0);
            registers.Flags.CY = bit7;
        }

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
            var addr = (uint)registers.DE;
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
            var bit0 = registers.A & 1;
            var bit7 = registers.Flags.CY;
            registers.A = (byte)((uint)(registers.A >> 1) | (bit7 << 7));
            registers.Flags.CY = (byte)bit0;
        }

        private static void OP_20() // RIM	1		special
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
                uint addr = (uint)(registers.A + 0x60);
                registers.Flags.UpdateZSP(addr);
                registers.Flags.UpdateCarryByte(addr);
                registers.A = (byte)(addr & 0xFF);
            }
            else
                registers.Flags.CY = 0x00;
        }

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

        private static void OP_30()  // SIM	1		special
        { }

        private void OP_31()
        {
            registers.SP = ReadOpcodeWord();
            registers.PC += 2;
        }

        private void OP_32()
        {
            uint addr = ReadOpcodeWord();
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

        private void OP_39()
        {
            var value = registers.HL + registers.SP;
            registers.Flags.UpdateCarryWord(value);
            registers.HL = (value & 0xFFFF);
        }

        private void OP_3A()
        {
            var addr = ReadOpcodeWord();
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
        {
            registers.B = registers.B;
        }

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
        {
            registers.L = registers.L;
        }

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
            uint addr = registers.HL;
            memory[addr] = registers.B;
        }

        private void OP_71()
        {
            uint addr = registers.HL;
            memory[addr] = registers.C;
        }

        private void OP_72()
        {
            uint addr = registers.HL;
            memory[addr] = registers.D;
        }

        private void OP_73()
        {
            uint addr = registers.HL;
            memory[addr] = registers.E;
        }

        private void OP_74()
        {
            uint addr = registers.HL;
            memory[addr] = registers.H;
        }

        private void OP_75()
        {
            uint addr = registers.HL;
            memory[addr] = registers.L;
        }

        private void OP_76()
        {
            running = false;
        }

        private void OP_77()
        {
            uint addr = registers.HL;
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
        {
            registers.A = registers.A;
        }

        private void OP_80()
        {
            var addr = (uint)registers.A + (uint)registers.B;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

        }

        private void OP_81()
        {
            var addr = (uint)registers.A + (uint)registers.C;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

        }

        private void OP_82()
        {
            var addr = (uint)registers.A + (uint)registers.D;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

        }

        private void OP_83()
        {
            var addr = (uint)registers.A + (uint)registers.E;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

        }

        private void OP_84()
        {
            var addr = (uint)registers.A + (uint)registers.H;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);

        }

        private void OP_85()
        {
            var addr = (uint)registers.A + (uint)registers.L;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_86()
        {
            var addr = (uint)registers.A + (uint)memory[registers.HL];
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL], 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL]);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_87()
        {
            var addr = (uint)registers.A + (uint)registers.A;
            registers.Flags.UpdateAuxCarryFlag(registers.A, registers.A);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_88()
        {
            var addr = (uint)registers.A + (uint)registers.B + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.B);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_89()
        {
            var addr = (uint)registers.A + (uint)registers.C + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.C);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8A()
        {
            var addr = (uint)registers.A + (uint)registers.D + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.D);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8B()
        {
            var addr = (uint)registers.A + (uint)registers.E + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.E);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8C()
        {
            var addr = (uint)registers.A + (uint)registers.H + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.H);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8D()
        {
            var addr = (uint)registers.A + (uint)registers.L + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.L);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8E()
        {
            var addr = (uint)registers.A + (uint)memory[registers.HL] + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL], 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.HL]);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_8F()
        {
            var addr = (uint)registers.A + (uint)registers.A + (uint)registers.Flags.CY;
            if (registers.Flags.CY == 1)
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.A, 1);
            else
                registers.Flags.UpdateAuxCarryFlag(registers.A, registers.A);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_90()
        {
            uint reg = registers.B;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_91()
        {
            uint reg = registers.C;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_92()
        {
            uint reg = registers.D;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_93()
        {
            uint reg = registers.E;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));

            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_94()
        {
            uint reg = registers.H;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_95()
        {
            uint reg = registers.L;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_96()
        {
            uint reg = (uint)memory[registers.HL];
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_97()
        {
            uint reg = registers.A;
            var addr = (uint)(registers.A + (~reg & 0xff) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)((~reg & 0xff) & 0xFF));
            registers.A = (byte)(addr & 0xFF);
        }

        private void OP_98()
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
        }

        private void OP_99()
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
        }

        private void OP_9A()
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
        }

        private void OP_9B()
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
        }

        private void OP_9C()
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
        }

        private void OP_9D()
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
        }

        private void OP_9E()
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
        }

        private void OP_9F()
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
        }

        private void OP_A0()
        {
            registers.A = (byte)(registers.A & registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A1()
        {
            registers.A = (byte)(registers.A & registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A2()
        {
            registers.A = (byte)(registers.A & registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A3()
        {
            registers.A = (byte)(registers.A & registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A4()
        {
            registers.A = (byte)(registers.A & registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A5()
        {
            registers.A = (byte)(registers.A & registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A6()
        {
            registers.A = (byte)(registers.A & memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A7()
        {
            registers.A = (byte)(registers.A & registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A8()
        {
            registers.A = (byte)(registers.A ^ registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_A9()
        {
            registers.A = (byte)(registers.A ^ registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_AA()
        {
            registers.A = (byte)(registers.A ^ registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_AB()
        {
            registers.A = (byte)(registers.A ^ registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_AC()
        {
            registers.A = (byte)(registers.A ^ registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_AD()
        {
            registers.A = (byte)(registers.A ^ registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_AE()
        {
            registers.A = (byte)(registers.A ^ memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_AF()
        {
            registers.A = (byte)(registers.A ^ registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B0()
        {
            registers.A = (byte)(registers.A | registers.B);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B1()
        {
            registers.A = (byte)(registers.A | registers.C);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B2()
        {
            registers.A = (byte)(registers.A | registers.D);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B3()
        {
            registers.A = (byte)(registers.A | registers.E);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B4()
        {
            registers.A = (byte)(registers.A | registers.H);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B5()
        {
            registers.A = (byte)(registers.A | registers.L);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B6()
        {
            registers.A = (byte)(registers.A | memory[registers.HL]);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
        }

        private void OP_B7()
        {
            registers.A = (byte)(registers.A | registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.Flags.CY = 0;
            registers.Flags.AC = 0;
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
            registers.Flags.P = (uint)Flags.CalculateParityFlag(registers.A);
            registers.Flags.CY = 0;
        }

        private void OP_C0()
        {
            if (registers.Flags.Z == 0)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
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
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
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
            var addr = (uint)registers.A + (uint)memory[registers.PC + 1];
            registers.Flags.UpdateAuxCarryFlag(registers.A, memory[registers.PC + 1]);
            registers.Flags.UpdateZSP((byte)addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_C7()
        {
            Call(0x38, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_C8()
        {
            if (registers.Flags.Z == 1)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
            }
        }

        private void OP_C9()
        {
            registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
            registers.SP += 2;
            registers.PC--;
        }

        private void OP_CA()
        {
            if (registers.Flags.Z == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = (uint)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_CC()
        {
            if (registers.Flags.Z == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
                Call((uint)addr, (uint)retAddr);
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
            var retAddr = (uint)(registers.PC + 3);
            Call((uint)addr, (uint)retAddr);
            registers.PC--;
        }

        private void OP_CE()
        {
            var addr = (uint)registers.A;
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
        }

        private void OP_CF()
        {
            Call(0x08, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_D0()
        {
            if (registers.Flags.CY == 0)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
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
                var addr = ReadOpcodeWord();
                registers.PC = (uint)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_D3()
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
        }

        private void OP_D4()
        {
            if (registers.Flags.CY == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
                Call((uint)addr, (uint)retAddr);
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
            var data = memory[registers.PC + 1];
            var addr = (uint)(registers.A - data);
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_D7()
        {
            Call(0x10, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_D8()
        {
            if (registers.Flags.CY == 1)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
            }
        }

        private void OP_DA()
        {
            if (registers.Flags.CY == 1)
            {
                var addr = ReadOpcodeWord();
                registers.PC = (uint)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_DB()
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
        }

        private void OP_DC()
        {
            if (registers.Flags.CY == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
                Call((uint)addr, (uint)retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_DE()
        {
            var data = memory[registers.PC + 1];
            var addr = (uint)(registers.A - data - registers.Flags.CY);
            registers.Flags.UpdateCarryByte(addr);
            registers.Flags.UpdateZSP(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_DF()
        {
            Call(0x18, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_E0()
        {
            if (registers.Flags.P == 0)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
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
                var addr = ReadOpcodeWord();
                registers.PC = (uint)addr;
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_E3()
        {
            var l = registers.L;
            var h = registers.H;
            registers.L = memory[registers.SP];
            registers.H = memory[registers.SP + 1];
            memory[registers.SP] = l;
            memory[registers.SP + 1] = h;
        }

        private void OP_E4()
        {
            if (registers.Flags.P == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
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
            var addr = (uint)(registers.A & memory[registers.PC + 1]);
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateCarryByte(addr);
            registers.A = (byte)(addr & 0xFF);
            registers.PC++;
        }

        private void OP_E7()
        {
            Call(0x20, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_E8()
        {
            if (registers.Flags.P == 1)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
            }
        }

        private void OP_E9()
        {
            registers.PC = (uint)(registers.H << 8 | registers.L);
            registers.PC--;
        }

        private void OP_EA()
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
        }

        private void OP_EB()
        {
            (registers.D, registers.H) = (registers.H, registers.D);
            (registers.L, registers.E) = (registers.E, registers.L);
        }

        private void OP_EC()
        {
            if (registers.Flags.P == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_EE()
        {
            registers.A ^= memory[registers.PC + 1];
            registers.Flags.UpdateCarryByte(registers.A);
            registers.Flags.UpdateZSP(registers.A);
            registers.PC++;
        }

        private void OP_EF()
        {
            Call(0x28, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_F0()
        {
            if (registers.Flags.P == 1)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
            }
        }

        private void OP_F1()
        {
            registers.A = memory[registers.SP + 1];
            var flags = memory[registers.SP];
            if (0x01 == (flags & 0x01)) registers.Flags.Z = 0x01; else registers.Flags.Z = 0x00;
            if (0x02 == (flags & 0x02)) registers.Flags.S = 0x01; else registers.Flags.S = 0x00;
            if (0x04 == (flags & 0x04)) registers.Flags.P = 0x01; else registers.Flags.P = 0x00;
            if (0x08 == (flags & 0x08)) registers.Flags.CY = 0x01; else registers.Flags.CY = 0x00;
            if (0x10 == (flags & 0x10)) registers.Flags.AC = 0x01; else registers.Flags.AC = 0x00;
            registers.SP += 2;
        }

        private void OP_F2()
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
        }

        private void OP_F3()
        {
            registers.INT_ENABLE = false;
        }

        private void OP_F4()
        {
            if (registers.Flags.S == 0)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
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
            memory[registers.SP - 1] = registers.A;
            byte addr = ((byte)(registers.Flags.Z | registers.Flags.S << 1 | registers.Flags.P << 2 | registers.Flags.CY << 3 | registers.Flags.AC << 4));
            memory[registers.SP - 2] = addr;
            registers.SP -= 2;
        }

        private void OP_F6()
        {
            var data = memory[registers.PC + 1];
            var value = (uint)(registers.A | data);
            registers.Flags.UpdateCarryByte(value);
            registers.Flags.UpdateZSP(value);
            registers.A = (byte)value;
            registers.PC++;
        }

        private void OP_F7()
        {
            Call(0x30, (uint)(registers.PC + 2));
            registers.PC--;
        }

        private void OP_F8()
        {
            if (registers.Flags.S == 1)
            {
                registers.PC = (uint)(memory[registers.SP + 1] << 8 | memory[registers.SP]);
                registers.SP += 2;
                registers.PC--;
            }
        }

        private void OP_F9()
        {
            registers.SP = (uint)registers.HL;
        }

        private void OP_FA()
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
        }

        private void OP_FB()
        {
            registers.INT_ENABLE = true;
        }

        private void OP_FC()
        {
            if (registers.Flags.S == 1)
            {
                var addr = ReadOpcodeWord();
                var retAddr = (uint)(registers.PC + 3);
                Call(addr, retAddr);
                registers.PC--;
            }
            else
            {
                registers.PC += 2;
            }
        }

        private void OP_FE()
        {
            UInt16 addr = (UInt16)(registers.A + (byte)(~(memory[registers.PC + 1]) & 0xFF) + 1);
            registers.Flags.UpdateCarryByte(addr);
            if (registers.Flags.CY == 0) registers.Flags.CY = 1; else registers.Flags.CY = 0;
            registers.Flags.UpdateZSP(addr);
            registers.Flags.UpdateAuxCarryFlag(registers.A, (byte)(~(memory[registers.PC + 1]) & 0xFF), 1);
            registers.PC++;
        }

        private void OP_FF()
        {
            Call(0x38, (uint)(registers.PC + 2));
            registers.PC--;
        }
#pragma warning restore IDE0051 // Remove unused private members


        private uint ReadOpcodeWord()
        {
            return (uint)(memory[registers.PC + 2] << 8 | memory[registers.PC + 1]);
        }

        private void Call(uint address, uint retAddress)
        {
            memory[registers.SP - 1] = (byte)((retAddress >> 8) & 0xFF);
            memory[registers.SP - 2] = (byte)(retAddress & 0xFF);
            registers.PC = address;
            registers.SP -= 2;
        }

        private void Interrupt(int num)
        {
            if (!registers.INT_ENABLE)
                return;
            memory[registers.SP - 1] = (byte)((registers.PC >> 8) & 0xFF);
            memory[registers.SP - 2] = (byte)(registers.PC & 0xFF);
            registers.SP -= 2;
            if (num == 1)
                registers.PC = 0x0008;
            if (num == 2)
                registers.PC = 0x0010;
        }
    }
}