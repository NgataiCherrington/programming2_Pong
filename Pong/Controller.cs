using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pong;

namespace task7_graphics
{
    public class Controller
    {
        private Ball ball;
        private Paddle leftPaddle;
        private Paddle rightPaddle;
        private Color color;
        private Graphics graphics;
        private int scoreLeft = 0;
        private int scoreRight = 0;

        public Ball Ball { get => ball; set => ball = value; }
        public Paddle LeftPaddle { get => leftPaddle; set => leftPaddle = value; }
        public Paddle RightPaddle { get => rightPaddle; set => rightPaddle = value; }
        public Color Color { get => color; set => color = value; }
        public int ScoreLeft { get => scoreLeft; set => scoreLeft = value; }
        public int ScoreRight { get => scoreRight; set => scoreRight = value; }
        public Graphics Graphics { get => graphics; set => graphics = value; }

        public Controller(Graphics graphics, Size clientSize) 
        {
            this.Graphics = graphics;   
            Random random = new Random();
            ball = new Ball(new Point(30, 10), new Point(clientSize.Width / 2, clientSize.Height / 2), Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), graphics, clientSize);
            leftPaddle = new Paddle(new Point(20, clientSize.Height / 2), new Point((int)(double) 8.5, (int)(double)8.5), Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), graphics, clientSize);
            rightPaddle = new Paddle(new Point(clientSize.Width - 40, clientSize.Height / 2), new Point((int)(double)8.5, (int)(double)8.5), Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), graphics, clientSize);
        }

   
        public void CheckCollison()
        {
            Rectangle ballBounds = ball.GetBounds();
            Rectangle leftPaddleBounds = leftPaddle.GetBounds();
            Rectangle rightPaddleBounds = rightPaddle.GetBounds();
            
            if (ballBounds.IntersectsWith(leftPaddleBounds))
            {
                int verticalHitDifference = ball.Position.Y - leftPaddle.Position.Y;
                int verticalSpeedChange = verticalHitDifference / (leftPaddle.GetBounds().Height / 2);
                ball.Speed = new Point(-ball.Speed.X, ball.Speed.Y + verticalSpeedChange);
            }

            if (ballBounds.IntersectsWith(rightPaddleBounds))
            {
                int verticalHitDifference = ball.Position.Y - rightPaddle.Position.Y;
                int verticalSpeedChange = verticalHitDifference / (rightPaddle.GetBounds().Height / 2);
                ball.Speed = new Point(-ball.Speed.X, ball.Speed.Y + verticalSpeedChange);
            }
        }

        public void DrawScore()
        {
            Font font = new Font("Tahoma", 16, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.White);

            graphics.DrawString($"Left player: {scoreLeft}", font, brush, 10, 10);
            graphics.DrawString($"Right player: {scoreRight}", font, brush, ball.ClientSize.Width - 200, 10);
        }

        public void Run()
        {
            ball.Move(true);
            CheckCollison();

            if (ball.Position.X < 0) // Ball went out on the left side
            {
                scoreRight++; // Right player scores
                ball.ResetBall();   // Reset the ball
                if (scoreRight == 10)
                {
                    MessageBox.Show("Right player Wins!");
                    ball.Move(false); // Stop the ball
                }
            }
            else if (ball.Position.X > ball.ClientSize.Width) // Ball went out on the right side
            {
                scoreLeft++;  // Left player scores
                ball.ResetBall();   // Reset the ball
                if (scoreLeft == 10)
                {
                    MessageBox.Show("Left player Wins!");
                    ball.Move(false); // Stop the ball
                }
            }

            ball.Draw();
            ball.BounceSide();
            leftPaddle.Draw();
            rightPaddle.Draw();
            DrawScore();
        }
    }
}
