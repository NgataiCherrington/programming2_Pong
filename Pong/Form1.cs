using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task7_graphics;

namespace Pong
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Controller controller;

        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;

        public Form1()
        {
            InitializeComponent();

            graphics = CreateGraphics();
            offScreenBitmap = new Bitmap(Width, Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);
            graphics = CreateGraphics();
            controller = new Controller(offScreenGraphics, ClientSize);
            timer1.Enabled = true;

            controller.Run();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Font font = new Font("Tahoma", 6, FontStyle.Regular);

            for (int x = 0; x < Width; x += 20)
            {
                graphics.DrawLine(Pens.Black, new Point(x, 0), new Point(x, Height));
                graphics.DrawString(x.ToString(), font, Brushes.Black, x, 0);
            }

            for (int y = 0; y < Height; y += 20)
            {
                graphics.DrawLine(Pens.Black, new Point(0, y), new Point(Width, y));
                graphics.DrawString(y.ToString(), font, Brushes.Black, 0, y);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            offScreenGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            controller.Run();
            graphics.DrawImage(offScreenBitmap, 0, 0);
        }
    }
}
