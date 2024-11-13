using Microsoft.Win32;
using SpaceInvaders;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace Invaders
{
    public partial class Form1 : Form
    {
        private _8080CPU? cpu;
        private Thread? cpu_thread;
        private Thread? display_thread;
        private bool displayRunning = false;

        public Form1()
        {
            InitializeComponent();
            Execute();
        }

        private void Execute()
        {
            cpu = new _8080CPU();
            //cpu.paused = true;
            //cpu.ReadROM(@"cpudiag.bin", 256);
            cpu.ReadROM(@"invaders.rom", 0x0);
            cpu_thread = new Thread(() => cpu.RunEmulation());
            cpu_thread.Start();
            while (!cpu.Running) { }
            display_thread = new Thread(() => RunDisplay());
            display_thread.Start();
        }

        private void RunDisplay()
        {
            displayRunning = true;
            while (cpu != null && cpu.Running && displayRunning)
            {
                if (!cpu.Running) { break; }
                Bitmap videoBitmap = new(224, 256);
                int ptr = 0;
                for (int x = 0; x < 224; x++)
                {
                    for (int y = 255; y > 0; y -= 8)
                    {
                        Color color = GetPixelColor(x, y);
                        byte value = cpu.Video[ptr++];
                        for (int b = 0; b < 8; b++)
                        {
                            videoBitmap.SetPixel(x, y - b, Color.FromArgb(180,0,0,0));
                            bool bit = (value & (1 << b)) != 0;
                            if ((value & (1 << b)) != 0)
                                videoBitmap.SetPixel(x, y - b, color);
                        }
                    }
                }
                try
                {
                    pictureBox1.Invoke((MethodInvoker)delegate { pictureBox1.Image = videoBitmap; });
                }
                catch { }
            }
        }

        private Color GetPixelColor(int x, int y)
        {
            if (y < 240 && y > 183) return Color.Green;
            if ((y < 256 && y > 239) && (x > 25 && x < 137)) return Color.Green;
            if (y < 64 && y > 31) return Color.Red;
            return Color.White;
        }

       private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cpu!.Running = false;
            cpu!.paused = false;
            displayRunning = false;
        }
    }
}
