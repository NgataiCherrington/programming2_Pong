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
        
        public Ball Ball { get => ball; set => ball = value; }
        public Paddle LeftPaddle { get => leftPaddle; set => leftPaddle = value; }
        public Paddle RightPaddle { get => rightPaddle; set => rightPaddle = value; }

        public Controller(Graphics graphics, Size clientSize) 
        {
            ball = new Ball(new Point(15, 15), new Point(clientSize.Width / 2, clientSize.Height / 2), Color.White, graphics, clientSize);
            leftPaddle = new Paddle(new Point(20, clientSize.Height / 2), new Point((int)(double) 8.5, (int)(double)8.5), Color.White, graphics, Brushes.White, clientSize);
            rightPaddle = new Paddle(new Point(clientSize.Width - 40, clientSize.Height / 2), new Point((int)(double)8.5, (int)(double)8.5), Color.White, graphics, Brushes.White, clientSize);
        }

   
        public void CheckCollison()
        {
            Rectangle ballBounds = ball.GetBounds();
            Rectangle leftPaddleBounds = leftPaddle.GetBounds();
            Rectangle rightPaddleBounds = rightPaddle.GetBounds();
            
            if (ballBounds.IntersectsWith(leftPaddleBounds))
            {
                ball.Speed = new Point(-ball.Speed.X, ball.Speed.Y + (ball.Position.Y - leftPaddle.Position.Y / 10));
            }

            if (ballBounds.IntersectsWith(rightPaddleBounds))
            {
                ball.Speed = new Point(-ball.Speed.X, ball.Speed.Y + (ball.Position.Y - leftPaddle.Position.Y / 10));
            }
        }

        public void ResetBall()
        {
            ball.Position = new Point(ball.ClientSize.Width / 2, ball.ClientSize.Height / 2);
            
            Random random = new Random();
            int speedX = random.Next(5, 10) == 0 ? 5 : -5;
            int speedY = random.Next(-3, 4);

            if (speedY == 0)
            {
                speedY = 1;
            }

            ball.Speed = new Point(speedX, speedY);
        }

        public void Run()
        {
            ball.Move();
            CheckCollison();
            ball.Draw();
            ball.BounceSide();
            leftPaddle.Draw();
            rightPaddle.Draw();
        }
    }
}
