using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            ball = new Ball(new Point(5, 5), new Point(100, 100), Color.White, graphics, clientSize);
            leftPaddle = new Paddle(new Point(20, clientSize.Height / 2), Color.White, graphics, Brushes.White, clientSize);
            rightPaddle = new Paddle(new Point(clientSize.Width - 40, clientSize.Height / 2), Color.White, graphics, Brushes.White, clientSize);
        }
        

        public void Run()
        {
            ball.Move();
            ball.Draw();
            ball.BounceSide();
            leftPaddle.Draw();
            rightPaddle.Draw();
        }

        
    }
}
