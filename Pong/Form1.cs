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
        Ball ball;
        Paddle leftPaddle;
        Paddle rightPaddle;
        

        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;

        private HashSet<Keys> pressedKeys;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            offScreenBitmap = new Bitmap(Width, Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);

            graphics = CreateGraphics();
            controller = new Controller(offScreenGraphics, ClientSize);
            
            timer1.Enabled = false;
            timer1.Interval = 16;
            
            pressedKeys = new HashSet<Keys>();

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            button1.Visible = true;
            button2.Visible = true;


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

            if (pressedKeys.Contains(Keys.W))
            {
                controller.LeftPaddle.MoveUp(true);
            }
            if (pressedKeys.Contains(Keys.S))
            {
                controller.LeftPaddle.MoveDown(true);
            }
            if (pressedKeys.Contains(Keys.Up))
            {
                controller.RightPaddle.MoveUp(true);
            }
            if (pressedKeys.Contains(Keys.Down))
            {
                controller.RightPaddle.MoveDown(true);
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hide the menu
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;

            // Start the game
            timer1.Enabled = true;
            controller.Ball.ResetBall();
        }
    }
}
