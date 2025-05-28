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

        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;
        private bool isRunning;
        private bool gameOver = true; 
        private SoundPlayer soundPlayer;

        private HashSet<Keys> pressedKeys;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;  // Enable key events for the form

            offScreenBitmap = new Bitmap(Width, Height); // Create a bitmap for off-screen drawing
            offScreenGraphics = Graphics.FromImage(offScreenBitmap); // Create graphics from the bitmap

            graphics = CreateGraphics(); // Create graphics for the form

            int ScoreLeft = 0;
            int ScoreRight = 0;
            controller = new Controller(new Point(0, 0), new Point(0, 0), Color.Black, offScreenGraphics, Brushes.White, ClientSize, ScoreLeft, ScoreRight); // Initialize the offScreen graphics

            timer1.Enabled = false; // Disable the timer initially
            timer1.Interval = 16; // Set the timer interval to approximately 60 FPS

            pressedKeys = new HashSet<Keys>(); // Initialize the set of pressed keys

            pictureBox1.Visible = true; // Logo image
            pictureBox2.Visible = true; // Made by image
            button1.Visible = true; // Play button
            button2.Visible = true; // Controls button

            soundPlayer = new SoundPlayer(Properties.Resources.gameMusic); // Load the sound file
            soundPlayer.PlayLooping(); // Play the sound in a loop
        }

        private void ToggleMenu(bool isShowing)     // This method toggles the visibility of the main menu and controls menu
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

        private void TogglePauseMenu(bool isShowing)    //  This method toggles the visibility of the pause menu
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

        private void timer1_Tick(object sender, EventArgs e)    // This method is called on each timer tick to update the game state
        {
            ToggleMenu(false);
            offScreenGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);    // Clear the off-screen graphics with a black rectangle
            //isRunning = 
            gameOver = controller.Run();   //  Run the game logic
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
            
            if (!isRunning)
            {
                timer1.Enabled = false; // Stop the timer if the game is not running
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)     // This method is called when a key is released
        {
            pressedKeys.Remove(e.KeyCode);  //  Remove the key from the set of pressed keys
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   //  This method is called when a key is pressed
        {
            pressedKeys.Add(e.KeyCode);     //  Add the key to the set of pressed keys

            switch (e.KeyCode)  //  Switch statement to handle key presses
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
                    isRunning = true;
                    controller.ResetGamePosition();
                    controller.ScoreLeft = 0;
                    controller.ScoreRight = 0;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e) // Play button
        {
            // Hide the menu
            pictureBox1.Visible = false;    // Logo image
            pictureBox2.Visible = false;    // Made by image
            button1.Visible = false;        // Play button
            button2.Visible = false;        //  Controls button
            pictureBox11.Visible = true;    // Show play image

            // Start the game
            timer1.Enabled = true;          // Enable the timer
            controller.Ball.ResetBall();
        }

        private void button2_Click(object sender, EventArgs e) // Controls button
        {
            // Hide the menu
            pictureBox1.Visible = false;    // Logo image
            pictureBox2.Visible = false;    // Made by image
            button1.Visible = false;        // Play button
            button2.Visible = false;        // Controls button

            // Show the controls menu
            pictureBox3.Visible = true;     // Controls image 1
            pictureBox4.Visible = true;     // Controls image 2
            pictureBox5.Visible = true;     // Controls image 3
            pictureBox6.Visible = true;     // Controls image 4
            pictureBox8.Visible = true;     // Controls image 5 
            pictureBox7.Visible = true;     // Controls image 6
            button3.Visible = true;         // Back to menu button
        }

        private void button3_Click(object sender, EventArgs e) // Back to menu button
        {
            // Hide the menu
            pictureBox1.Visible = true;     // Logo image
            pictureBox2.Visible = true;     // Made by image
            button1.Visible = true;         // Play button
            button2.Visible = true;         // Controls button

            // Show the controls menu
            pictureBox3.Visible = false;    // Controls image 1
            pictureBox4.Visible = false;    // Controls image 2
            pictureBox5.Visible = false;    // Controls image 3
            pictureBox6.Visible = false;    // Controls image 4
            pictureBox7.Visible = false;    // Controls image 6
            pictureBox8.Visible = false;    // Controls image 5
            button3.Visible = false;        // Back to menu button
        }
    }
}
