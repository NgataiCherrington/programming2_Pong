using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
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
        private bool isRunning;


        private HashSet<Keys> pressedKeys;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;  // Enable key events for the form

            offScreenBitmap = new Bitmap(Width, Height); // Create a bitmap for off-screen drawing
            offScreenGraphics = Graphics.FromImage(offScreenBitmap); // Create graphics from the bitmap

            graphics = CreateGraphics(); // Create graphics for the form
            
            controller = new Controller(new Point(0, 0), new Point(0, 0), Color.Black, offScreenGraphics, Brushes.White, ClientSize); // Initialize the offScreen graphics


            timer1.Enabled = false; // Disable the timer initially
            timer1.Interval = 16; // Set the timer interval to approximately 60 FPS

            pressedKeys = new HashSet<Keys>(); // Initialize the set of pressed keys

            pictureBox1.Visible = true; // Logo image
            pictureBox2.Visible = true; // Made by image
            button1.Visible = true; // Play button
            button2.Visible = true; // Controls button
        }

        private void ToggleMenu(bool isShowing)
        {
            if (!isShowing)
            {
                // Hide the menu
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                button1.Visible = false;
                button2.Visible = false;

                // Show the controls menu
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                button3.Visible = false;
            }
        }

        private void TogglePauseMenu(bool isShowing)
        {
           if (!isShowing)
            {
                pictureBox9.Visible = false;
                pictureBox10.Visible = true;
            }

           if (isShowing)
            {
                pictureBox9.Visible = true;
                pictureBox10.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isRunning)
            {
                ToggleMenu(false);
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

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.Space:
                    isRunning = true;
                    TogglePauseMenu(true);
                    pictureBox11.Visible = false; // Hide play image
                    break;
                case Keys.P:
                    isRunning = false;
                    TogglePauseMenu(false);
                    break;
                case Keys.R:
                    isRunning = false;
                    controller.ResetGamePosition();
                    break;     
            }
        }

        private void button1_Click(object sender, EventArgs e) // Play button
        {
            // Hide the menu
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            pictureBox11.Visible = true; // Show play image

            // Start the game
            timer1.Enabled = true;
            controller.Ball.ResetBall();
        }

        private void button2_Click(object sender, EventArgs e) // Controls button
        {
            // Hide the menu
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;

            // Show the controls menu
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
            pictureBox8.Visible = true;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e) // Back to menu button
        {
            // Hide the menu
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            button1.Visible = true;
            button2.Visible = true;

            // Show the controls menu
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false ;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            button3.Visible = false;
        }
    }
}
