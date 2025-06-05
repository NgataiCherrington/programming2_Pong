using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;
using Pong;

namespace task7_graphics
{
    /// <summary>
    /// Represents the controller for the Pong game, managing the ball, paddles, and game logic
    /// </summary>
    public class Controller : GameObject
    {
        private Ball ball;
        private Paddle leftPaddle;
        private Paddle rightPaddle;
        private SoundPlayer soundPlayer;
        private int scoreLeft = 0;
        private int scoreRight = 0;
        bool gameOver = false; // Variable to track if the game is over
        Random random = new Random();

        public Ball Ball { get => ball; set => ball = value; }
        public Paddle LeftPaddle { get => leftPaddle; set => leftPaddle = value; }
        public Paddle RightPaddle { get => rightPaddle; set => rightPaddle = value; }
        public bool GameOver { get => gameOver; set => gameOver = value; }

        public Controller(Point position, Point speed, Color color, Graphics graphics, Brush brush, Size clientSize) : base(position, speed, color, graphics, brush, clientSize)
        {
            this.Graphics = graphics;

            ball = new Ball(
                new Point(30, 10),
                new Point(clientSize.Width / 2, clientSize.Height / 2),
                Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256)),
                graphics,
                brush,
                clientSize);

            leftPaddle = new Paddle(
                new Point(10, clientSize.Height / 2),
                new Point((int)(double)8.5, (int)(double)8.5),
                Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)),
                graphics,
                brush,
                clientSize
            );

            rightPaddle = new Paddle(
                new Point(clientSize.Width - 30, clientSize.Height / 2),
                new Point((int)(double)8.5, (int)(double)8.5),
                Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)),
                graphics,
                brush,
                clientSize
            );
        }

        /// <summary>
        /// Checks for collisions between the ball and the paddles, and updates the ball's speed accordingly
        /// Ball speeds up everytime hit collides with a paddle
        /// Paddles are divided into top and bottom halves
        /// </summary>
        public void CheckCollison()
        {
            Rectangle ballBounds = ball.GetBounds();  // Get the bounds of the ball
            Rectangle leftPaddleBounds = leftPaddle.GetBounds(); // Get the bounds of the left paddle
            Rectangle rightPaddleBounds = rightPaddle.GetBounds(); // Get the bounds of the right paddle

            soundPlayer = new SoundPlayer(Pong.Properties.Resources.paddle1); // Initialize the sound player

            if (ballBounds.IntersectsWith(leftPaddleBounds)) // Check if the ball intersects with the left paddle
            {
                int paddleMiddle = leftPaddle.Position.Y + leftPaddle.GetBounds().Height / 2; // Calculate the middle of the paddle
                if (ball.Position.Y < paddleMiddle)
                {
                    //ball.Speed = new Point(-ball.Speed.X + 1, -Math.Abs(ball.Speed.Y)); // Reverse the ball's speed and adjust its Y speed
                    ball.Speed = new Point(-ball.Speed.X + 4, ball.Speed.Y + random.Next(-2, 7));
                }
                else
                {
                    //ball.Speed = new Point(-ball.Speed.X + 1, Math.Abs(ball.Speed.Y)); // Reverse the ball's speed and adjust its Y speed
                    ball.Speed = new Point(-ball.Speed.X + 4, ball.Speed.Y + random.Next(-2, 7));
                }
                soundPlayer.Play();
            }

            if (ballBounds.IntersectsWith(rightPaddleBounds))
            {
                int paddleMiddle = rightPaddle.Position.Y + rightPaddle.GetBounds().Height / 2; // Calculate the middle of the paddle

                if (ball.Position.Y < paddleMiddle)
                {
                    //ball.Speed = new Point(-ball.Speed.X + 1, -Math.Abs(ball.Speed.Y)); // Reverse the ball's speed and adjust its Y speed
                    ball.Speed = new Point(-ball.Speed.X + 1, ball.Speed.Y + random.Next(-2, 7));
                }
                else
                {
                    //ball.Speed = new Point(-ball.Speed.X + 1, Math.Abs(ball.Speed.Y)); // Reverse the ball's speed and adjust its Y speed
                    ball.Speed = new Point(-ball.Speed.X + 1, ball.Speed.Y + random.Next(-2, 7));
                }
                soundPlayer.Play();
            }
        }

        public void DrawScore()     // This method draws the score of both players on the screen
        {
            Font font = new Font("Impact", 45, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.WhiteSmoke);

            graphics.DrawString($"{scoreLeft}", font, brush, 100, 10);
            graphics.DrawString($"{scoreRight}", font, brush, ball.ClientSize.Width - 200, 10);
        }

        public void ResetGamePosition()     // This method resets the positions of the ball and paddles, and resets the scores
        {
            ball.Position = new Point(ball.ClientSize.Width / 2, ball.ClientSize.Height / 2);
            leftPaddle.Position = new Point(10, ball.ClientSize.Height / 2);
            rightPaddle.Position = new Point(ball.ClientSize.Width - 30, ball.ClientSize.Height / 2);

            scoreLeft = 0;
            scoreRight = 0;
        }

        public void GameScore()     // This method checks if the ball has gone out of bounds and updates the score accordingly
        {  
            if (ball.Position.X < 0) // Ball went out on the left side
            {
                scoreRight++; // Right player scores
                ball.ResetBall();  // Reset the ball
            }
            else if (ball.Position.X > ball.ClientSize.Width) // Ball went out on the right side
            {
                scoreLeft++;  // Left player scores
                ball.ResetBall();   // Reset the ball
            }
        }

        /// <summary>
        /// Runs the game loop, moving the ball, checking for collisions, updating the score, and drawing the ball and paddles
        /// </summary>
        public void Run()   // This method runs the game loop, moving the ball, checking for collisions, updating the score, and drawing the ball and paddles
        {
            Font font = new Font("Tahoma", 16, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.White);

            if (!gameOver)
            {
                ball.Move(true);
                CheckCollison();
                ball.Draw();
                ball.BounceSide();
                leftPaddle.Draw();
                rightPaddle.Draw();
                DrawScore();
                GameScore();
                
                if (scoreLeft >= 10 || scoreRight >= 10)
                {
                    if (scoreLeft >= 10)
                    {
                        graphics.DrawString("Left Player Wins!", font, brush, ball.ClientSize.Width / 2 - 100, ball.ClientSize.Height / 2);
                    }
                    else if (scoreRight >= 10)
                    {
                        graphics.DrawString("Right Player Wins!", font, brush, ball.ClientSize.Width / 2 - 100, ball.ClientSize.Height / 2);
                    }

                    gameOver = true; // Return the result of GameScore to indicate if the game is over
                }
            }
        }
    }
}
