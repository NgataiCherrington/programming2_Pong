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
        
        public Ball Ball { get => ball; set => ball = value; }
        public Paddle LeftPaddle { get => leftPaddle; set => leftPaddle = value; }
        public Paddle RightPaddle { get => rightPaddle; set => rightPaddle = value; }
        public Color Color { get => color; set => color = value; }

        public Controller(Graphics graphics, Size clientSize) 
        {
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



        public void Run()
        {
            ball.Draw();
            leftPaddle.Draw();
            rightPaddle.Draw();
            ball.BounceSide();
            ball.Move();
            CheckCollison();
        }
    }
}
