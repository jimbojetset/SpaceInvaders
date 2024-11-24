using Invaders.CPU;
using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Form1 : Form
    {
        private _8080CPU? cpu;
        private Thread? port_thread;
        private Thread? cpu_thread;
        private Thread? display_thread;
        private Thread? sound_thread;
        private Bitmap? videoBitmap;
        private readonly byte[] inputPorts = new byte[4] { 0x0E, 0x08, 0x00, 0x00 };
        private readonly int SCREEN_WIDTH = 448;
        private readonly int SCREEN_HEIGHT = 512;
        private readonly string appPath = AppDomain.CurrentDomain.BaseDirectory;
        private readonly byte[] video = new byte[0x1C00];

        public Form1()
        {
            InitializeComponent();
            Execute();
        }

        private void Execute()
        {   
            cpu = new _8080CPU(0x0100, true);
            //cpu.LoadROM(appPath + @"ROMS\cputest.com", 0x0100, 0x4B00);
            cpu.LoadROM(appPath + @"ROMS\tst8080.com", 0x0100, 0x0600);
            //cpu.LoadROM(appPath + @"ROMS\zexall.com", 0x0100, 0x2200);
            //cpu.LoadROM(appPath + @"ROMS\test.com", 0x0100, 0x0701);

            //cpu = new _8080CPU(0x0000, false);
            //cpu.LoadROM(appPath + @"ROMS\invaders.h", 0x0000, 0x800); // invaders.h 0000 - 07FF
            //cpu.LoadROM(appPath + @"ROMS\invaders.g", 0x0800, 0x800); // invaders.g 0800 - 0FFF
            //cpu.LoadROM(appPath + @"ROMS\invaders.f", 0x1000, 0x800); // invaders.f 1000 - 17FF
            //cpu.LoadROM(appPath + @"ROMS\invaders.e", 0x1800, 0x800); // invaders.e 1800 - 1FFF

            cpu_thread = new Thread(() => cpu!.Start());
            cpu_thread.IsBackground = true;
            cpu_thread.Start();

            while (!cpu.Running) { }

            port_thread = new Thread(() => PortThread());
            port_thread.IsBackground = true;
            port_thread.Start();

            display_thread = new Thread(() => DisplayThread());
            display_thread.IsBackground = true;
            display_thread.Start();

            sound_thread = new Thread(() => SoundThread());
            sound_thread.IsBackground = true;
            sound_thread.Start();

        }

        private void PortThread()
        {
            while (cpu != null && cpu.Running)
            {
                while (cpu.PortIn == inputPorts)
                    Thread.Sleep(8);
                cpu.PortIn = inputPorts;
            }
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);

        private void DisplayThread()
        {
            while (cpu != null && cpu.Running)
            {
                while (memcmp(video, cpu.Video, video.Length) == 0)
                    Thread.Sleep(8);

                Array.Copy(cpu.Video, 0, video, 0, video.Length);
                videoBitmap = new(SCREEN_WIDTH, SCREEN_HEIGHT);
                using (Graphics graphics = Graphics.FromImage(videoBitmap))
                {
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    int ptr = 0;
                    for (int x = 0; x < SCREEN_WIDTH; x += 2)
                        for (int y = 511; y > 0; y -= 16)
                        {
                            Pen pen = GetPenColor(x, y);
                            byte value = video[ptr++];
                            for (int b = 0; b < 8; b++)
                                if ((value & (1 << b)) != 0)
                                    graphics.DrawRectangle(pen, x, y - (b * 2), 1, 1);
                        }
                }
                try { pictureBox1.Invoke((MethodInvoker)delegate { pictureBox1.BackgroundImage = videoBitmap; }); } catch { }
            }
        }

        private static Pen GetPenColor(int screenPos_X, int screenPos_Y)
        {
            if (screenPos_Y < 478 && screenPos_Y > 390) return new Pen(Color.FromArgb(0xE0, 0x0F, 0xDF, 0x0F));
            if ((screenPos_Y < 512 && screenPos_Y > 480) && (screenPos_X > 0 && screenPos_X < 254)) return new Pen(Color.FromArgb(0xE0, 0x0F, 0xDF, 0x0F));
            if (screenPos_Y < 128 && screenPos_Y > 64) return new Pen(Color.FromArgb(0xE0, 0xFF, 0x40, 0x00));
            return new Pen(Color.FromArgb(0xE0, 0xEF, 0xEF, 0xFF));
        }

        private void SoundThread()
        {
            SoundPlayer player = new();
            byte prevPort3 = new();
            byte prevPort5 = new();

            while (cpu != null && cpu.Running)
            {
                while (prevPort3 == cpu.PortOut[3] && prevPort5 == cpu.PortOut[5])
                    Thread.Sleep(8);

                if (prevPort3 != cpu.PortOut[3])
                {
                    if (((cpu.PortOut[3] & 0x01) == 0x01) && ((cpu.PortOut[3] & 0x01) != (prevPort3 & 0x01)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\ufo_lowpitch.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[3] & 0x02) == 0x02) && ((cpu.PortOut[3] & 0x02) != (prevPort3 & 0x02)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\shoot.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[3] & 0x04) == 0x04) && ((cpu.PortOut[3] & 0x04) != (prevPort3 & 0x04)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\explosion.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[3] & 0x08) == 0x08) && ((cpu.PortOut[3] & 0x08) != (prevPort3 & 0x08)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\invaderkilled.wav";
                        player.PlaySync();
                    }
                    prevPort3 = cpu!.PortOut[3];
                }

                if (prevPort5 != cpu.PortOut[5])
                {
                    if (((cpu.PortOut[5] & 0x01) == 0x01) && ((cpu.PortOut[5] & 0x01) != (prevPort5 & 0x01)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\fastinvader1.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[5] & 0x02) == 0x02) && ((cpu.PortOut[5] & 0x02) != (prevPort5 & 0x02)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\fastinvader2.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[5] & 0x04) == 0x04) && ((cpu.PortOut[5] & 0x04) != (prevPort5 & 0x04)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\fastinvader3.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[5] & 0x08) == 0x08) && ((cpu.PortOut[5] & 0x08) != (prevPort5 & 0x08)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\fastinvader4.wav";
                        player.PlaySync();
                    }
                    if (((cpu.PortOut[5] & 0x10) == 0x10) && ((cpu.PortOut[5] & 0x10) != (prevPort5 & 0x10)))
                    {
                        player.SoundLocation = Application.StartupPath + @"\Sound\explosion.wav";
                        player.PlaySync();
                    }
                    prevPort5 = cpu!.PortOut[5];
                }
            }
        }

        private byte GetKeyValue(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C) return 1;         // Coin
            if (e.KeyCode == Keys.D1) return 2;        // 1P Start
            if (e.KeyCode == Keys.D2) return 3;        // 2P Start
            if (e.KeyCode == Keys.Left) return 4;      // 1P Left
            if (e.KeyCode == Keys.Right) return 5;     // 1P Right
            if (e.KeyCode == Keys.D) return 6;         // 1P Fire
            if (e.KeyCode == Keys.Left) return 7;      // 2P Left
            if (e.KeyCode == Keys.Right) return 8;     // 2P Right
            if (e.KeyCode == Keys.D) return 9;         // 2P Fire
            if (e.KeyCode == Keys.O) return 10;        // Easter Egg Part 1
            if (e.KeyCode == Keys.P) return 11;        // Easter Egg Part 2
            if (e.KeyCode == Keys.T) return 12;        // Tilt
            return 99;
        }

        private void KeyPressed(uint key)
        {
            switch (key)
            {
                case 1: // Coin
                    inputPorts[1] |= 0x01;
                    break;

                case 2: // 1P Start
                    inputPorts[1] |= 0x04;
                    break;

                case 3: // 2P start
                    inputPorts[1] |= 0x02;
                    break;

                case 4: // 1P Left
                    inputPorts[1] |= 0x20;
                    break;

                case 5: // 1P Right
                    inputPorts[1] |= 0x40;
                    break;

                case 6: // 1P Fire
                    inputPorts[1] |= 0x10;
                    break;

                case 7: // 2P Left
                    inputPorts[2] |= 0x20;
                    break;

                case 8: // 2P Right
                    inputPorts[2] |= 0x40;
                    break;

                case 9: // 2P Fire
                    inputPorts[2] |= 0x10;
                    break;

                case 10: // Easter Egg Part 1
                    inputPorts[1] += 0x72;
                    break;

                case 11: // Easter Egg Part 2
                    inputPorts[1] += 0x34;
                    break;

                case 12: // Tilt
                    inputPorts[2] += 0x04;
                    break;
            }
        }

        private void KeyLifted(uint key)
        {
            switch (key)
            {
                case 1: // Coin
                    inputPorts[1] &= 0xFE;
                    break;

                case 2: // 1P Start
                    inputPorts[1] &= 0xFB;
                    break;

                case 3: // 2P start
                    inputPorts[1] &= 0xFD;
                    break;

                case 4: // 1P Left
                    inputPorts[1] &= 0xDF;
                    break;

                case 5: // 1P Right
                    inputPorts[1] &= 0xBF;
                    break;

                case 6: // 1P Fire
                    inputPorts[1] &= 0xEF;
                    break;

                case 7: // 2P Left
                    inputPorts[2] &= 0xDF;
                    break;

                case 8: // 2P Right
                    inputPorts[2] &= 0xBF;
                    break;

                case 9: // 2P Fire
                    inputPorts[2] &= 0xEF;
                    break;

                case 10: // Easter Egg Part 1
                    inputPorts[1] &= 0x8D;
                    break;

                case 11: // Easter Egg Part 2
                    inputPorts[1] &= 0xCB;
                    break;

                case 12: // Tilt
                    inputPorts[2] &= 0xFB;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            uint k = GetKeyValue(e);
            if (k != 99)
                KeyPressed(k);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            uint k = GetKeyValue(e);
            if (k != 99)
                KeyLifted(k);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cpu!.Running = false;
        }
    }
}